using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YtContainer = YoutubeExplode.Videos.Streams.Container;

namespace YouTubeDownloadApp
{
    public partial class YouTubeDownload : Form
    {
        private readonly YoutubeClient _youtube = new YoutubeClient();

        private List<IVideoStreamInfo> _videoStreams = new List<IVideoStreamInfo>();
        private IAudioStreamInfo _audioStream;
        private string _safeVideoTitle = "video";
        private string _videoTitle = string.Empty;
        //test

        public YouTubeDownload()
        {
            InitializeComponent();
            cmbQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            btnDownload.Enabled = false;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
        }

        private async void btnFetch_Click(object sender, EventArgs e)
        {
            string videoUrl = txtUrl.Text.Trim();

            if (string.IsNullOrWhiteSpace(videoUrl))
            {
                MessageBox.Show(
                    "Please enter a YouTube video URL.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SetBusy(true);
            btnDownload.Enabled = false;
            progressBar.Value = 0;
            cmbQuality.Items.Clear();
            _videoStreams.Clear();
            _audioStream = null;

            try
            {
                lblStatus.Text = "Fetching video information...";

                var video = await _youtube.Videos.GetAsync(videoUrl);
                _videoTitle = video.Title;
                _safeVideoTitle = MakeSafeFileName(video.Title);

                lblVideoTitle.Text = $"Title: {video.Title}";
                lblChannel.Text = $"Channel: {video.Author.ChannelTitle}";
                lblDuration.Text = $"Duration: {video.Duration}";

                lblStatus.Text = "Fetching available video qualities...";

                var manifest = await _youtube.Videos.Streams.GetManifestAsync(video.Id);
                _videoStreams = manifest
                    .GetVideoOnlyStreams()
                    .Where(s => s.Container.Equals(YtContainer.Mp4))
                    .OrderByDescending(s => s.VideoResolution.Height)
                    .ThenByDescending(s => s.VideoQuality.Label)
                    .GroupBy(s => s.VideoQuality.Label)
                    .Select(g => (IVideoStreamInfo)g.First())
                    .ToList();

               
                var mp4AudioStreams = manifest
                    .GetAudioOnlyStreams()
                    .Where(s => s.Container.Equals(YtContainer.Mp4))
                    .ToList();

                if (mp4AudioStreams.Count > 0)
                    _audioStream = (IAudioStreamInfo)mp4AudioStreams.GetWithHighestBitrate();

                if (_videoStreams.Count == 0)
                {
                    throw new Exception("No MP4 video streams were found for this video.");
                }

                if (_audioStream == null)
                {
                    throw new Exception("No compatible MP4 audio stream was found for this video.");
                }

                foreach (var stream in _videoStreams)
                {
                    double sizeMb = stream.Size.MegaBytes;
                    cmbQuality.Items.Add(
                        $"{stream.VideoQuality.Label}  |  " +
                        $"{stream.VideoResolution.Width}x{stream.VideoResolution.Height}  |  " +
                        $"{stream.Container.Name.ToUpperInvariant()}  |  " +
                        $"Video {sizeMb:F1} MB");
                }

                cmbQuality.SelectedIndex = 0;
                btnDownload.Enabled = true;
                lblStatus.Text = $"Ready - {_videoStreams.Count} quality option(s) found.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Fetch failed.";
                MessageBox.Show(
                    "Failed to load the video.\r\n\r\n" + ex.Message,
                    "YouTube Download",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            int selectedIndex = cmbQuality.SelectedIndex;

            if (selectedIndex < 0 || selectedIndex >= _videoStreams.Count)
            {
                MessageBox.Show(
                    "Please select a video quality.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_audioStream == null)
            {
                MessageBox.Show(
                    "Audio stream is not loaded. Please click Fetch again.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string ffmpegPath = FindFfmpeg();
            if (string.IsNullOrWhiteSpace(ffmpegPath))
            {
                MessageBox.Show(
                    "FFmpeg was not found.\r\n\r\n" +
                    "Put ffmpeg.exe in the same folder as this application, " +
                    "or add FFmpeg to the Windows PATH, then try again.",
                    "FFmpeg Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var selectedVideo = _videoStreams[selectedIndex];

            var saveDialog = new SaveFileDialog
            {
                Title = "Save YouTube Video",
                Filter = "MP4 Video (*.mp4)|*.mp4",
                DefaultExt = "mp4",
                AddExtension = true,
                FileName = _safeVideoTitle + ".mp4"
            };

            try
            {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                {
                    lblStatus.Text = "Download canceled.";
                    return;
                }

                string outputPath = saveDialog.FileName;
                string tempFolder = Path.Combine(
                    Path.GetTempPath(),
                    "YouTubeDownloadApp",
                    Guid.NewGuid().ToString("N"));

                Directory.CreateDirectory(tempFolder);

                string tempVideo = Path.Combine(tempFolder, "video.mp4");
                string tempAudio = Path.Combine(tempFolder, "audio.m4a");

                try
                {
                    SetBusy(true);
                    btnDownload.Enabled = false;
                    progressBar.Value = 0;

                    lblStatus.Text = $"Downloading video ({selectedVideo.VideoQuality.Label})...";

                    var videoProgress = new Progress<double>(p =>
                    {
                        int value = (int)Math.Round(p * 70.0);
                        progressBar.Value = Math.Max(0, Math.Min(70, value));
                        lblStatus.Text = $"Downloading video... {p:P0}";
                    });

                    await _youtube.Videos.Streams.DownloadAsync(
                        selectedVideo,
                        tempVideo,
                        videoProgress);

                    lblStatus.Text = "Downloading audio...";

                    var audioProgress = new Progress<double>(p =>
                    {
                        int value = 70 + (int)Math.Round(p * 25.0);
                        progressBar.Value = Math.Max(70, Math.Min(95, value));
                        lblStatus.Text = $"Downloading audio... {p:P0}";
                    });

                    await _youtube.Videos.Streams.DownloadAsync(
                        _audioStream,
                        tempAudio,
                        audioProgress);

                    progressBar.Value = 95;
                    lblStatus.Text = "Merging video and audio with FFmpeg...";

                    await MergeWithFfmpegAsync(
                        ffmpegPath,
                        tempVideo,
                        tempAudio,
                        outputPath);

                    progressBar.Value = 100;
                    lblStatus.Text = "Download complete!";

                    MessageBox.Show(
                        $"Video downloaded successfully!\r\n\r\n{outputPath}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Download failed.";
                    try
                    {
                        if (File.Exists(outputPath))
                            File.Delete(outputPath);
                    }
                    catch
                    {
                        
                    }

                    MessageBox.Show(
                        "The download could not be completed.\r\n\r\n" + ex.Message,
                        "YouTube Download",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                finally
                {
                    SetBusy(false);
                    btnDownload.Enabled = _videoStreams.Count > 0;

                    try
                    {
                        if (Directory.Exists(tempFolder))
                            Directory.Delete(tempFolder, true);
                    }
                    catch
                    {
                        
                    }
                }
            }
            finally
            {
                if (saveDialog != null)
                    saveDialog.Dispose();
            }
        }

        private static async Task MergeWithFfmpegAsync(
            string ffmpegPath,
            string videoPath,
            string audioPath,
            string outputPath)
        {
            string arguments =
                $"-y -i \"{videoPath}\" -i \"{audioPath}\" " +
                "-map 0:v:0 -map 1:a:0 -c copy -movflags +faststart " +
                $"\"{outputPath}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            Process process = new Process { StartInfo = startInfo };
            try
            {
                if (!process.Start())
                    throw new Exception("Could not start FFmpeg.");

                string errorText = await process.StandardError.ReadToEndAsync();
                await Task.Run(() => process.WaitForExit());

                if (process.ExitCode != 0)
                {
                    string shortError = errorText;
                    if (shortError.Length > 2000)
                        shortError = shortError.Substring(shortError.Length - 2000);

                    throw new Exception(
                        $"FFmpeg failed with exit code {process.ExitCode}.\r\n\r\n{shortError}");
                }

                if (!File.Exists(outputPath) || new FileInfo(outputPath).Length == 0)
                    throw new Exception("FFmpeg finished, but the output video was not created.");
            }
            finally
            {
                process.Dispose();
            }
        }

        private static string FindFfmpeg()
        {
            // 1. Look beside the application executable.
            string localFfmpeg = Path.Combine(AppContext.BaseDirectory, "ffmpeg.exe");
            if (File.Exists(localFfmpeg))
                return localFfmpeg;

            // 2. Look in Windows PATH.
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "where.exe",
                    Arguments = "ffmpeg",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                Process process = Process.Start(psi);
                if (process == null)
                    return null;

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    string first = output
                        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(first) && File.Exists(first.Trim()))
                        return first.Trim();
                }
            }
            catch
            {
                // Ignore lookup errors.
            }

            return null;
        }

        private static string MakeSafeFileName(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return "video";

            string result = title;
            foreach (char c in Path.GetInvalidFileNameChars())
                result = result.Replace(c, '_');

            result = result.Trim().TrimEnd('.');

            if (string.IsNullOrWhiteSpace(result))
                result = "video";

            // Leave room for extension/path limits.
            if (result.Length > 150)
                result = result.Substring(0, 150).Trim();

            return result;
        }

        private void SetBusy(bool busy)
        {
            btnFetch.Enabled = !busy;
            txtUrl.Enabled = !busy;
            cmbQuality.Enabled = !busy;
            UseWaitCursor = busy;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _youtube.Dispose();
            base.OnFormClosed(e);
        }
    }
}

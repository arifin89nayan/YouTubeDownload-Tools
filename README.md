# YouTubeDownload-Tools

A lightweight **YouTube video downloader for Windows** built with **C#**, **.NET 8**, and **Windows Forms**.

The application uses **YoutubeExplode** to retrieve YouTube video information and available video/audio streams. Users can select the desired video quality, and the application downloads the corresponding video and audio streams separately before combining them into a single playable file using **FFmpeg**.

The project is designed as a simple desktop utility with an easy-to-use graphical interface.

---

## Features

- YouTube URL input
- Fetch YouTube video information
- Detect available video qualities
- Select desired video resolution
- Download high-quality video streams
- Download audio automatically
- Merge video and audio using FFmpeg
- Support for 1080p and higher resolutions when available
- MP4 output
- Windows Forms graphical interface
- Automatic FFmpeg detection
- Save downloaded videos to a user-selected location
- Basic error handling and status messages
- Built with .NET 8
- Uses YoutubeExplode API

---

## Technologies Used

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| .NET 8 | Application framework |
| Windows Forms | Desktop graphical user interface |
| YouTubeExplode | YouTube metadata and stream extraction |
| FFmpeg | Audio and video merging |
| Visual Studio 2022 | Development environment |

---

## Requirements

Before running the application, make sure the following software is installed.

### 1. Visual Studio 2022

Install:

**Visual Studio 2022**

During installation, enable the following workload:


.NET Desktop Development
```text
Application Workflow
┌──────────────────────────┐
│     YouTube Video URL    │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│          Fetch           │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│      YoutubeExplode      │
│                          │
│ Get Video Metadata       │
│ Get Stream Manifest      │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│ Available Video Streams  │
│                          │
│ 2160p                    │
│ 1440p                    │
│ 1080p                    │
│ 720p                     │
│ 480p                     │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│   User Selects Quality   │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│ Download Video-Only      │
│ Stream                   │
└────────────┬─────────────┘
             │
             │
             ▼
┌──────────────────────────┐
│ Download Audio-Only      │
│ Stream                   │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│          FFmpeg          │
│                          │
│ Merge Video + Audio      │
└────────────┬─────────────┘
             │
             ▼
┌──────────────────────────┐
│     Final MP4 Video      │
└──────────────────────────┘

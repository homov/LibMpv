[![Stand With Ukraine](https://raw.githubusercontent.com/vshymanskyy/StandWithUkraine/main/banner2-direct.svg)](https://vshymanskyy.github.io/StandWithUkraine)

LibMpv Wrapper
==============

LibMpv.Client
-------------
The LibMpv.Client project contains a complete libmpv API wrapper automatically generated using a modified version of FFmpeg.AutoGen (LibMpv.Generator)


LibMpv.MVVM
-----------
MpvContext as ViewModel for easier use in MVVM projects


LibMpv.Avalonia
---------------
VideoView (NativeVideoView, OpenGlVideoView, SoftwareVideoView) control for AvaloniaUI

What works:

- Linux (renderers - OpenGl, Software)
- Windows (renderers - OpenGl, Software, Native window)
- Android (renderers - OpenGl). Works on Android Phone emulator but fails on Android TV emulator

To-Do:
- testing, improvements...
- iOS
- MacOS


LibMpv.WPF
----------
VideoView (NativeVideoView) control for WPF (an attempt to solve the airspace issue)

To-Do:
- add simple example for WPF again

Iptv player sample
------------------
Minimal but functional (Avaonia, WPF)

https://github.com/homov/LibMpv/assets/67293663/8c97b57a-b435-47d8-a793-e56d6109b51c


Native binaries
---------------
Platform-specific libraries taken from the projects

- media-kit [media-kit](https://github.com/media-kit/media-kit)
- mpv-android [mpv-android](https://github.com/mpv-android/mpv-android)

﻿# Color Ramp Generator
[![Licence](https://img.shields.io/badge/licence-MIT-brightgreen)](/LICENCE.md)
![Platform](https://img.shields.io/badge/platform-win--32%20%7C%20win--64-lightgrey)
[![Version](https://img.shields.io/github/v/release/Exerionius/ColorRampGenerator?include_prereleases&label=pre-release)](https://github.com/Exerionius/ColorRampGenerator/releases)
[![Downloads](https://img.shields.io/github/downloads-pre/Exerionius/ColorRampGenerator/v0.1.4/total?label=downloads)](https://github.com/Exerionius/ColorRampGenerator/releases)

Color Ramp Generator is a nifty tool to make your life easier when you need to make a color ramp out of the base color.  
This tool is created primarily for programmers who are struggling to create a pixel art palette, spending hours going back and forth between colors in the ramp and tweaking them one by one. Color Ramp Generator allows you to edit the whole ramp at the same time, so you always see if your colors go nicely together or not.

![Screenshot](/screenshot.png?raw=true)

## Installation
Color Ramp Generator requires the [.NET Framework 4.8 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/net48) being installed on your system.  
To use the program just grab the zip file from the latest [release](https://github.com/Exerionius/ColorRampGenerator/releases), unzip it to a folder and run ColorRampGenerator.exe

## Usage

### Getting Started
1. Use Hue, Saturation and Brightness sliders to make a base color of your liking. For the base color I recommend setting Brightness to 50 and Saturation between 50 and 70.
2. Choose your ramp size.
3. Use Hue Shift slider to adjust colors in the ramp. You can choose whether shifts will have the same direction on both sides to the left and to the right of the base color, or they will have opposite directions.
4. Use Hue Shift slider to adjust colors in the ramp the same way you did with Hue.
5. Use Hue Shift slider to adjust colors in the ramp the same way you did with Hue and Saturation.
6. Congratulations! You have made your first color ramp.

### Where to go from here
* Add a couple more ramps in the list. Each ramp is independent of the others, so all ramps can have different sizes, base color settings and shifts.
* Play with custom shifts, see how it looks in the graph.
* Press the "Copy to clipboard" button and paste your palette into a graphic software of your choice.

## Contributing
Pull requests are welcome.

## Acknowledgment
The project uses free [LiveCharts](https://github.com/Live-Charts/Live-Charts/) library for the graph visualization.  
The project contains some modified parts of [Prism](https://github.com/PrismLibrary/Prism) library. Licence can be found [here](https://github.com/PrismLibrary/Prism/blob/master/LICENSE).

Sources of inspiration:
* AdamCYonis' [video](https://www.youtube.com/watch?v=hkrK65FPmDI) about color palettes
* Slynyrd's [article](https://www.slynyrd.com/blog/2018/1/10/pixelblog-1-color-palettes) about color palettes
* [This](http://pixeljoint.com/forum/forum_posts.asp?TID=10695) PixelJoint forum thread

Special thanks:
* [This](https://stackoverflow.com/questions/44177115/copying-from-and-to-clipboard-loses-image-transparency/46424800#46424800) StackOverflow answer for helping copying alpha values to clipboard.

## License
This project is licensed under the MIT License - see the [LICENCE.md](/LICENCE.md) file for details
﻿# Color Ramp Generator
![Platform](https://img.shields.io/badge/platform-win--32%20%7C%20win--64-lightgrey) [![Licence](https://img.shields.io/badge/licence-MIT-brightgreen)](/LICENCE.md)

Color Ramp Generator is a nifty tool to make your life easier when you need to make a color ramp out of the base color.

![Screenshot](/screenshot.png?raw=true)

## Installation
Color Ramp Generator requires the [.NET Framework 4.8 Runtime](https://dotnet.microsoft.com/download/dotnet-framework/net48) being installed on your system.  
To use the program just grab the zip file from the latest [release](https://github.com/Exerionius/ColorRampGenerator/releases), unzip it to a folder and run ColorRampGenerator.exe

## Usage

### Getting Started
1. Use Hue, Saturation and Brightness sliders to make a base color of your liking. For the base color I recommend setting Brightness to 50 and Saturation between 50 and 70.
2. Choose your ramp size.
3. Chose Hue shifts. You can use presets from the drop-down to the right or use your custom values.
4. Chose Saturation shifts the same way you did with Hue.
5. Chose Brightness shifts the same way you did with Hue and Saturation.
6. Congratulations! You have made your first color ramp.

### Where to go from here
* Add a couple more ramps in the list. Each ramp is independent of the others, so all ramps can have different sizes, base color settings and shifts.
* Play with custom shifts, see how it looks in the graph.
* Take a screenshot and use it to pick a colors in the graphic software of your choice.

### What if I want my own shift presets?
You can find the presets.json file in the folder next to the executable file. Feel free to edit it the way you like, just follow the format.

## Contributing
Pull requests are welcome.

## Acknowledgment
The project uses free [LiveCharts](https://github.com/Live-Charts/Live-Charts/) library for the graph visualization.

## License
This project is licensed under the MIT License - see the [LICENCE.md](/LICENCE.md) file for details
# Screenshot Plugin for Xamarin and Windows

A simple Screenshot plugin for Xamarin and Windows to get and save screenshot in yours apps.


## NuGet
* NuGet: [Xam.Plugin.Screenshot](https://www.nuget.org/packages/Xam.Plugin.Screenshot) [![NuGet](https://img.shields.io/nuget/v/Xam.Plugin.Screenshot.svg?label=NuGet)](https://www.nuget.org/packages/Xam.Plugin.Screenshot/)


## Build Status

Build status: [![Build status](https://ci.appveyor.com/api/projects/status/1w46g7ebn59w6d0f?svg=true)](https://ci.appveyor.com/project/wilsonvargas/screenshotplugin)

## Platform Support

|Platform|Version|
| ------------------- | :------------------: |
|Xamarin.iOS|iOS 8+|
|Xamarin.Android|API 14+|
|Windows 10 UWP|10+|

## Documentation

### Get bytes[] from screenshot

```c#
using Plugin.Screenshot;

...

var stream = new MemoryStream(await CrossScreenshot.Current.CaptureAsync());
ImageData.Source = ImageSource.FromStream(() => stream);
```

### Save Screenshot into Gallery Images

```c#
using Plugin.Screenshot;

...

await CrossScreenshot.Current.CaptureAndSaveAsync();
```

### iOS setup
Add in your Info.plist
```xml
<key>NSPhotoLibraryUsageDescription</key>
  <string>This application needs your permission to take photos.</string>
```



### Created By: [@Wilson Vargas](http://twitter.com/Wilson_VargasM)
* Twitter: [@Wilson_VargasM](http://twitter.com/Wilson_VargasM)
* Blog: [blog.wilsonvargas.com](https://blog.wilsonvargas.com), [Héroes del código](http://www.heroesdelcodigo.com/author/wilson/)

### License
The MIT License (MIT), see [LICENSE](LICENSE) file.

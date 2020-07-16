# image-resizer
### a simple image resizer written in C#

image-resizer is a simple command line application built in .NET Core with C# that utilizes ImageSharp to resize an image.
It creates a new image that is resized proportionally based upon the provided width and saves the new image in the same directory as the original.

### requirements

You will need [.NET Core](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/intro) installed in order to run image-resizer

### how to run

Once you have cloned this repository you can use the following format to resize an image.

`dotnet run myImage.jpg 32`

It also works on a full image path as well as relative paths.

`dotnet run C:/test/file/path/myImage.jpg 200`

`dotnet run ../myImage.jpg 200`

### test it out!

Try resizing the test image!

`dotnet run testImg.jpg 200`

<img src="testImg.jpg" alt="Kismet the prairie dog" width="400" />
That's Kismet the prairie dog. She's a silly goober.

Thank you for checking out my repository!

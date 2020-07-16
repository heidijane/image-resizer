/*
~*****Image Resizer*****~
~*****A C# Exercise*****~

~*****Build a simple command line application in .NET Core that resizes an image*****~

The application should accept two command line arguments:

Image path - the location of the image file on the computer
Width - the desired width of the new image

Example:
dotnet run myimage.jpg 32

When executed the application should create a new image in the same directory as the source image.

The new file should contain an image with the desired width and a new height that is proportional to the original height. For example if the source image has a width of 100 and a height of 50 and you resize the image to a width of 50, the height should be 25.

The new filename should be based on the source image filename and should also include the new dimensions. For example a soure file named: myimage.jpg with dimensions 100x50 when resized to a width of 50 should produce a file called myimage_50x25.jpg.

You should use this nuget package: https://www.nuget.org/packages/SixLabors.ImageSharp/
*/

using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace image_resizer
{
    class Program
    {
        static void Main(string[] args)
        {
            /*******
            STEPS!

            [x] install the ImageSharp nuget package

            [x] get the two command line arguments

            [x] check to make sure that user has two command line arguments

            [x] check to make sure that the first argument is a valid image file

            [x] check to make sure that the second argument is a valid integer width

            [x] determine what the image proportional height should be based upon the user supplied width

            [x] make a copy of the image that has a file name with the new width and height (example: myimage_50x25.jpg)
            new file should be in the same directory as the original

            [x] actually resize the image

            [x] save the image
            
            Let's have some fun! :D
            ******/

            //get the command line arguments
            string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length != 3)
            {
                Console.WriteLine("Exactly 2 arguments must be provided.");
                return;
            }

            string image = arguments[1];
            string widthString = arguments[2];

            //see if image exists
            if (!File.Exists(image))
            {
                Console.WriteLine("The file you provided does not exist!");
                Console.WriteLine("Please provide your request in the format: dotnet run imagePath  desiredImageWidth");
                Console.WriteLine("Example: dotnet run myImage.jpg 50");
                return;
            }

            //file exists, now check to see if it is an image
            //there are "better" ways of doing this but since this is a simple application 
            //and the image ins't being uploaded somewhere that it could break something, this should work fine.

            //get filename without extension
            string fileName = Path.GetFileNameWithoutExtension(image);

            //get file extension
            string extension = Path.GetExtension(image);
            extension.ToLower();

            string[] supportedFormats = { ".bmp", ".gif", ".jpg", ".jpeg", ".png" };

            if (!Array.Exists(supportedFormats, format => format == extension))
            {
                Console.WriteLine("File format is invalid. Image must be a .bmp, .gif, .jpg, or .png");
                return;
            }

            int imageWidth = 0;
            //make sure that the image width is an integer
            try
            {
                imageWidth = Int32.Parse(widthString);
            }
            catch
            {
                Console.WriteLine("Second argument must be an integer representing the desired image width");
                return;
            }

            if (imageWidth == 0 || imageWidth > 5000)
            {
                Console.WriteLine("Image width must be greater than 0 and less than 5000.");
                return;
            }

            //load the current image so that we can get its size in pixels
            Image resizedImage = Image.Load(image);

            int originalImageWidth = resizedImage.Width;
            int originalImageHeight = resizedImage.Height;

            //determine the proportional height of the resized image
            int newHeight = imageWidth * originalImageHeight;
            newHeight = newHeight / originalImageWidth;

            resizedImage.Mutate(x => x.Resize(imageWidth, newHeight));
            string directoryName = Path.GetDirectoryName(image);
            if (directoryName != "")
            {
                resizedImage.Save($"{directoryName}\\{fileName}_{imageWidth}x{newHeight}{extension}");
            }
            else
            {
                resizedImage.Save($"{fileName}_{imageWidth}x{newHeight}{extension}");
            }

            Console.WriteLine();
            Console.WriteLine("~* Image Resizer *~");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Resized from {originalImageWidth}x{originalImageHeight} px to {imageWidth}x{newHeight} px");

            if (directoryName != "")
            {
                Console.WriteLine($"Saved as {directoryName}\\{fileName}_{imageWidth}x{newHeight}{extension}!");
            }
            else
            {
                Console.WriteLine($"Saved as {fileName}_{imageWidth}x{newHeight}{extension}!");
            }

        }
    }
}
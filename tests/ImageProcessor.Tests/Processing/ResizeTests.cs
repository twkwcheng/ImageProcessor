using System.Drawing;
using System.Drawing.Drawing2D;
using ImageProcessor.Processing;
using Xunit;

namespace ImageProcessor.Tests.Processing
{
    public class ResizeTests
    {
        private const string category = "Resize";

        [Theory]
        [InlineData(ResizeMode.Crop)]
        [InlineData(ResizeMode.Pad)]
        [InlineData(ResizeMode.BoxPad)]
        [InlineData(ResizeMode.Max)]
        [InlineData(ResizeMode.Min)]
        [InlineData(ResizeMode.Stretch)]
        public void FactoryCanResize(ResizeMode mode)
        {
            TestFile file = TestFiles.Jpeg.Penguins;
            using (var factory = new ImageFactory())
            {
                factory.Load(file.FullName)
                       .Resize(factory.Image.Width / 2, (factory.Image.Height / 2) + 40, mode)
                       .SaveAndCompare(file, category, mode);
            }
        }

        [Theory]
        [InlineData(InterpolationMode.Bilinear)]
        [InlineData(InterpolationMode.Bicubic)]
        [InlineData(InterpolationMode.NearestNeighbor)]
        [InlineData(InterpolationMode.HighQualityBilinear)]
        [InlineData(InterpolationMode.HighQualityBicubic)]
        public void FactoryCanResizeWithDifferentInterpolationModes(InterpolationMode mode)
        {
            TestFile file = TestFiles.Jpeg.Penguins;
            using (var factory = new ImageFactory())
            {
                factory.Load(file.FullName)
                       .Resize(new ResizeOptions
                       {
                           Size = new Size(factory.Image.Width / 2, (factory.Image.Height / 2) + 40),
                           InterpolationMode = mode
                       })
                       .SaveAndCompare(file, category, mode);
            }
        }
    }
}

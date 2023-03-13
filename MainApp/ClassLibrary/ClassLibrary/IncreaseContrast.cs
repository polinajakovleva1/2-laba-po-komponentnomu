using PluginInterface;
using System;
using System.Drawing;
using System.Runtime.Remoting.Channels;

namespace ClassLibrary
{
    [Version(1, 0)]
    public class IncreaseContrast : IPlugin
    {
        public string Name
        {
            get
            {
                return "Повышение контраста изображения";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }

        public void Transform(BitApp app)
        {
            var bitmap = app.Image;
            int r, g, b;
            for (int i = 0; i < bitmap.Width; i++)
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    double col = Math.Pow(((100.0 + 70.0) / 100.0), 2);
                    double temp = ((((color.R / 255.0) - 0.5) * col) + 0.5) * 255.0;
                    r = (int)temp;
                    temp = ((((color.G / 255.0) - 0.5) * col) + 0.5) * 255.0;
                    g = (int)temp;
                    temp = ((((color.B / 255.0) - 0.5) * col) + 0.5) * 255.0;
                    b = (int)temp;
                    if (r < 0) { r = 0; }
                    if (r > 255) { r = 255; }
                    if (g < 0) { g = 0; }
                    if (g > 255) { g = 255; }
                    if (b < 0) { b = 0; }
                    if (b > 255) { b = 255; }
                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, r, g, b));
                }
            app.Image = bitmap;
        }
    }
}

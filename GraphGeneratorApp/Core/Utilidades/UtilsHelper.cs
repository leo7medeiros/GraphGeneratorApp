using SkiaSharp;
using System.Reflection;
using Color = Microsoft.Maui.Graphics.Color;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace GraphGeneratorApp.Core.Utilidades
{
    public static class UtilsHelper
    {
        public static Color GetColorFromResource(string colorKey)
        {
            if (Application.Current?.Resources.TryGetValue(colorKey, out var colorValue) ?? false)
            {
                return (Color)colorValue;
            }
            return Colors.Transparent; // RETORNA UMA COR PADRÃO SE A CHAVE NÃO EXISTIR
        }

        public static SKColor ToSKColor(this Color color)
        {
            return new SKColor(
                (byte)(color.Red * 255),
                (byte)(color.Green * 255),
                (byte)(color.Blue * 255),
                (byte)(color.Alpha * 255));
        }

        private static readonly string BaseResourcePath = "GraphGeneratorApp.Resources.Images.Incorporadas.";

        public static SKSvg LoadSvg(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"{BaseResourcePath}{fileName}";

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new Exception($"O recurso não foi encontrado: {resourceName}. Verifique se ele consta na pasta Images/Incorporadas, e se o modo de compilação está como RECURSO INSERIDO.");

            var svg = new SKSvg();
            svg.Load(stream);
            return svg;
        }

        public static Color Alpha(Color? color, float factor)
        {
            factor = Math.Clamp(factor, 0, 1);

            // CALCULA O VALOR DA NOVA COR BASEADA NO FATOR
            float alpha = color.Alpha * factor;

            return new Color(color.Red, color.Green, color.Blue, alpha);
        }

        public static Color Lighten(Color? color, float factor)
        {
            if (color is not null)
            {
                factor = Math.Clamp(factor, 0, 1);

                // CALCULA O VALOR DA NOVA COR BASEADA NO FATOR
                float red = color.Red + (1 - color.Red) * factor;
                float green = color.Green + (1 - color.Green) * factor;
                float blue = color.Blue + (1 - color.Blue) * factor;

                return new Color(red, green, blue, color.Alpha);
            }
            return Colors.Transparent;
        }
    }
}
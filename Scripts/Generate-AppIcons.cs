#:package Svg.Skia@5.2.3
#:property ManagePackageVersionsCentrally=false

// Renders the brand SVGs to the raster assets of the Avalonia app:
//   - Docs/Design/Icons/crepeduchef-icon.svg -> CrepeDuChef.Avalonia/Assets/crepeduchef.ico (multi-size)
//   - CrepeDuChef.Maui/Resources/AppIcon/appiconfg.svg -> CrepeDuChef.Avalonia/Assets/logo.png
// Usage (from the repository root): dotnet run Scripts/Generate-AppIcons.cs [--preview <dir>]

using SkiaSharp;
using Svg.Skia;

const string IconSource = "Docs/Design/Icons/crepeduchef-icon.svg";
const string IconOutput = "CrepeDuChef.Avalonia/Assets/crepeduchef.ico";
const string LogoSource = "CrepeDuChef.Maui/Resources/AppIcon/appiconfg.svg";
const string LogoOutput = "CrepeDuChef.Avalonia/Assets/logo.png";
const int LogoSize = 256;

int[] iconSizes = [16, 24, 32, 48, 64, 128, 256];

int previewIndex = Array.IndexOf(args, "--preview");
string? previewDir = previewIndex >= 0 && previewIndex + 1 < args.Length ? args[previewIndex + 1] : null;

// Icon
List<byte[]> icons = [.. iconSizes.Select(size => RenderPng(IconSource, size))];
WriteIco(IconOutput, iconSizes, icons);
Console.WriteLine($"Wrote {IconOutput} ({string.Join(", ", iconSizes)} px)");

// Logo
byte[] logo = RenderPng(LogoSource, LogoSize);
File.WriteAllBytes(LogoOutput, logo);
Console.WriteLine($"Wrote {LogoOutput} ({LogoSize} px)");

if (previewDir is not null)
{
    Directory.CreateDirectory(previewDir);
    for (int i = 0; i < iconSizes.Length; i++)
    {
        File.WriteAllBytes(Path.Combine(previewDir, $"icon-{iconSizes[i]}.png"), icons[i]);
    }
    File.WriteAllBytes(Path.Combine(previewDir, "logo.png"), logo);
}

static byte[] RenderPng(string svgPath, int size)
{
    using SKSvg svg = new();
    SKPicture picture = svg.Load(svgPath) ?? throw new InvalidOperationException($"Cannot load '{svgPath}'.");
    SKRect bounds = picture.CullRect;

    using SKBitmap bitmap = new(size, size, SKColorType.Rgba8888, SKAlphaType.Premul);
    using (SKCanvas canvas = new(bitmap))
    {
        canvas.Clear(SKColors.Transparent);
        canvas.Scale(size / bounds.Width, size / bounds.Height);
        canvas.DrawPicture(picture);
    }

    using SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);
    return data.ToArray();
}

// ICO container with PNG-compressed entries (supported since Windows Vista).
static void WriteIco(string path, int[] sizes, List<byte[]> images)
{
    Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);

    using FileStream stream = File.Create(path);
    using BinaryWriter writer = new(stream);

    writer.Write((short)0);            // reserved
    writer.Write((short)1);            // type: icon
    writer.Write((short)sizes.Length); // image count

    int offset = 6 + (16 * sizes.Length);
    for (int i = 0; i < sizes.Length; i++)
    {
        byte dimension = (byte)(sizes[i] >= 256 ? 0 : sizes[i]); // 0 means 256
        writer.Write(dimension);       // width
        writer.Write(dimension);       // height
        writer.Write((byte)0);         // palette size
        writer.Write((byte)0);         // reserved
        writer.Write((short)1);        // color planes
        writer.Write((short)32);       // bits per pixel
        writer.Write(images[i].Length);
        writer.Write(offset);
        offset += images[i].Length;
    }

    foreach (byte[] image in images)
    {
        writer.Write(image);
    }
}

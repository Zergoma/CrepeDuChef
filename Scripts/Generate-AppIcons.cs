#:package Svg.Skia@5.2.3
#:property ManagePackageVersionsCentrally=false

// Renders the app icon SVG to PNG previews and to a multi-size .ico for the Avalonia app.
// Usage (from the repository root): dotnet run Scripts/Generate-AppIcons.cs
//   optional args: <source.svg> <output.ico> <preview-dir>

using SkiaSharp;
using Svg.Skia;

string source = args.Length > 0 ? args[0] : "Docs/Design/Icons/crepeduchef-icon.svg";
string icoPath = args.Length > 1 ? args[1] : "CrepeDuChef.Avalonia/Assets/crepeduchef.ico";
string? previewDir = args.Length > 2 ? args[2] : null;

int[] sizes = [16, 24, 32, 48, 64, 128, 256];

using SKSvg svg = new();
SKPicture picture = svg.Load(source) ?? throw new InvalidOperationException($"Cannot load '{source}'.");
SKRect bounds = picture.CullRect;

List<byte[]> images = [];
foreach (int size in sizes)
{
    byte[] png = RenderPng(picture, bounds, size);
    images.Add(png);

    if (previewDir is not null)
    {
        Directory.CreateDirectory(previewDir);
        File.WriteAllBytes(Path.Combine(previewDir, $"icon-{size}.png"), png);
    }
}

Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(icoPath))!);
WriteIco(icoPath, sizes, images);
Console.WriteLine($"Wrote {icoPath} ({string.Join(", ", sizes)} px)");

static byte[] RenderPng(SKPicture picture, SKRect bounds, int size)
{
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

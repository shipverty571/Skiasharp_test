using SkiaSharp;

namespace skiasharp_test_app.Model;

public class Rect
{
    public Rect(Point point, int width, int height, SKPaint paint)
    {
        Point = point;
        Width = width;
        Height = height;
        Paint = paint;
    }
    
    public Point Point { get; set; }
    
    public int Width { get; set; }
    
    public int Height { get; set; }
    
    public SKPaint Paint { get; set; }
}
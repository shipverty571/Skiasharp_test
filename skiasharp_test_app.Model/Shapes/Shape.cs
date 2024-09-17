using System.Diagnostics;
using SkiaSharp;

namespace skiasharp_test_app.Model.Shapes;

public abstract class Shape
{
    public abstract int X { get; set; }
    
    public abstract int Y { get; set; }
    
    public abstract int Width { get; }
    
    public abstract int Height { get; }
    
    public abstract SKPaint Paint { get; set; }

    public bool ContainsPoint(SKPoint point)
    {
        return point.X >= X && point.X <= X + Width && point.Y >= Y && point.Y <= Y + Height;
    }

    public abstract SKPath GetPath();
}
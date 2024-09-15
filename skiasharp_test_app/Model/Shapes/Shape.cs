using SkiaSharp;

namespace skiasharp_test_app.Model.Shapes;

public abstract class Shape
{
    public abstract int X { get; set; }
    
    public abstract int Y { get; set; }
    
    public abstract SKPaint Paint { get; set; }

    public abstract bool ContainsPoint(SKPoint point);

    public abstract SKPath GetPath();
}
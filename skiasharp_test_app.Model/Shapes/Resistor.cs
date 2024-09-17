using SkiaSharp;

namespace skiasharp_test_app.Model.Shapes;

public class Resistor : Shape
{
    public Resistor()
    {
        Width = 100;
        Height = 20;
    }
    
    public override int X { get; set; }
    
    public override int Y { get; set; }
    
    public override int Width { get; }
    
    public override int Height { get; }

    public override SKPaint Paint { get; set; }

    public override SKPath GetPath()
    {
        var path = new SKPath();
        path.MoveTo(X, Y + 10);
        path.LineTo(X + 20, Y + 10);
        path.AddRect(new SKRect(X + 20, Y, X + 80, Y + 20));
        path.MoveTo(X + 80, Y + 10);
        path.LineTo(X + 100, Y + 10);
        path.Close();
        
        return path;
    }
}
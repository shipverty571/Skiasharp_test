using SkiaSharp;

namespace skiasharp_test_app.Model.Shapes;

public class Resistor : Shape
{
    public override int X { get; set; }
    
    public override int Y { get; set; }
    
    public override SKPaint Paint { get; set; }
    
    public override bool ContainsPoint()
    {
        throw new NotImplementedException();
    }

    public override SKPath GetPath()
    {
        throw new NotImplementedException();
    }
}
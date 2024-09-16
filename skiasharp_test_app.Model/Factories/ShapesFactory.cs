using SkiaSharp;
using skiasharp_test_app.Model.Shapes;

namespace skiasharp_test_app.Model.Factories;

public static class ShapesFactory
{
    public static Resistor Resistor()
    {
        var resistor = new Resistor();
        resistor.X = 200;
        resistor.Y = 200;
        resistor.Paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 2
        };

        return resistor;
    }
}
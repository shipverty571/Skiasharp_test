using SkiaSharp;
using skiasharp_test_app.Model.Shapes;

namespace skiasharp_test_app.Model;

public static class TestData
{
    private static List<Shape> _shapes = new List<Shape>();

    private static List<SKColor> _colors = new List<SKColor>
    {
        SKColors.Chartreuse,
        SKColors.Firebrick,
        SKColors.Blue,
        SKColors.Bisque,
        SKColors.Coral,
        SKColors.Gold,
        SKColors.Black
    };

    public static List<Shape> GetShapes()
    {
        if (_shapes.Count == 0)
        {
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                Shape shape = new Resistor();
                shape.X = random.Next(0, 1000);
                shape.Y = random.Next(0, 1000);
                shape.Paint = new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Black,
                    StrokeWidth = 5
                };
                _shapes.Add(shape);
            }
        }

        return _shapes;
    }
}
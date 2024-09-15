using SkiaSharp;

namespace skiasharp_test_app.Model;

public static class TestData
{
    private static List<Rect> _rectangles = new List<Rect>();

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

    public static List<Rect> GetRectangles()
    {
        if (_rectangles.Count == 0)
        {
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                _rectangles.Add(new Rect(
                    new Point(random.Next(0, 1000), random.Next(0, 1000)),
                    50,
                    50,
                    new SKPaint{Color = _colors[random.Next(0, 7)]})
                );
            }
        }

        return _rectangles;
    }
}
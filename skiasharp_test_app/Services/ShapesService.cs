using SkiaSharp;

namespace skiasharp_test_app.Services;

public static class ShapesService
{
    public static SKPath GetResistor()
    {
        var path = new SKPath();
        path.MoveTo(0, 10);
        path.LineTo(20, 10);
        path.AddRect(new SKRect(20, 0, 80, 20));
        path.MoveTo(80, 10);
        path.LineTo(100, 10);
        path.Close();
        
        return path;
    }
}
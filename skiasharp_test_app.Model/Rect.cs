namespace skiasharp_test_app.Model;

public class Rect
{
    public Rect(Point point, int width, int height)
    {
        Point = point;
        Width = width;
        Height = height;
    }
    
    public Point Point { get; set; }
    
    public int Width { get; set; }
    
    public int Height { get; set; }
}
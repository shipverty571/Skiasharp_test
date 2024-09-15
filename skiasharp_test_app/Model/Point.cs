using System.Diagnostics;

namespace skiasharp_test_app.Model;

public class Point : IEquatable<Point>
{
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point()
    {
        
    }

    public Point(Point point)
    {
        X = point.X;
        Y = point.Y;
    }
    
    public int X { get; set; }
    
    public int Y { get; set; }
    
    public bool Equals(Point? subject)
    {
        if (subject == null) return false;
        if (ReferenceEquals(this, subject)) return true;
        return
            X == subject.X &&
            Y == subject.Y;
    }

    public override bool Equals(object? subject)
    {
        if (subject == null) return false;
        if (ReferenceEquals(this, subject)) return true;
        return Equals((Point)subject);
    }
}
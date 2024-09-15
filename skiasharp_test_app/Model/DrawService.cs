using System.Windows.Input;
using SkiaSharp;
using SkiaSharp.Views.WPF;

namespace skiasharp_test_app.Model;

public class DrawService
{
    private List<Rect> _rectangles = TestData.GetRectangles();
    
    public DrawService(ToolTipService toolTipService)
    {
        ToolTipService = toolTipService;
        ToolTipService.Callback = CompleteTooltip;
    }
    
    private SKCanvas Canvas { get; set; }
    
    private bool MouseLeftButtonDown { get; set; }
    
    private Point MousePosition { get; set; }
    
    private Point StartMousePosition { get; set; }
    
    private Rect? CurrentRect { get; set; } = null;
    
    public ToolTipService ToolTipService { get; }
    
    public void Render(SKCanvas canvas)
    {
        Canvas = canvas;
        Canvas.Clear(new SKColor(130, 130, 130));
        foreach (var shape in _rectangles)
        {
            Canvas.DrawRect(
                shape.Point.X, 
                shape.Point.Y, 
                shape.Width, 
                shape.Height, 
                shape.Paint);
        }
    }

    public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        MouseLeftButtonDown = false;
    }

    public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        MouseLeftButtonDown = true;
        MousePosition = GetMousePosition(sender, e);
        StartMousePosition = GetMousePosition(sender, e);
        // Перебираю элементы начиная с конца. Это важно!!!!
        for (int i = _rectangles.Count - 1; i >= 0; i--)
        {
            var rect = _rectangles[i];
            if (MousePosition.X < rect.Point.X ||
                MousePosition.X > rect.Point.X + rect.Width) continue;
            if (MousePosition.Y < rect.Point.Y ||
                MousePosition.Y > rect.Point.Y + rect.Height) continue;
            CurrentRect = rect;
            _rectangles.RemoveAt(i);
            _rectangles.Insert(_rectangles.Count - 1, CurrentRect);

            StartMousePosition.X -= CurrentRect.Point.X;
            StartMousePosition.Y -= CurrentRect.Point.Y;
            return;
        }
        
        CurrentRect = null;
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
        MousePosition = GetMousePosition(sender, e);

        if (!MouseLeftButtonDown)
        {
            ToolTipService.StartTimer(new Point(MousePosition));
            return;
        }
        if (CurrentRect is null) return;

        ToolTipService.IsOpen = false;
        var newX = MousePosition.X - StartMousePosition.X;
        var newY = MousePosition.Y - StartMousePosition.Y;
        CurrentRect.Point = new Point(newX, newY);
    }
    
    private Point GetMousePosition(object sender, MouseEventArgs eventArgs)
    {
        var pixelPosition = eventArgs.GetPosition(sender as SKElement);
        
        return new Point
        {
            X = (int)pixelPosition.X,
            Y = (int)pixelPosition.Y
        };
    }

    public bool CompleteTooltip(Point startMousePosition)
    {
        if (!MousePosition.Equals(startMousePosition)) return false;
        
        for (int i = _rectangles.Count - 1; i >= 0; i--)
        {
            var rect = _rectangles[i];
            if (MousePosition.X < rect.Point.X ||
                MousePosition.X > rect.Point.X + rect.Width) continue;
            if (MousePosition.Y < rect.Point.Y ||
                MousePosition.Y > rect.Point.Y + rect.Height) continue;
            ToolTipService.Content = $"Rectangle {i+1}";
            return true;
        }
        
        return false;
    }
}
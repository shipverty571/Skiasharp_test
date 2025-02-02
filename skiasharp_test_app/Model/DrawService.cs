﻿using System.Windows.Input;
using SkiaSharp;
using skiasharp_test_app.Model.Shapes;
using SkiaSharp.Views.WPF;

namespace skiasharp_test_app.Model;

public class DrawService
{
    private List<Shape> _shapes = TestData.GetShapes();
    
    public DrawService(ToolTipService toolTipService)
    {
        ToolTipService = toolTipService;
        ToolTipService.Callback = CompleteTooltip;
    }
    
    private SKCanvas Canvas { get; set; }
    
    private bool MouseLeftButtonDown { get; set; }
    
    private SKPoint MousePosition { get; set; }
    
    private SKPoint StartMousePosition { get; set; }
    
    private Shape? CurrentShape { get; set; } = null;
    
    public ToolTipService ToolTipService { get; }
    
    public void Render(SKCanvas canvas)
    {
        Canvas = canvas;
        Canvas.Clear(new SKColor(130, 130, 130));
        foreach (var shape in _shapes)
        {
            Canvas.DrawPath(shape.GetPath(), shape.Paint);
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
        for (int i = _shapes.Count - 1; i >= 0; i--)
        {
            var shape = _shapes[i];
            if (!shape.ContainsPoint(MousePosition)) continue;
            CurrentShape = shape;
            _shapes.RemoveAt(i);
            _shapes.Insert(_shapes.Count - 1, CurrentShape);

            StartMousePosition = new SKPoint(
                StartMousePosition.X - CurrentShape.X,
                StartMousePosition.Y - CurrentShape.Y);
            return;
        }
        
        CurrentShape = null;
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
        MousePosition = GetMousePosition(sender, e);

        if (!MouseLeftButtonDown)
        {
            ToolTipService.StartTimer(MousePosition);
            return;
        }
        if (CurrentShape is null) return;

        ToolTipService.IsOpen = false;
        var newX = MousePosition.X - StartMousePosition.X;
        var newY = MousePosition.Y - StartMousePosition.Y;
        CurrentShape.X = (int) newX;
        CurrentShape.Y = (int) newY;
    }
    
    private SKPoint GetMousePosition(object sender, MouseEventArgs eventArgs)
    {
        var pixelPosition = eventArgs.GetPosition(sender as SKElement);
        
        return new SKPoint
        {
            X = (int)pixelPosition.X,
            Y = (int)pixelPosition.Y
        };
    }

    public bool CompleteTooltip(SKPoint startMousePosition)
    {
        if (!MousePosition.Equals(startMousePosition)) return false;
        
        for (int i = _shapes.Count - 1; i >= 0; i--)
        {
            var shape = _shapes[i];
            if (!shape.ContainsPoint(MousePosition)) continue;
            
            ToolTipService.Content = $"Shape {i+1}";
            return true;
        }
        
        return false;
    }
}
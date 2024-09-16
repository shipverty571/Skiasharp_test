using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using skiasharp_test_app.Services;
using skiasharp_test_app.ViewModel;
using SkiaSharp.Views.Desktop;
using ToolTipService = skiasharp_test_app.Services.ToolTipService;

namespace skiasharp_test_app;

public partial class MainWindow : Window
{
    private readonly DrawService _drawService;
    
    public MainWindow()
    {
        InitializeComponent();
        
        var vm = new MainVM();
        DataContext = vm;
        
        var toolTipService = new ToolTipService();
        toolTipService.ToolTipStatusChanged += ToolTipService_OnToolTipStatusChanged;
        _drawService = new DrawService(toolTipService);

        var tooltip = new ToolTip();
        Canvas.ToolTip = tooltip;
        tooltip.IsOpen = false;
        
        _ = Task.Run(() =>
        {
            while (true)
            {
                try
                {
                    Dispatcher.Invoke(() =>
                    {
                        Canvas.InvalidateVisual();
                    });
                    _ = SpinWait.SpinUntil(() => false, 1);
                }
                catch
                {
                    break;
                }
            }
        });
    }
    
    private void Canvas_OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
    {
        _drawService.Render(e.Surface.Canvas);
    }

    private void Canvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _drawService.OnMouseLeftButtonDown(sender, e);
    }

    private void Canvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _drawService.OnMouseLeftButtonUp(sender, e);
    }

    private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
    {
        _drawService.OnMouseMove(sender, e);
    }

    private void ToolTipService_OnToolTipStatusChanged(object? sender, EventArgs e)
    {
        Dispatcher.Invoke(() =>
        {
            var toolTip = (ToolTip)Canvas.ToolTip;
            var toolTipService = (ToolTipService)sender!;
            toolTip.Content = toolTipService.Content;
            toolTip.IsOpen = toolTipService.IsOpen;
        });
    }
}
using System.ComponentModel;
using System.Timers;
using SkiaSharp;
using Timer = System.Timers.Timer;

namespace skiasharp_test_app.Model;

public class ToolTipService
{
    private static SKPoint _startMousePosition;

    private static Timer _timer;

    private static AutoResetEvent _autoEvent;

    private static BackgroundWorker _worker;

    public event EventHandler<EventArgs> ToolTipStatusChanged;

    private bool _IsOpen;

    public ToolTipService()
    {
        _worker = new BackgroundWorker();
        _worker.WorkerSupportsCancellation = true;
        _worker.DoWork += DoWork;
        _timer = new Timer()
        {
            AutoReset = false,
            Enabled = false,
            Interval = TimeSpan.FromSeconds(1).TotalMilliseconds
        };
        _timer.Elapsed += CompleteTimer;
    }
    
    public static Predicate<SKPoint> Callback { get; set; }
    
    public string Content { get; set; }

    public bool IsOpen
    {
        get => _IsOpen;
        set
        {
            _IsOpen = value;
            ToolTipStatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void StartTimer(SKPoint startMousePosition)
    {
        IsOpen = false;
        _startMousePosition = startMousePosition;
        if (_worker.IsBusy)
        {
            _timer.Enabled = false;
            _worker.CancelAsync();
            return;
        }
        _worker.RunWorkerAsync();
    }
    
    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        _timer.Enabled = true;
    }

    public void CompleteTimer(object source, ElapsedEventArgs e)
    {
        IsOpen = Callback(_startMousePosition);
        _timer.Enabled = false;
    }
}
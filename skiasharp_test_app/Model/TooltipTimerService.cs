using System.ComponentModel;

namespace skiasharp_test_app.Model;

public class TooltipTimerService
{
    private static BackgroundWorker _worker = new BackgroundWorker();

    private static Point _startMousePosition;

    private static Predicate<Point> _runWorkerComplete;

    private static Timer? _timer;

    public TooltipTimerService(Predicate<Point> runWorkerComplete)
    {
        _worker.WorkerSupportsCancellation = true;
        _worker.DoWork += DoWork;
        _worker.RunWorkerCompleted += RunWorkerComplete;
        _runWorkerComplete = runWorkerComplete;
    }

    public void StartTimer(Point mousePosition)
    {
        if (_timer is not null)
        {
            _timer.Dispose();
            _timer = null;
        }
        else
        {
            var autoEvent = new AutoResetEvent(false);
            _timer = new Timer(_worker.RunWorkerAsync, autoEvent, 4, 100000);
        }
        _startMousePosition = mousePosition;
        /*if (_worker.CancellationPending) return;
        if (_worker.IsBusy)
        {
            _worker.CancelAsync();
            return;
        }*/
        _worker.RunWorkerAsync();
    }

    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        if (_worker.CancellationPending)
        {
            e.Cancel = true;
        }
    }

    private void RunWorkerComplete(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (!e.Cancelled)
        {
            _runWorkerComplete(_startMousePosition);
            _timer?.Dispose();
            _timer = null;
        }
    }
}
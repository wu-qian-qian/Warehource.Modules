namespace UI.Helper;

/// <summary>
///     对Job进行简单的封装
/// </summary>
public class TaskHelper
{
    private static readonly TaskFactory _taskFactory;

    //public List<Task> _jobs = new List<Task>();

    static TaskHelper()
    {
        _taskFactory = new TaskFactory(TaskCreationOptions.LongRunning, TaskContinuationOptions.PreferFairness);
    }

    public static void CreatJob(byte timer, Action act, CancellationToken token)
    {
        var task = _taskFactory.StartNew(() =>
        {
            while (true)
            {
                if (token.IsCancellationRequested) break;
                act();
                Task.Delay(TimeSpan.FromSeconds(timer), token);
            }
        });
    }

    public static void CreatJobAsync(byte timer, Func<Task> act, CancellationToken token)
    {
        _taskFactory.StartNew(async () =>
        {
            while (true)
            {
                if (token.IsCancellationRequested) break;
                await act();
                //休眠
                await Task.Delay(TimeSpan.FromSeconds(timer), token);
            }
        });
    }
}
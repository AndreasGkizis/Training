namespace AsyncExamples;

public class DeadLock
{
    public async Task SimulateSimpleDeadLock()
    {
        var tcs1 = new TaskCompletionSource<bool>();
        var tcs2 = new TaskCompletionSource<bool>();

        var task1 = Task.Run(async () =>
        {
            await tcs1.Task;
            tcs2.SetResult(true);
        });

        var task2 = Task.Run(async () =>
            {
                await tcs2.Task;
                tcs1.SetResult(true);
            }
        );
        
        await Task.WhenAll(task1, task2);
    }
}
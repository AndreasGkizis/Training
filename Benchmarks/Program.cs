using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace Benchmarks;

public class Program
{
	public static void Main()
	{
		var config = DefaultConfig.Instance
									.AddJob(Job
									.ShortRun
									.WithLaunchCount(1)
									.WithToolchain(InProcessNoEmitToolchain.Instance));

		var sum = BenchmarkRunner.Run<ConfigureAwaitBenchmarks>(config);
	}
}

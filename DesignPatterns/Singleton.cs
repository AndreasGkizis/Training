using System.ComponentModel.DataAnnotations;

namespace DesignPatterns;

public sealed class Singleton
{
#pragma warning disable CS8618 
// Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
	private static Singleton _instance;
	// this is the actual ctor which should only be used once!
	private Singleton() { }
	public static Singleton GetInstance()
	{
		if (_instance is null)
		{
			_instance = new Singleton();
		}
		return _instance;
	}
}
public sealed class ThreadSafeSingleton
{
	private static ThreadSafeSingleton _instance;
	public string Name { get; private set; }
	private static readonly object _lock = new object();
	// this is the actual ctor which should only be used once!
	private ThreadSafeSingleton(string name)
	{
		Name = name;
	}
	public static ThreadSafeSingleton GetInstance(string name)
	{
		if (_instance == null)
		{
			lock (_lock)
			{

				if (_instance == null)
				{
					_instance = new ThreadSafeSingleton(name);
				}
			}
		}
		return _instance;
	}
}

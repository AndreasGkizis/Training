using DesignPatterns;

namespace Tests.DesignPatterns;


public class SingletonTests
{
	[Fact]
	public void GetInstance_ShouldReturnSameInstance()
	{
		// Act
		var instance1 = Singleton.GetInstance();
		var instance2 = Singleton.GetInstance();

		// Assert
		Assert.Same(instance1, instance2);
	}
}

public class ThreadSafeSingletonTests
{
	[Fact]
	public void GetInstance_ShouldReturnSameInstance_ForMultipleCalls()
	{
		// Act
		var instance1 = ThreadSafeSingleton.GetInstance("First");
		var instance2 = ThreadSafeSingleton.GetInstance("Second");

		// Assert
		Assert.Same(instance1, instance2);
		Assert.Equal("First", instance1.Name);
		Assert.Equal("First", instance2.Name); 
	}

	// this runs first, so now the name of the signleton is "First" if other tests attempt to create a new one they will not be able to 
	[Fact]
	public void GetInstance_ShouldBeThreadSafe()
	{
		ThreadSafeSingleton? instance1 = null;
		ThreadSafeSingleton? instance2 = null;

		// Act
		var thread1 = new Thread(() => instance1 = ThreadSafeSingleton.GetInstance("First"));
		var thread2 = new Thread(() => instance2 = ThreadSafeSingleton.GetInstance("Second"));

		thread1.Start();
		thread2.Start();

		thread1.Join();
		thread2.Join();

		// Assert
		Assert.Same(instance1, instance2);
	}
}


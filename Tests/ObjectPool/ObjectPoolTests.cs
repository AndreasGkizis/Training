using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;
using ObjectPool;

namespace Tests.ObjectPool;

public class ObjectPoolTests
{
    [Fact(DisplayName = "Stress Test: Verifies that the object pool handles high concurrency without crashing.")]
    public void Prove_Pool_Is_ThreadSafe_Under_Load()
    {
        // Arrange
        var provider = new DefaultObjectPoolProvider();
        var pool = provider.Create(new DefaultPooledObjectPolicy<UserSession>());

        int parallelTasks = 1_000;
        int iterationsPerTask = 1_000;

        var exception = Record.Exception(() =>
            {
                Parallel.For(0, parallelTasks, _ =>
                {
                    for (int i = 0; i < iterationsPerTask; i++)
                    {
                        var obj = pool.Get();
                        obj.UserId = 5;
                        pool.Return(obj);
                    }
                });
            }
        );
        Assert.Null(exception); // Explicitly say "There should be no errors"
    }

    // ==================================================
    // TEST 2: UNIQUENESS GUARANTEE
    // ==================================================

    [Fact]
    public void Pool_Should_Never_Give_Same_Instance_To_Two_Threads_At_Once()
    {
        // Arrange
        var provider = new DefaultObjectPoolProvider();
        var pool = provider.Create(new DefaultPooledObjectPolicy<UserSession>());

        var currentlyRented = new ConcurrentDictionary<UserSession, byte>();
        var errorFound = false;

        // Act
        Parallel.For(0, 10000, (i, state) =>
        {
            var obj = pool.Get();

            // If this object is ALREADY in our dictionary, it means 
            // another thread is currently holding it!
            if (!currentlyRented.TryAdd(obj, 0))
            {
                errorFound = true;
                state.Stop();
            }

            // Simulate work
            Thread.Yield();

            // Remove before returning
            currentlyRented.TryRemove(obj, out _);
            pool.Return(obj);
        });

        // Assert
        Assert.False(errorFound, "CRITICAL: The pool gave the same object to multiple threads simultaneously!");
    }

    // ==================================================
    // TEST 3: THE DIRTY OBJECT BUG (PROVING THE DANGER)
    // ==================================================

    [Fact]
    public void Demonstrate_Dirty_State_Leaks_Across_Threads()
    {
        // Arrange
        var provider = new DefaultObjectPoolProvider();
        // Default policy DOES NOT reset state
        var pool = provider.Create(new DefaultPooledObjectPolicy<UserSession>());

        var dirtyObjectFound = false;
        var objectLock = new object();

        // Act
        Parallel.For(0, 5000, (i) =>
        {
            var session = pool.Get();

            // CHECK: Did we get an object modified by another thread?
            if (session.SecretToken == "SECRET_DATA")
            {
                lock (objectLock) dirtyObjectFound = true;
            }

            // MODIFY: Pollute the object
            session.SecretToken = "SECRET_DATA";

            // Return without cleaning (simulating the bug)
            pool.Return(session);
        });

        // Assert
        // We EXPECT to find dirty objects. If this fails, the pool is too clean!
        Assert.True(dirtyObjectFound,
            "We expected to see leaked state, but didn't catch any. (This confirms the risk exists)");
    }

    // ==================================================
    // TEST 4: THE FIX (PROVING THE POLICY WORKS)
    // ==================================================

    [Fact]
    public void Policy_Should_Prevent_Dirty_State_Leaks()
    {
        // Arrange
        var provider = new DefaultObjectPoolProvider();
        // WE USE OUR RESET POLICY HERE
        var pool = provider.Create(new UserSessionPolicy());

        var dirtyObjectFound = false;
        var objectLock = new object();

        // Act
        Parallel.For(0, 5000, (i) =>
        {
            var session = pool.Get();

            // CHECK: This should NEVER happen now
            if (session.SecretToken == "SECRET_DATA")
            {
                lock (objectLock) dirtyObjectFound = true;
            }

            // MODIFY
            session.SecretToken = "SECRET_DATA";

            // Return (Policy automatically cleans it)
            pool.Return(session);
        });
        // Assert
        Assert.False(dirtyObjectFound, "The policy failed to clean the object! Data leaked across threads.");
    }
}
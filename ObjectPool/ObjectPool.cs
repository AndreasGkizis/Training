using Microsoft.Extensions.ObjectPool;

namespace ObjectPool;

public static class Example
{
    public static void RunDirtyObjectBugDemo()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("Problem : Dirty State Bug");
        Console.ForegroundColor = ConsoleColor.White;

        var provider = new DefaultObjectPoolProvider();
        var sessionPool = provider.Create(new DefaultPooledObjectPolicy<UserSession>());

        // User A
        var sessionA = sessionPool.Get();
        sessionA.UserId = 101;
        sessionA.IsAdmin = true; // <--- Critical State set by User A
        sessionA.SecretToken = "AdminPassword123";

        // Return to pool WITHOUT resetting state
        sessionPool.Return(sessionA);

        // User B gets assuming they have a fresh instance
        var sessionB = sessionPool.Get();

        if (sessionB.IsSanitized())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("~~> SUCCESS: Object was automatically sanitized!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("~~> FAIL: Object was NOT sanitized!");
            Console.ForegroundColor = ConsoleColor.White;
            sessionB.Print();
        }

        Console.WriteLine("End : Dirty State Bug");
    }

    public static void RunDirtyObjectBugDemoWithPolicy()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Solution to [Dirty State Bug] with Custom Policy.");
        Console.ForegroundColor = ConsoleColor.White;
        var provider = new DefaultObjectPoolProvider();
        var sessionPool = provider.Create(new UserSessionPolicy());

        // User A
        var sessionA = sessionPool.Get();
        sessionA.UserId = 101;
        sessionA.IsAdmin = true; // <--- Critical State set by User A
        sessionA.SecretToken = "AdminPassword123"; // <--- Critical State set by User A

        sessionPool.Return(sessionA); // THE POLICY AUTOMATICALLY CLEANS IT HERE.

        // User B gets assuming they have a fresh instance
        var sessionB = sessionPool.Get();

        if (sessionB.IsSanitized())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("~~> SUCCESS: Object was automatically sanitized!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("~~> FAIL: Object was NOT sanitized!");
            Console.ForegroundColor = ConsoleColor.White;
            sessionB.Print();
        }

        Console.WriteLine("End : Solution to [Dirty State Bug] with Custom Policy.");
    }

    public static void RunDirtyObjectBugDemoWith_Resettable()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("Solution to [Dirty State Bug] with IResettable.");
        Console.ForegroundColor = ConsoleColor.White;
        var provider = new DefaultObjectPoolProvider();
        var sessionPool = provider.Create(new DefaultPooledObjectPolicy<UserSessionResettable>());

        // User A
        var sessionA = sessionPool.Get();
        sessionA.UserId = 101;
        sessionA.IsAdmin = true; // <--- Critical State set by User A
        sessionA.SecretToken = "AdminPassword123"; // <--- Critical State set by User A

        // Return to pool WITHOUT resetting state
        sessionPool.Return(sessionA);

        // User B gets assuming they have a fresh instance
        var sessionB = sessionPool.Get();

        if (sessionB.IsSanitized())
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("~~> SUCCESS: Object was automatically sanitized!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("~~> FAIL: Object was NOT sanitized!");
            Console.ForegroundColor = ConsoleColor.White;
            sessionB.Print();
        }

        Console.WriteLine("End : Solution to [Dirty State Bug] with IResettable.");
    }
}

public class UserSession
{
    public int UserId { get; set; }
    public bool IsAdmin { get; set; }
    public string SecretToken { get; set; } = string.Empty;

    public bool IsSanitized() => UserId == 0 && string.IsNullOrEmpty(SecretToken) && IsAdmin == false;

    public void Print()
    {
        Console.ForegroundColor = IsSanitized() ? ConsoleColor.Green : ConsoleColor.DarkYellow;

        Console.WriteLine("-------------------------");
        Console.WriteLine($"  UserId:      {UserId}");
        Console.WriteLine($"  IsAdmin:     {IsAdmin}");
        Console.WriteLine($"  SecretToken: {SecretToken}");
        Console.WriteLine($"  Sanitized:   {IsSanitized()}");
        Console.WriteLine("-------------------------");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.White;
    }
}

public class UserSessionResettable : UserSession, IResettable
{
    public bool TryReset()
    {
        UserId = 0;
        IsAdmin = false; // Auto-reset
        SecretToken = string.Empty;
        return true; // Return true to accept back into pool
    }
}

public class UserSessionPolicy : IPooledObjectPolicy<UserSession>
{
    public UserSession Create()
    {
        return new UserSession();
    }

    // This runs AUTOMATICALLY when someone calls pool.Return(obj)
    public bool Return(UserSession obj)
    {
        // --- SANITIZATION LOGIC HERE ---
        // This runs on the thread returning the object.

        obj.UserId = 0;
        obj.IsAdmin = false; // <--- Fixes the security bug
        obj.SecretToken = string.Empty; // <--- Prevents data leaks

        // Return 'true' to say "Yes, put this back in the pool."
        // Return 'false' if the object is broken and should be discarded.
        return true;
    }
}
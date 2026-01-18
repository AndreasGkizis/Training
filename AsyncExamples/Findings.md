## TLDR: No Use in applying ConfigureAwait(false) in ASP.NET Core

## In more detail...

In non ASP.NET Classic exists the `SynchronizationContext` (more info [here](https://learn.microsoft.com/en-us/archive/msdn-magazine/2011/february/msdn-magazine-parallel-computing-it-s-all-about-the-synchronizationcontext)).
It is used to manage and propagate context-specific execution, allowing tasks or operations to run on a particular thread or context (e.g., UI thread). 
It enables framework-independent synchronization and coordination of asynchronous operations across different environments like Windows Forms, WPF, and ASP.NET.

### Why No `SynchronizationContext` in Core?

>Stepping back a moment, a good question to ask is why the AspNetSynchronizationContext was removed in ASP.NET Core. While I’m not privy to the team’s internal discussions on the subject, I assume it is for two reasons: performance and simplicity. Consider the performance aspect first.
>
>When an asynchronous handler resumes execution on legacy ASP.NET, the continuation is queued to the request context. The continuation must wait for any other continuations that have already been queued (only one may run at a time). When it is ready to run, a thread is taken from the thread pool, enters the request context, and then resumes executing the handler. That “re-entering” the request context involves a number of housekeeping tasks, such as setting HttpContext.Current and the current thread’s identity and culture.
>
>With the context-less ASP.NET Core approach, when an asynchronous handler resumes execution, a thread is taken from the thread pool and executes the continuation. The context queue is avoided, and there is no “entering” of the request context necessary. In addition, the async/await mechanism is highly optimized for the context-less scenario. There’s simply less work to do for asynchronous requests.
>
>Simplicity is another aspect of this decision. AspNetSynchronizationContext worked well, but it had some tricky parts, particularly around identity management.
>
>OK, so there’s no SynchronizationContext

### You Don’t Need ConfigureAwait(false), But Still Use It in Libraries

>Since there is no context anymore, there’s no need for ConfigureAwait(false). Any code that knows it’s running under ASP.NET Core does not need to explicitly avoid its context. In fact, the ASP.NET Core team themselves have dropped the use of ConfigureAwait(false).
>
>However, I still recommend that you use it in your core libraries - anything that may be reused in other applications. If you have code in a library that may also run in a UI app, or legacy ASP.NET app, or anywhere else there may be a context, then you should still use ConfigureAwait(false) in that library.
 
Quotes taken from [HERE](https://blog.stephencleary.com/2017/03/aspnetcore-synchronization-context.html)
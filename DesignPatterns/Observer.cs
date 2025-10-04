namespace DesignPatterns;

public class ObserverPattern
{
	#region interfaces

	public interface IObserver
	{
		public void Update(ISubject subject);
	}

	public interface ISubject
	{
		public void ChangeState(string newState);
		public string GetCurrentState();
		public void AddSubscription(IObserver sub);
		public void RemoveSubscription(IObserver sub);
		public void Notify();
	}
	#endregion

	#region Concrete Classes 
	public class YoutubeChannel : ISubject
	{
		private string _state { get; set; } = string.Empty;
		private List<IObserver> Subscribers { get; set; } = [];
		public void AddSubscription(IObserver sub)
		{
			Subscribers.Add(sub);
		}

		public void RemoveSubscription(IObserver sub)
		{
			Subscribers.Remove(sub);
		}

		public void Notify()
		{
			foreach (var sub in Subscribers)
			{
				sub.Update(this);
			}
		}

		public void ChangeState(string newState)
		{
			_state = newState;
			Notify();
		}

		public string GetCurrentState()
		{
			return _state;
		}
	}

	public class Subscriber : IObserver
	{
		public void Update(ISubject subject)
		{
			Console.WriteLine($"the new state is {subject.GetCurrentState}");
		}
	}
	#endregion
}

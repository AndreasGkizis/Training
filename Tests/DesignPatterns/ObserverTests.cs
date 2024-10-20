using DesignPatterns;
using static DesignPatterns.ObserverPattern;

namespace Tests.DesignPatterns;
public class ObserverPatternTests
{
	[Fact]
	public void Subscriber_Should_Be_Updated_When_State_Changed()
	{
		// Arrange
		var subscriber = new Mock<IObserver>();
		var youtubeChannel = new YoutubeChannel();

		youtubeChannel.AddSubscription(subscriber.Object);
		// Act

		youtubeChannel.ChangeState("NEW Video Out!");

		// Assert
		subscriber.Verify(s => s.Update(It.IsAny<ISubject>()), Times.Once);
	}
}

using Microsoft.CSharp.RuntimeBinder;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class DomainEventsTests
    {
        [Fact]
        public void SubscribeWithNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => DomainEvents.Subscribe<IEvent>(null));
        }

        [Fact]
        public void PublishWithEmptyEventMessageThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => DomainEvents.Publish<IEvent>(null));
        }

        [Fact]
        public void PublishWithNonEventTypeThrowsException()
        {
            dynamic nonEventObject = new Object();
            Assert.Throws<RuntimeBinderException>(() => DomainEvents.Publish<IEvent>(nonEventObject));
        }

        [Theory,AutoMoqData]
        public void PublishRaisesEventsToSubscriber(
            Mock<IEventHandler<TestEvent>> testHandler,
            TestEvent eventData)
        {
            DomainEvents.Subscribe(testHandler.Object);
            DomainEvents.Publish(eventData);
            testHandler.Verify(a => a.Handle(eventData), Times.Once());
        }

        [Theory, AutoMoqData]
        public void ClearAllSubscriptionsDoesNotRaiseEventsToPreviouslySubscribedClients(
            Mock<IEventHandler<TestEvent>> testHandler,
            TestEvent eventData)
        {
            DomainEvents.Subscribe(testHandler.Object);
            DomainEvents.ClearAllSubscriptions();
            DomainEvents.Publish(eventData);
            testHandler.Verify(a => a.Handle(eventData), Times.Never());
        }

        [Theory, AutoMoqData]
        public void SubscribersRegisteredAfterClearingAllSubscriptionsGetsNotifiedOfEvents(
            Mock<IEventHandler<TestEvent>> testHandler,
            TestEvent eventData)
        {
            DomainEvents.ClearAllSubscriptions();
            DomainEvents.Subscribe(testHandler.Object);
            DomainEvents.Publish(eventData);
            testHandler.Verify(a => a.Handle(eventData), Times.Once());
        }

        [Theory, AutoMoqData]
        public void PublishWithNoSubscribersDoesNotCallHandler(
           Mock<IEventHandler<TestEvent>> testHandler,
           TestEvent eventData)
        {
            DomainEvents.ClearAllSubscriptions();
            DomainEvents.Publish(eventData);
            testHandler.Verify(a => a.Handle(eventData), Times.Never());
        }

        [Theory, AutoMoqData]
        public void PublishRaisesEventsToAllSubscribers(
            IEnumerable<Mock<IEventHandler<TestEvent>>> testHandlers,
            TestEvent eventData)
        {
            foreach (var testHandler in testHandlers)
                DomainEvents.Subscribe(testHandler.Object);

            DomainEvents.Publish(eventData);

            foreach(var testHandler in testHandlers)
                testHandler.Verify(a => a.Handle(eventData), Times.Once());
        }

        [Theory,AutoMoqData]
        public void PublishAnotherEventDoesNotInvokeHandlersOfOtherEvents(
            Mock<IEventHandler<TestEvent>> testHandler,
            TestAnotherEvent anotherEventData)
        {
            DomainEvents.Subscribe(testHandler.Object);
            DomainEvents.Publish(anotherEventData);
            testHandler.Verify(a => a.Handle(It.IsAny<TestEvent>()), Times.Never());
        }
    }

    public class TestEvent : IEvent
    {
        public Guid MessageId
        {
            get
            {
                return Guid.NewGuid();
            }
        }
    }
    public class TestAnotherEvent : IEvent
    {
        public Guid MessageId
        {
            get
            {
                return Guid.NewGuid();
            }
        }
    }
}

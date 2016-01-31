namespace CommandLineApplicationLauncherModel.UnitTest
{
    public class TestDomainEventHandler<T> : IEventHandler<T> where T : IEvent
    {
        public bool EventHandlerInvoked { get; private set; }

        public T EventData { get; private set; }

        public void Handle(T eventData)
        {
            this.EventData = eventData;
            this.EventHandlerInvoked = true;
        }
    }
}

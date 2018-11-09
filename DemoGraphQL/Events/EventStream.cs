using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace DemoGraphQL.Events
{
    public class EventStream<T> : IEventStream<T>
    {
        private readonly ISubject<T> _stream = new ReplaySubject<T>(1);

        public void AddEvent(T @event)
        {
            _stream.OnNext(@event);
        }

        public IObservable<T> AsObservable()
        {
            return _stream.AsObservable();
        }
    }

    public interface IEventStream<T>
    {
        void AddEvent(T @event);
        IObservable<T> AsObservable();
    }
}

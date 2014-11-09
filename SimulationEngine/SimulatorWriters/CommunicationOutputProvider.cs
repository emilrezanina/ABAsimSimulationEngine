using System;
using System.Collections.Generic;
using SimulationEngine.Communication;

namespace SimulationEngine.SimulatorWriters
{
    public class CommunicationOutputProvider : IObservable<Message>
    {
        private readonly List<IObserver<Message>> _observers;

        public CommunicationOutputProvider()
        {
            _observers = new List<IObserver<Message>>();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<Message>> _observers;
            private readonly IObserver<Message> _observer;

            public Unsubscriber(List<IObserver<Message>> observers, IObserver<Message> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void TraceReceivedMessage(Message message)
        {
            foreach (var observer in _observers)
            {
                if (message == null)
                    observer.OnError(new ArgumentNullException());
                else
                    observer.OnNext(message);
            }
        }

        public void CommunicationEnd()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
            _observers.Clear();
        }
    }
}

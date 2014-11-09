using System;
using System.Collections.Generic;

namespace SimulationEngine.SimulatorWriters
{
    public class ActualTimeOutputProvider : IObservable<long>
    {
        private readonly List<IObserver<long>> _observers;

        public ActualTimeOutputProvider()
        {
            _observers = new List<IObserver<long>>();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<long>> _observers;
            private readonly IObserver<long> _observer;

            public Unsubscriber(List<IObserver<long>> observers, IObserver<long> observer)
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

        public IDisposable Subscribe(IObserver<long> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void ChangingActualTime(long time)
        {
            foreach (var observer in _observers)
            {
                    observer.OnNext(time);
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

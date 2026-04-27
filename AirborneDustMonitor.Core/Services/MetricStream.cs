using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Services
{
    // A MetricStream egy egyszerű implementációja az IObservable interfésznek, amely lehetővé teszi a MetricData típusú adatok publikálását és a feliratkozók értesítését.
    public class MetricStream : IObservable<MetricData>
    {
        private readonly List<IObserver<MetricData>> _observers;

        public MetricStream()
        {
            _observers = new List<IObserver<MetricData>>();
        }

        public IDisposable Subscribe(IObserver<MetricData> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<MetricData>> _observers;
            private readonly IObserver<MetricData> _observer;
            public Unsubscriber(List<IObserver<MetricData>> observers, IObserver<MetricData> observer)
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

        public void Publish(MetricData data)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(data);
            }
        }
    }
}
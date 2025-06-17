using System;

namespace Game.Event
{
    public class EventController<T>
    {
        public event Action<T> BaseAction;
        public void AddListener(Action<T> listener) => BaseAction += listener;
        public void InvokeEvent(T listener) => BaseAction?.Invoke(listener);
        public void RemoveListener(Action<T> listener) => BaseAction -= listener;
    }
}
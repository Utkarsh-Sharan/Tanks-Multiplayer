using UnityEngine;
using Game.Utilities;

namespace Game.Event
{
    public class EventService : GenericMonoSingleton<EventService>
    {
        public EventController<bool> OnPrimaryFireEvent { get; private set; }
        public EventController<Vector2> OnPlayerMoveEvent { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            InitializeEvents();
        }

        private void InitializeEvents()
        {
            OnPrimaryFireEvent = new EventController<bool>();
            OnPlayerMoveEvent = new EventController<Vector2>();
        }
    }
}
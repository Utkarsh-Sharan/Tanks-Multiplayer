using Game.Utilities;

namespace Game.Network
{
    public class HostSingleton : GenericMonoSingleton<HostSingleton>
    {
        public HostGameManager HostGameManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        public void CreateHost()
        {
            HostGameManager = new HostGameManager();
        }
    }
}
using Game.Utilities;
using System.Threading.Tasks;

namespace Game.Network
{
    public class HostSingleton : GenericMonoSingleton<HostSingleton>
    {
        private HostGameManager _hostGameManager;

        protected override void Awake()
        {
            base.Awake();
        }

        public void CreateHost()
        {
            _hostGameManager = new HostGameManager();
        }
    }
}
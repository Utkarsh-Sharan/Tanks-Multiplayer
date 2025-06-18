using Game.Utilities;
using System.Threading.Tasks;

namespace Game.Network
{
    public class ClientSingleton : GenericMonoSingleton<ClientSingleton>
    {
        private ClientGameManager _clientGameManager;

        protected override void Awake()
        {
            base.Awake();
        }

        public async Task CreateClient()
        {
            _clientGameManager = new ClientGameManager();
            await _clientGameManager.InitAsync();
        }
    }
}
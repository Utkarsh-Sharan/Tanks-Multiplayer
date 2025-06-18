using Game.Utilities;
using System.Threading.Tasks;

namespace Game.Network
{
    public class ClientSingleton : GenericMonoSingleton<ClientSingleton>
    {
        public ClientGameManager ClientGameManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        public async Task<bool> CreateClient()
        {
            ClientGameManager = new ClientGameManager();

            return await ClientGameManager.InitAsync();
        }
    }
}
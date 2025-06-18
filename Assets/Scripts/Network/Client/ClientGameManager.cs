using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine.SceneManagement;

namespace Game.Network
{
    public class ClientGameManager
    {
        public async Task<bool> InitAsync()
        {
            //Authenticate player
            await UnityServices.InitializeAsync();

            AuthState authState = await AuthenticationWrapper.DoAuth();

            if (authState == AuthState.Authenticated)
                return true;

            return false;
        }

        public void GoToMenu()
        {
            SceneManager.LoadScene(1);
        }
    }
}
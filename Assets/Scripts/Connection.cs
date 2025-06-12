using UnityEngine;
using Unity.Netcode;

namespace Game.Connection
{
    public class Connection : MonoBehaviour
    {
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
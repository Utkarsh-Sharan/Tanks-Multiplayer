using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Game.Utilities;
using Unity.Networking.Transport.Relay;

namespace Game.Network
{
    public class ClientGameManager
    {
        private JoinAllocation _allocation;

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

        public async Task StartClientAsync(string joinCode)
        {
            try
            {
                _allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
            }
            catch(Exception ex)
            {
                Debug.LogException(ex);
                return;
            }

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            RelayServerData relayServerData = new RelayServerData(_allocation, Constants.DTLS);

            transport.SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartClient();
        }
    }
}
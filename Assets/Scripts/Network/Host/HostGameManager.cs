using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Game.Utilities;

namespace Game.Network
{
    public class HostGameManager
    {
        private Allocation _allocation;
        private string _joinCode;

        public async Task StartHostAsync()
        {
            try
            {
                _allocation = await Relay.Instance.CreateAllocationAsync(Constants.MAX_CONNECTIONS);  //we create an allocation.
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return;
            }

            try
            {
                _joinCode = await Relay.Instance.GetJoinCodeAsync(_allocation.AllocationId);    //we then get the code for our allocation.
                Debug.Log(_joinCode);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return;
            }

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            RelayServerData relayServerData = new RelayServerData(_allocation, Constants.UDP);

            transport.SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.SceneManager.LoadScene(Constants.GAME_SCENE_NAME, LoadSceneMode.Single);
        }
    }
}
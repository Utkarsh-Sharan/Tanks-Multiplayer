using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Game.Network;

namespace Game.Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;
        [SerializeField] private TMP_InputField _joinCodeField;

        private void OnEnable()
        {
            _hostButton.onClick.AddListener(StartHost);
            _clientButton.onClick.AddListener(StartClient);
        }

        private async void StartHost()
        {
            await HostSingleton.Instance.HostGameManager.StartHostAsync();
        }

        private async void StartClient()
        {
            await ClientSingleton.Instance.ClientGameManager.StartClientAsync(_joinCodeField.text);
        }

        private void OnDisable()
        {
            _hostButton.onClick.RemoveListener(StartHost);
            _clientButton.onClick.RemoveListener(StartClient);
        }
    }
}
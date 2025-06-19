using UnityEngine;
using UnityEngine.UI;
using Game.Network;

namespace Game.Ui
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _hostButton;

        private void OnEnable()
        {
            _hostButton.onClick.AddListener(StartHost);
        }

        private async void StartHost()
        {
            await HostSingleton.Instance.HostGameManager.StartHostAsync();
        }

        private void OnDisable()
        {
            _hostButton.onClick.RemoveListener(StartHost);
        }
    }
}
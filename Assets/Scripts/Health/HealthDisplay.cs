using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Heal
{
    public class HealthDisplay : NetworkBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _healthBarImage;

        public override void OnNetworkSpawn()
        {
            if(!IsClient)
                return;

            _health.CurrentHealth.OnValueChanged += HandleHealthChange;
            HandleHealthChange(0, _health.CurrentHealth.Value);
        }

        private void HandleHealthChange(int oldHealth, int newHealth)
        {
            _healthBarImage.fillAmount = (float)newHealth/_health.MaxHealth;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsClient)
                return;

            _health.CurrentHealth.OnValueChanged -= HandleHealthChange;
        }
    }
}
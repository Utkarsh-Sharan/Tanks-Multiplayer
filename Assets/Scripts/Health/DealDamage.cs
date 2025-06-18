using UnityEngine;
using Game.ScriptableObj;
using Unity.Netcode;

namespace Game.Heal
{
    public class DealDamage : MonoBehaviour
    {
        [SerializeField] private ProjectileScriptableObject _projectileSO;
        private ulong _clientID;

        public void SetOwner(ulong clientID) => _clientID = clientID;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.attachedRigidbody == null)
                return;

            if (other.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject netObj))   //if we hit ourselves accidentally, return. Deal no damage.
            {
                if (_clientID == netObj.OwnerClientId)
                    return;
            }

            if (other.attachedRigidbody.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(_projectileSO.DamageAmount);
            }
        }
    }
}
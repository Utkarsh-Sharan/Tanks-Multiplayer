using Unity.Netcode;
using UnityEngine;
using Game.ScriptableObj.Input;

namespace Game.Player
{
    public class PlayerAiming : NetworkBehaviour
    {
        [SerializeField] private InputScriptableObject _inputSO;
        [SerializeField] private Transform _turretTransform;
 
        private void LateUpdate()
        {
            if (!IsOwner)
                return;

            Vector2 aimScreenPosition = _inputSO.AimPosition;
            Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(aimScreenPosition);

            _turretTransform.up = new Vector2(aimWorldPosition.x - _turretTransform.position.x, aimWorldPosition.y - _turretTransform.position.y);
        }
    }
}
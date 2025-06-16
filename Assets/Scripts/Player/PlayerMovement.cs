using Unity.Netcode;
using UnityEngine;
using Game.Event;
using Game.ScriptableObj.Input;
using Game.ScriptableObj.Player;

namespace Game.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private InputScriptableObject _inputSO;
        [SerializeField] private PlayerScriptableObject _playerSO;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Rigidbody2D _playerBody;

        private Vector2 _previousMovementInput;

        public override void OnNetworkSpawn()   //not using Start() as we wait for network to setup. Start is too early.
        {
            if (!IsOwner)
                return;

            EventService.Instance.OnPlayerMoveEvent.AddListener(HandleMovement);
        }

        private void FixedUpdate()
        {
            if (!IsOwner)
                return;

            _playerBody.velocity = (Vector2)_playerTransform.up * _previousMovementInput.y * _playerSO.MoveSpeed;
        }

        private void Update()
        {
            if (!IsOwner)
                return;

            float zRotation = _previousMovementInput.x * (-_playerSO.TurningRate) * Time.deltaTime;
            _playerTransform.Rotate(0, 0, zRotation);
        }

        private void HandleMovement(Vector2 movementInput)
        {
            _previousMovementInput = movementInput;
        } 

        public override void OnNetworkDespawn() //not using OnDestroy() as we wait for network to setup. OnDestroy is too late.
        {
            if (!IsOwner)
                return;

            EventService.Instance.OnPlayerMoveEvent.RemoveListener(HandleMovement);
        }
    }
}
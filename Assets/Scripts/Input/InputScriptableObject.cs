using UnityEngine;
using UnityEngine.InputSystem;
using Game.Event;
using static Controls;

namespace Game.Input
{
    [CreateAssetMenu(fileName = "InputScriptableObject", menuName = "ScriptableObject/InputScriptableObject")]
    public class InputScriptableObject : ScriptableObject, IPlayerActions
    {
        private Controls _controls;

        private void OnEnable()
        {
            if(_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }

            _controls.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            EventService.Instance.OnPlayerMoveEvent?.InvokeEvent(context.ReadValue<Vector2>());
        }

        public void OnPrimaryFire(InputAction.CallbackContext context)
        {
            if(context.performed)
                EventService.Instance.OnPrimaryFireEvent?.InvokeEvent(true);
            else if(context.canceled)
                EventService.Instance.OnPrimaryFireEvent?.InvokeEvent(false);
        }
    }
}
using UnityEngine;

namespace Game.ScriptableObj
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public float MoveSpeed;
        public float TurningRate;
    }
}
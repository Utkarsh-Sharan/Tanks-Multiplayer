using UnityEngine;

namespace Game.ScriptableObj
{
    [CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "ScriptableObject/ProjectileScriptableObject")]
    public class ProjectileScriptableObject : ScriptableObject
    {
        public GameObject ServerProjectilePrefab;
        public GameObject ClientProjectilePrefab;
        public float ProjectileSpeed;
        public float FireRate;
        public float MuzzleFlashDuration;
    }
}

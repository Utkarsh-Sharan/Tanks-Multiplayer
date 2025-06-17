using UnityEngine;

namespace Game.Utilities
{
    public class DestroyOnContact : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
        }
    }
}
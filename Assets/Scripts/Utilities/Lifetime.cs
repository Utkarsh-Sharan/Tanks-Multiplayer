using System.Collections;
using UnityEngine;

namespace Game.Utilities
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private Coroutine _lifeRoutine;

        private void Start()
        {
            if (_lifeRoutine == null)
                _lifeRoutine = StartCoroutine(LifeRoutine());
        }

        private IEnumerator LifeRoutine()
        {
            yield return new WaitForSeconds(_lifeTime);

            Destroy(gameObject);
        }
    }
}
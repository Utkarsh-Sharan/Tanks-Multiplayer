using Unity.Netcode;
using UnityEngine;
using Game.ScriptableObj;
using Game.Event;

namespace Game.Player
{
    public class ProjectileLauncher : NetworkBehaviour
    {
        [SerializeField] private InputScriptableObject _inputSO;
        [SerializeField] private ProjectileScriptableObject _projectileSO;
        [SerializeField] private Transform _projectileSpawnPosition;
        [SerializeField] private GameObject _muzzleFlashObject;
        [SerializeField] private Collider2D _playerCollider;

        private bool _shouldFire;
        private float _previousFireTime;
        private float _muzzleFlashTimer;

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;

            EventService.Instance.OnPrimaryFireEvent.AddListener(HandlePrimaryFire);
        }

        private void HandlePrimaryFire(bool shouldFire)
        {
            _shouldFire = shouldFire;
        }

        private void Update()
        {
            if(_muzzleFlashTimer > 0f)
            {
                _muzzleFlashTimer -= Time.deltaTime;

                if(_muzzleFlashTimer <= 0f)
                    _muzzleFlashObject.SetActive(false);
            }

            if (!IsOwner)
                return;

            if (!_shouldFire)
                return;

            if (Time.time < 1 / _projectileSO.FireRate + _previousFireTime)
                return;

            PrimaryFireServerRpc(_projectileSpawnPosition.position, _projectileSpawnPosition.up);
            SpawnDummyProjectile(_projectileSpawnPosition.position, _projectileSpawnPosition.up);

            _previousFireTime = Time.time;
        }

        [ServerRpc]
        private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
        {
            GameObject projectile = Instantiate(_projectileSO.ServerProjectilePrefab, spawnPos, Quaternion.identity);
            projectile.transform.up = direction;

            Physics2D.IgnoreCollision(_playerCollider, projectile.GetComponent<Collider2D>());

            if (projectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                rb.velocity = rb.transform.up * _projectileSO.ProjectileSpeed;

            SpawnDummyProjectileClientRpc(spawnPos, direction);
        }

        [ClientRpc]
        private void SpawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
        {
            if (IsOwner)
                return;

            SpawnDummyProjectile(spawnPos, direction);
        }

        private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
        {
            _muzzleFlashObject.SetActive(true);
            _muzzleFlashTimer = _projectileSO.MuzzleFlashDuration;

            GameObject projectile = Instantiate(_projectileSO.ClientProjectilePrefab, spawnPos, Quaternion.identity);
            projectile.transform.up = direction;

            Physics2D.IgnoreCollision(_playerCollider, projectile.GetComponent<Collider2D>());

            if(projectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                rb.velocity = rb.transform.up * _projectileSO.ProjectileSpeed;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
                return;

            EventService.Instance.OnPrimaryFireEvent.RemoveListener(HandlePrimaryFire);
        }
    }
}
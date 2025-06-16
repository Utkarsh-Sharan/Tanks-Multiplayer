using Unity.Netcode;
using UnityEngine;
using Game.ScriptableObj;
using Game.Event;
using System;

namespace Game.Player
{
    public class ProjectileLauncher : NetworkBehaviour
    {
        [SerializeField] private InputScriptableObject _inputSO;
        [SerializeField] private ProjectileScriptableObject _projectileSO;
        [SerializeField] private Transform _projectileSpawnPosition;

        private bool _shouldFire;

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
            if (!IsOwner)
                return;

            if (!_shouldFire)
                return;

            PrimaryFireServerRpc(_projectileSpawnPosition.position, _projectileSpawnPosition.up);
            SpawnDummyProjectile(_projectileSpawnPosition.position, _projectileSpawnPosition.up);
        }

        [ServerRpc]
        private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
        {
            GameObject projectile = Instantiate(_projectileSO.ServerProjectilePrefab, spawnPos, Quaternion.identity);
            projectile.transform.up = direction;

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
            GameObject projectile = Instantiate(_projectileSO.ClientProjectilePrefab, spawnPos, Quaternion.identity);
            projectile.transform.up = direction;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
                return;

            EventService.Instance.OnPrimaryFireEvent.RemoveListener(HandlePrimaryFire);
        }
    }
}
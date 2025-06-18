using Unity.Netcode;
using UnityEngine;
using Game.ScriptableObj;
using Game.Event;

namespace Game.Coin
{
    public class CoinSpawner : NetworkBehaviour
    {
        [SerializeField] private CoinScriptableObject _coinSO;

        private float _coinRadius;
        private Collider2D[] _coinBuffer = new Collider2D[1];

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
                return;

            _coinRadius = _coinSO.RespawningCoinPrefab.GetComponent<CircleCollider2D>().radius;

            for (int i = 0; i < _coinSO.MaxCoins; ++i)
            {
                SpawnCoin();
            }
        }

        private void SpawnCoin()
        {
            RespawningCoin respawningCoin = Instantiate(_coinSO.RespawningCoinPrefab, GetSpawnPoint(), Quaternion.identity);

            respawningCoin.SetValue(_coinSO.CoinValue);
            respawningCoin.GetComponent<NetworkObject>().Spawn();

            EventService.Instance.OnCoinCollectedEvent.AddListener(HandleCoinCollection);
        }

        private void HandleCoinCollection(RespawningCoin coin)
        {
            coin.transform.position = GetSpawnPoint();
            coin.Reset();
        }

        private Vector2 GetSpawnPoint()
        {
            float x = 0;
            float y = 0;

            while (true)
            {
                x = Random.Range(_coinSO.XSpawnRange.x, _coinSO.XSpawnRange.y);
                y = Random.Range(_coinSO.YSpawnRange.x, _coinSO.YSpawnRange.y);
                Vector2 spawnPosition = new Vector2(x, y);

                int numColliders = Physics2D.OverlapCircleNonAlloc(spawnPosition, _coinRadius, _coinBuffer, _coinSO.LayerMask);
                if (numColliders == 0)
                    return spawnPosition;
            }
        }
    }
}
using UnityEngine;
using Game.Coin;

namespace Game.ScriptableObj
{
    [CreateAssetMenu(fileName = "CoinScriptableObject", menuName = "ScriptableObject/CoinScriptableObject")]
    public class CoinScriptableObject : ScriptableObject
    {
        public RespawningCoin RespawningCoinPrefab;
        public int MaxCoins;
        public int CoinValue;
        public Vector2 XSpawnRange;
        public Vector2 YSpawnRange;
        public LayerMask LayerMask;
    }
}
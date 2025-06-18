using System;
using Unity.Netcode;
using UnityEngine;

namespace Game.Coin
{
    public class CoinWallet : NetworkBehaviour
    {
        public NetworkVariable<int> TotalCoins = new NetworkVariable<int>();

        public void SpendCoins(int costToFire)
        {
            TotalCoins.Value -= costToFire;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent<Coin>(out Coin coin))
            {
                int coinValue = coin.Collect();

                if (!IsServer)
                    return;

                TotalCoins.Value += coinValue;
            }
        }
    }
}
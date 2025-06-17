using Unity.Netcode;
using UnityEngine;

namespace Game.Coin
{
    public abstract class Coin : NetworkBehaviour
    {
        [SerializeField] private SpriteRenderer _coinSprite;

        protected int coinValue;
        protected bool alreadyCollected;

        public abstract int Collect();

        public void SetValue(int value) => coinValue = value;

        protected void Show(bool show) => _coinSprite.enabled = show;
    }
}
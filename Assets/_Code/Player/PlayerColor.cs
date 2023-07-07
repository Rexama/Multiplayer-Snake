using Unity.Netcode;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerColor : NetworkBehaviour
    {
        SpriteRenderer _spriteRenderer;

        public override void OnNetworkSpawn()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _spriteRenderer.color = OwnerClientId == 0 ? Color.blue : Color.red;
        }
    }
}
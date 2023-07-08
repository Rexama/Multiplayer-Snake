using System;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerTail : NetworkBehaviour
    {
        protected PlayerTail FrontTail;
        
        protected Vector3 CurrentPosition;
        protected Vector3 PrevPosition;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void PrepareTail(PlayerTail frontTail)
        {
            FrontTail = frontTail;
            CurrentPosition = FrontTail.PrevPosition;
            transform.position = CurrentPosition;
            _spriteRenderer.color = OwnerClientId == 0 ? Color.blue : Color.red;
        }

        public void MoveTail()
        {
            PrevPosition = CurrentPosition;
            CurrentPosition = FrontTail.PrevPosition;
            transform.position = CurrentPosition;
        }
    }
}
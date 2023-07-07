using Unity.Netcode;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerTail : NetworkBehaviour
    {
        protected PlayerTail FrontTail;
        
        protected Vector3 CurrentPosition;
        protected Vector3 PrevPosition;
        
        public void PrepareTail(PlayerTail frontTail)
        {
            FrontTail = frontTail;
            CurrentPosition = FrontTail.PrevPosition;
            transform.position = CurrentPosition;

        }

        public void MoveTail()
        {
            PrevPosition = CurrentPosition;
            CurrentPosition = FrontTail.PrevPosition;
            transform.position = CurrentPosition;
        }
    }
}
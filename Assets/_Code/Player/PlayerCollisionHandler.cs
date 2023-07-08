using System;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerCollisionHandler : NetworkBehaviour
    {
        private PlayerHead _playerHead;

        private void Awake()
        {
            _playerHead = GetComponent<PlayerHead>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {

            if(!(IsHost || IsServer)) return;
            
            if(col.CompareTag("PlayerTail") || col.CompareTag("PlayerHead"))
            {
                Debug.Log("PlayerDieeee");
                //_playerHead.DieClientRpc();
            }
        }
    }
}
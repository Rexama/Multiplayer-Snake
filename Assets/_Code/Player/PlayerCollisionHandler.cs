using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerCollisionHandler : MonoBehaviour
    {
        PlayerHead _playerHead;

        private void Awake()
        {
            _playerHead = GetComponent<PlayerHead>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("Enter");
            if(col.CompareTag("Food"))
            {
                Destroy(col.gameObject);
                _playerHead.AddTail();
            }
            if(col.CompareTag("PlayerTail"))
            {
                
            }
        }
    }
}
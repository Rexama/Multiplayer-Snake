﻿using System;
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

            if(col.CompareTag("PlayerTail"))
            {
                
            }
        }
    }
}
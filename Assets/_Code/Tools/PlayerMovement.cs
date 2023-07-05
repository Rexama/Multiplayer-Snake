using System;
using _Code.Player;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Network
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        
        private Vector2 _moveDirection = Vector2.right;
        private Vector2 _lastMoveDirection = Vector2.right;
        private float _nextStepMagnitude;

        private void Start()
        {
            InvokeRepeating(nameof(TakeStep), 0, playerSettings.stepTime);
        }

        private void TakeStep()
        {
            //transform.position += (Vector3)_moveDirection * playerSettings.moveSpeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W)) _moveDirection = Vector2.up;
            if (Input.GetKeyDown(KeyCode.S)) _moveDirection = Vector2.down;
            if (Input.GetKeyDown(KeyCode.A)) _moveDirection = Vector2.left;
            if (Input.GetKeyDown(KeyCode.D)) _moveDirection = Vector2.right;
        }
    }
}

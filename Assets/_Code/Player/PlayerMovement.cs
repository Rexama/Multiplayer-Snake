using System;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        
        private Grid.Grid _grid;
        private Vector2 _moveDirection = Vector2.right;
        private Vector2 _lastMoveDirection = Vector2.right;
        private float _nextStepMagnitude;
        private Vector2 currentCordiantes;

        private void Awake()
        {
            _grid = Grid.Grid.Instance;
        }

        private void Start()
        {
            PlacePlayerToRandomPoint();
            InvokeRepeating(nameof(TakeStep), 0, playerSettings.stepTime);
        }

        private void PlacePlayerToRandomPoint()
        {
            currentCordiantes = _grid.GetRandomStartCord();
            transform.position = _grid.grid[(int)currentCordiantes.y][(int)currentCordiantes.x];
        }

        private void TakeStep()
        {
            currentCordiantes = _grid.GetNextCord(_moveDirection, currentCordiantes);
            transform.position = _grid.grid[(int)currentCordiantes.y][(int)currentCordiantes.x];
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

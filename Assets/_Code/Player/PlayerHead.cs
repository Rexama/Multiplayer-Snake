using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Player
{
    public class PlayerHead : PlayerTail
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private GameObject tailPrefab;
        
        private Grid.Grid _grid;
        private Vector2 _moveDirection = Vector2.right;
        private float _nextStepMagnitude;
        private Vector2 currentCordiantes;
        private List<PlayerTail> _tails = new List<PlayerTail>() {  };

        private void Awake()
        {
            _grid = Grid.Grid.Instance;
            _tails.Add(this);
        }

        private void Start()
        {
            if (!IsOwner) return;
            
            PlacePlayerToRandomPoint();
            InvokeRepeating(nameof(TakeStep), 0, playerSettings.stepTime);
        }

        private void PlacePlayerToRandomPoint()
        {
            currentCordiantes = _grid.GetRandomCord();
            transform.position = _grid.grid[(int)currentCordiantes.y][(int)currentCordiantes.x];
            CurrentPosition = transform.position;
        }

        private void TakeStep()
        {
            currentCordiantes = _grid.GetNextCord(_moveDirection, currentCordiantes);
            PrevPosition = CurrentPosition;
            CurrentPosition = _grid.grid[(int)currentCordiantes.y][(int)currentCordiantes.x];
            transform.position = CurrentPosition;
            
            MoveTails();
        }
        
        public void AddTail()
        {
            var newTail = Instantiate(tailPrefab, transform.position, Quaternion.identity);
            newTail.GetComponent<NetworkObject>().Spawn(true);

            var tail = newTail.GetComponent<PlayerTail>();
            tail.PrepareTail(_tails[_tails.Count -1]);
            _tails.Add(tail);
        }

        public void MoveTails()
        {
            if(_tails.Count == 1) return;
            
            for(int i = 1 ; i< _tails.Count; i++)
            {
                _tails[i].MoveTail();
            }
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

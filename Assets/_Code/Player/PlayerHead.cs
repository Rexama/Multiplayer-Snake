using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Code.Player
{
    public class PlayerHead : PlayerTail
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private GameObject tailPrefab;
        
        private Grid.Grid _grid;
        private Vector2 _moveDirection;
        private NetworkVariable<float> _posX = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private NetworkVariable<float> _posY = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        private Vector2 currentCordiantes;
        private List<PlayerTail> _tails = new List<PlayerTail>() {  };

        
        private void Awake()
        {
            _grid = Grid.Grid.Instance;
            _tails.Add(this);
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                PlacePlayerToRandomPoint();
                //InvokeRepeating(nameof(TakeStep), 0, playerSettings.stepTime);
            }
        }
        
        [ClientRpc]
        public void StartPlayerMovementClientRpc()
        {
            Debug.Log("StartPlayerMovementClientRpc");
            InvokeRepeating(nameof(TakeStep), 0, playerSettings.stepTime);
        }



        private void PlacePlayerToRandomPoint()
        {
            currentCordiantes = _grid.GetRandomCord();
            transform.position = _grid.grid[(int)currentCordiantes.y][(int)currentCordiantes.x];
            CurrentPosition = transform.position;
            _posX.Value = CurrentPosition.x;
            _posY.Value = CurrentPosition.y;
        }

        private void TakeStep()
        {
            if(IsOwner)
            {
                currentCordiantes = _grid.GetNextCord(_moveDirection, currentCordiantes);
                PrevPosition = CurrentPosition;
                CurrentPosition = _grid.grid[(int) currentCordiantes.y][(int) currentCordiantes.x];
                transform.position = CurrentPosition;
                _posX.Value = CurrentPosition.x;
                _posY.Value = CurrentPosition.y;
                
            }
            else
            {
                PrevPosition = CurrentPosition;
                CurrentPosition = new Vector3(_posX.Value, _posY.Value, 0);
                transform.position = CurrentPosition;
            }
            MoveTails();
        }
        
        
        
        [ClientRpc]
        public void AddTailClientRpc(NetworkBehaviourReference tailRef)
        {
            if(tailRef.TryGet<PlayerTail>(out PlayerTail tail))
            {
                tail.PrepareTail(_tails[_tails.Count -1]);
                _tails.Add(tail);
            }
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
            if (!IsOwner) return;

            if (Input.GetKeyDown(KeyCode.W) && _moveDirection != Vector2.down) _moveDirection =Vector2.up;
            if (Input.GetKeyDown(KeyCode.S) && _moveDirection != Vector2.up) _moveDirection =Vector2.down;
            if (Input.GetKeyDown(KeyCode.A) && _moveDirection != Vector2.right) _moveDirection =Vector2.left;
            if (Input.GetKeyDown(KeyCode.D) && _moveDirection != Vector2.left) _moveDirection =Vector2.right;
        }
    }
}

using System;
using System.Collections.Generic;
using _Code.Grid;
using Unity.Netcode;
using UnityEngine;
using NetworkEvent = Unity.Networking.Transport.NetworkEvent;

namespace _Code.Player
{
    public class Head : NetworkBehaviour
    {
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private GameObject tailPrefab;
        
        private Grid.Grid _grid;
        private Vector2 _moveDirection = Vector2.right;
        private Vector2Int curCord;
        private Vector2Int prevCord;
        private List<Tail> tails;
        private CellType playerType;

        private void Awake()
        {
            playerType = IsHost ? CellType.Player1 : CellType.Player2;
            
            _grid = Grid.Grid.Instance;
        }

        private void Start()
        {
            PlacePlayerToRandomPoint();
            InvokeRepeating(nameof(TakeStep), 0, playerSettings.stepTime);
        }

        private void PlacePlayerToRandomPoint()
        {
            prevCord = curCord;
            curCord = _grid.GetRandomCord();
            transform.position = _grid.grid[curCord.y][curCord.x].Position;
            _grid.grid[curCord.y][curCord.x].CellType = playerType;
        }

        private void TakeStep()
        {
            var nextCellType = _grid.GetCellType(_moveDirection, curCord);

            if (nextCellType == CellType.Food)
            {
                OnFoodEaten();
            }
            if (nextCellType == CellType.Wall)
            {
                OnWallHit();
            }
            
                
            curCord = _grid.GetNextCord(_moveDirection, curCord);
            transform.position = _grid.grid[curCord.y][curCord.x].Position;
            _grid.UpdateGrid(prevCord, CellType.Empty );
            _grid.UpdateGrid(curCord, CellType.Player1);
        }
        
        private void OnFoodEaten()
        {
            var newTail = Instantiate(tailPrefab, _grid.grid[curCord.y][curCord.x].Position, Quaternion.identity);
            tails.Add(newTail.GetComponent<Tail>());
        }
        
        private void OnWallHit()
        {
            CancelInvoke(nameof(TakeStep));
            Debug.Log("Wall hit");
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

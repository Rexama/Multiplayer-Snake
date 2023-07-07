using System;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Grid
{
    public class FoodSpawner : NetworkBehaviour
    {
        [SerializeField] GameObject foodPrefab;
        
        private Grid _grid;

        private void Awake()
        {
            _grid = Grid.Instance;
        }

        public override void OnNetworkSpawn()
        {
            if(IsServer) SpawnFood();
        }

        private void Update()
        {
            if (!IsServer) return;
            
            if (Input.GetKeyDown(KeyCode.F)) SpawnFood();
        }

        private void SpawnFood()
        {
            var randomCord = _grid.GetRandomCord();
            var food = Instantiate(foodPrefab, _grid.grid[(int)randomCord.y][(int)randomCord.x], Quaternion.identity);
            food.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
using System;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Grid
{
    public class FoodSpawner : NetworkBehaviour
    {
        [SerializeField] GameObject foodPrefab;
        
        private Grid _grid;
        
        //singleton
        public static FoodSpawner Instance { get; private set; }
        private void OnEnable()
        {
            if (Instance != null && Instance != this) Destroy(gameObject);
            else Instance = this;
        }
        

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

        public void SpawnFood()
        {
            var randomCord = _grid.GetRandomCord();
            var food = Instantiate(foodPrefab, _grid.grid[(int)randomCord.y][(int)randomCord.x], Quaternion.identity);
            food.GetComponent<NetworkObject>().Spawn(true);
        }
    }
}
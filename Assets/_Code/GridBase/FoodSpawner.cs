using System;
using UnityEngine;

namespace _Code.Grid
{
    public class FoodSpawner : MonoBehaviour
    {
        
        private void OnEnable()
        {
            EventBus.Subscribe("OnFoodEaten", SpawnFood);
        }

        private void Start()
        {
            //SpawnFood();
        }
        
        private void SpawnFood()
        {
            var grid = Grid.Instance;
            var randomCord = grid.GetRandomCord();
            //transform.position = grid.grid[(int)randomCord.y][(int)randomCord.x];
        }
    }
}
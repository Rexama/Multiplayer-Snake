using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Code.Grid
{
    public class Grid : Singleton<Grid>
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float offset;
        [SerializeField] private GameObject obj;
        
        public List<List<Vector2>> grid = new List<List<Vector2>>();
        
        

        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            var xPos = transform.position.x;
            var yPos = transform.position.y;
            
            for (var y = yPos; y < height + yPos; y++)
            {
                var newRow = new List<Vector2>();
                for (var x = xPos; x < width + xPos; x++) 
                {
                    newRow.Add(new Vector2(x*offset, y*offset));
                    Instantiate(obj, new Vector3(x*offset, y*offset, 0), Quaternion.identity, transform);
                }
                grid.Insert(0,newRow);
            }
        }
        
        public Vector2 GetRandomStartCord()
        {
            var randomX = UnityEngine.Random.Range(0, width-1);
            var randomY = UnityEngine.Random.Range(0, height-1);
            
            Debug.Log(randomY + " " + randomX);

            Debug.Log(grid.Count);
            Debug.Log(grid[randomY][randomX]);
            
            return new Vector2(randomX, randomY);
        }

        public Vector2 GetNextCord(Vector2 direction, Vector2 cords)
        {
            if(direction == Vector2.down && cords.y < height - 1)
                return new Vector2(cords.x, cords.y + 1);
            
            if(direction == Vector2.up && cords.y > 0)
                return new Vector2(cords.x, cords.y - 1);
            
            if(direction == Vector2.left && cords.x > 0)
                return new Vector2(cords.x - 1, cords.y);
            
            if (direction == Vector2.right && cords.x < width - 1)
                return new Vector2(cords.x + 1, cords.y);
            
            else
                return Vector2.negativeInfinity;
        }
    }
}
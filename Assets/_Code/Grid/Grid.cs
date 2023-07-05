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


        private void Awake()
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
                    var newObj = Instantiate(obj, new Vector3(x*offset, y*offset, 0), Quaternion.identity, transform);
                    newObj.name = x + " " + y;
                }
                grid.Add(newRow);
            }
        }
        
        public Vector2 GetRandomCord()
        {
            var randomX = UnityEngine.Random.Range(0, width-1);
            var randomY = UnityEngine.Random.Range(0, height-1);
            
            return new Vector2(randomX, randomY);
        }

        public Vector2 GetNextCord(Vector2 direction, Vector2 cords)
        {
            if(direction == Vector2.left && cords.x > 0)
                return new Vector2(cords.x - 1, cords.y);

            if (direction == Vector2.right && cords.x < width - 1)
                return new Vector2(cords.x + 1, cords.y);
            
            if (direction == Vector2.up && cords.y < height - 1)
                return new Vector2(cords.x, cords.y + 1);
            
            if (direction == Vector2.down && cords.y > 0)
                return new Vector2(cords.x, cords.y - 1);

            else
                return GetTeleportCord(direction, cords);
        }

        private Vector2 GetTeleportCord(Vector2 direction, Vector2 cords)
        {
            if (direction == Vector2.down && cords.y == 0)
                return new Vector2(cords.x, height - 1);
            if(direction == Vector2.up && cords.y == height - 1)
                return new Vector2(cords.x, 0);
            if(direction == Vector2.left && cords.x == 0)
                return new Vector2(width - 1, cords.y);
            if(direction == Vector2.right && cords.x == width - 1)
                return new Vector2(0, cords.y);
            else
                return cords;
        }
    }
}
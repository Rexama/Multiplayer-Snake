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
        [SerializeField] private GameObject wall;
        
        public List<List<CellInfo>> grid = new List<List<CellInfo>>();
        public List<Vector2> emptyCells = new List<Vector2>();
        
        
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
                var newRow = new List<CellInfo>();
                for (var x = xPos; x < width + xPos; x++)
                {
                    CellInfo newCell;
                    if(y == yPos || x== xPos || y==height + yPos - 1  || x == width + xPos - 1)
                    {
                        newCell = new CellInfo(new Vector2(x*offset, y*offset),CellType.Wall);
                        Instantiate(wall, new Vector3(x*offset, y*offset, 0), Quaternion.identity, transform);
                    }
                    else
                    {
                        newCell = new CellInfo(new Vector2(x * offset, y * offset), CellType.Empty);
                        emptyCells.Add(new Vector2(x * offset, y * offset));
                        Instantiate(obj, new Vector3(x * offset, y * offset, 0), Quaternion.identity, transform);
                    }
                    newRow.Add(newCell);
                }
                grid.Insert(0,newRow);
            }
        }
        
        public Vector2Int GetRandomCord()
        {
            var random = UnityEngine.Random.Range(0, emptyCells.Count - 1);

            
        }
        
        public CellType GetCellType(Vector2 direction,Vector2Int cords)
        {
            var nextCords = GetNextCord(direction, cords);
            return grid[(int)nextCords.y][(int)nextCords.x].CellType;
        }

        public Vector2Int GetNextCord(Vector2 direction, Vector2Int cords)
        {
            if(direction == Vector2.down && cords.y < height - 1)
                return new Vector2Int(cords.x, cords.y + 1);
            
            if(direction == Vector2.up && cords.y > 0)
                return new Vector2Int(cords.x, cords.y - 1);
            
            if(direction == Vector2.left && cords.x > 0)
                return new Vector2Int(cords.x - 1, cords.y);
            
            if (direction == Vector2.right && cords.x < width - 1)
                return new Vector2Int(cords.x + 1, cords.y);
            
            else
                return Vector2Int.zero;
        }
        
        public void UpdateGrid(Vector2 cords, CellType cellType)
        {
            grid[(int)cords.y][(int)cords.x].CellType = cellType;
        }
    }
}
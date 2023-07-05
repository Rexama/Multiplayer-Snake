using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Code.Grid
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private int width;
        [SerializeField] private int height;
        //[SerializeField] private GameObject obj;
        
        private List<List<Vector2>> _grid = new List<List<Vector2>>();

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
                    newRow.Add(new Vector2(x, y));
                    //Instantiate(obj, new Vector3(x, y, 0), Quaternion.identity, transform);
                }
                _grid.Insert(0,newRow);
            }
        }
        
        public void GetNextStep()
    }
}
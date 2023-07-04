using UnityEngine;

namespace _Code.Grid
{
    public class CellInfo
    {
        public Vector2 Position;
        public CellType CellType;
        
        public CellInfo(Vector2 position, CellType cellType)
        {
            this.Position = position;
            this.CellType = cellType;
        }
    }
    public enum CellType
    {
        Empty,
        Wall,
        Player1,
        Player2,
        Food,
    }
}
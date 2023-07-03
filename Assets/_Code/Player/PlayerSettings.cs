using UnityEngine;

namespace _Code.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        public float stepTime;
        public float offset;
    }
}
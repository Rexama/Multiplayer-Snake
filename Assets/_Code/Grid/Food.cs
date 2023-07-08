using System;
using _Code.Game;
using _Code.Player;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace _Code.Grid
{
    public class Food : NetworkBehaviour
    {
        public GameObject tailPrefab;
        public static event Action<int> OnFoodEaten; 

        private void OnTriggerEnter2D(Collider2D col)
        {

            if(!(IsHost || IsServer)) return;
            
            if(col.CompareTag("PlayerHead"))
            {
                var newTail = Instantiate(tailPrefab, transform.position, Quaternion.identity);
                
                var tail = newTail.GetComponent<PlayerTail>();
                
                var playerTail = col.GetComponent<PlayerHead>();
                newTail.GetComponent<NetworkObject>().SpawnWithOwnership(playerTail.OwnerClientId);
                playerTail.AddTailClientRpc(tail);
                
                OnFoodEaten.Invoke((int)playerTail.OwnerClientId);
                FoodSpawner.Instance.SpawnFood();
                Destroy(gameObject);
                
            }
        }
    }
}
using _Code.Player;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Grid
{
    public class Food : NetworkBehaviour
    {
        public GameObject tailPrefab;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(IsServer || IsHost)
            if(col.CompareTag("PlayerHead"))
            {
                var newTail = Instantiate(tailPrefab, transform.position, Quaternion.identity);
                newTail.GetComponent<NetworkObject>().SpawnWithOwnership(NetworkManager.Singleton.LocalClientId);

                var tail = newTail.GetComponent<PlayerTail>();
                col.GetComponent<PlayerHead>().AddTailClientRpc(tail);
                FoodSpawner.Instance.SpawnFood();
                Destroy(gameObject);
                
            }
        }
    }
}
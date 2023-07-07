using System;
using System.Collections.Generic;
using _Code.Player;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Game
{
    public class GameManager : NetworkBehaviour
    {
        private NetworkManager _networkManager;
        private List<PlayerHead> _players = new List<PlayerHead>() {  };


        public override void OnNetworkSpawn()
        {
            Debug.Log(IsServer);
            Debug.Log(IsHost);
            
            if(IsServer || IsHost)
            {
                Debug.Log("Server");
                _networkManager = NetworkManager.Singleton;
                _networkManager.OnClientConnectedCallback += OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            Debug.Log("Client connected: " + clientId);
            _players.Add(_networkManager.ConnectedClients[clientId].PlayerObject.GetComponent<PlayerHead>());
        }

        private void Update()
        {
            if (!(IsServer || IsHost)) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                foreach (var player in _players)
                {
                    player.StartPlayerMovementClientRpc();
                }
            } 
        }
    }
}
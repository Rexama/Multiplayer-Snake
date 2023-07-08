using System;
using _Code.Grid;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace _Code.Game
{
    public class ScoreManager : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI player1Score;
        [SerializeField] private TextMeshProUGUI player2Score;

        private void OnEnable()
        {
            Food.OnFoodEaten += IncreaseScoreClientRpc;
        }

        [ClientRpc]
        public void IncreaseScoreClientRpc(int i)
        {
            Debug.Log("IncreaseScoreClientRpc");
            if (i == 0) player1Score.text = (int.Parse(player1Score.text) + 1).ToString();

            else player2Score.text = (int.Parse(player2Score.text) + 1).ToString();
            
            ResetScoreClientRpc();
        }
        
        [ClientRpc]
        public void ResetScoreClientRpc()
        {
            Debug.Log("ResetScoreClientRpc");
        }
        
    }
}
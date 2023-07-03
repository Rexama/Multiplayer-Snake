using Unity.Netcode;
using UnityEngine;

namespace Buttons
{
    public class ClientButton : ButtonObject
    {
        protected override void OnButtonPressed()
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
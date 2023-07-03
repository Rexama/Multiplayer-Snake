using Unity.Netcode;
using UnityEngine;

namespace Buttons
{
    public class ServerButton : ButtonObject
    {
        protected override void OnButtonPressed()
        {
            NetworkManager.Singleton.StartServer();
        }
    }
}
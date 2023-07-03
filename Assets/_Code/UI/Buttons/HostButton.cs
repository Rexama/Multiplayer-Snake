using Unity.Netcode;
using UnityEngine;

namespace Buttons
{
    public class HostButton : ButtonObject
    {
        protected override void OnButtonPressed()
        {
            NetworkManager.Singleton.StartHost();
        }
    }
}
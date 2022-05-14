using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LOK1game.Networking;

namespace LOK1game.Networking
{
    public class NetworkPlayer : MonoBehaviour
    {
        private void FixedUpdate()
        {
            ClientSend.PlayerMovement(transform.position, transform.rotation);
        }
    }
}
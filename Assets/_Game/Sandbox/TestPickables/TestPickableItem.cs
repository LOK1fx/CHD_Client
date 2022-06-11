using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LOK1game;
using LOK1game.Player;

public class TestPickableItem : MonoBehaviour, IPickable
{
    public void OnPick(Pawn sender)
    {
        if(sender.TryGetComponent<Player>(out var player))
        {
            player.Camera.TriggerRecoil(new Vector3(15, 15, 15));
        }
    }
}
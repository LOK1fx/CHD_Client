using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct InteractionData
{
    public object Sender { get; set; }
    public Vector3 InteractionOrigin { get; set; }

    public InteractionData(object sender, Vector3 interactionOrigin)
    {
        Sender = sender;
        InteractionOrigin = interactionOrigin;
    }
}
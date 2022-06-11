using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace LOK1game.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private float _interactionMaxDistance = 1f;
        [SerializeField] private Transform _interactionPivotTransform;
        [SerializeField] private LayerMask _usableMask;

        public bool TryInteract(Player player)
        {
            if(Physics.Raycast(_interactionPivotTransform.position, _interactionPivotTransform.forward, out var hit, _interactionMaxDistance, _usableMask, QueryTriggerInteraction.Ignore))
            {
                if(hit.collider.gameObject.TryGetComponent<IUsable>(out var usable))
                {
                    usable.Use(player);

                    return true;
                }
            }

            return false;
        }
    }
}

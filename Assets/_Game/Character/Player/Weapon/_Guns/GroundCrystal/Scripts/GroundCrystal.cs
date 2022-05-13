using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Weapon
{
    public class GroundCrystal : MonoBehaviour
    {
        public void Activate(float activeTime)
        {
            StartCoroutine(ActivateRoutine(activeTime));
        }

        private IEnumerator ActivateRoutine(float activeTime)
        {
            yield return new WaitForSeconds(activeTime);

            Destroy(gameObject);
        }
    }

}
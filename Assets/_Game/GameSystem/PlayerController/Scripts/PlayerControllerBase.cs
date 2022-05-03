using UnityEngine;

namespace LOK1game
{
    public class PlayerControllerBase : MonoBehaviour
    {
        public IPawn ControlledPawn { get; private set; }

        private void Update()
        {
            if(ControlledPawn != null)
            {
                ControlledPawn.OnInput(this);
            }
        }

        public void SetControlledPawn(IPawn pawn)
        {
            ControlledPawn = pawn;
            ControlledPawn.OnPocces(this);
        }
    }
}
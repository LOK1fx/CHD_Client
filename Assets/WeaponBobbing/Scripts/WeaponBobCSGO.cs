using LOK1game;
using UnityEngine;

namespace WeaponBobbing
{
	public class WeaponBobCSGO : WeaponBobBase, IPawn
    {
		new private const float INTERNAL_MULTIPLIER = 6.0f;
		private const float FRONT_BOB_MULTIPLIER = 100;
		private float frontBobMovementAmount = 0.0f;

        [Header("CSGO Style Bob Settings")]
		[Range(0f, 0.1f)] public float maxFrontBobMovement = 0.05f;
		[HideInInspector] public float frontBobbingSpeed = 1.0f;
		[Range(0.05f, 0.3f)] public float sideBobbingSpeed = 0.15f;
		[Range(0.02f, 0.04f)] public float sideBobbingAmount = 0.02f;

        private float horizontal;
        private float vertical;
        private float aHorz;
        private float aVert;

        private bool movingX;
        private bool movingY;

        private void Update() {
			if((movingX && aVert > 0) || (movingY && aHorz > 0)) {
				var moveSpeed = Mathf.Clamp(aVert + aHorz, 0, 1) * frontBobbingSpeed * FRONT_BOB_MULTIPLIER;

				frontBobMovementAmount += moveSpeed;
			}
			else {
				frontBobMovementAmount -= frontBobbingSpeed * FRONT_BOB_MULTIPLIER;

				if(frontBobMovementAmount < 0) {
					frontBobMovementAmount = 0;
				}
			}

			frontBobMovementAmount *= Time.deltaTime;

			var xMovement = 0.0f;
			var yMovement = 0.0f;

			var calcPosition = initPos;

			if (aHorz == 0 && aVert == 0) {
				timer = 0.0f;
			}
			else {
				xMovement = Mathf.Sin(timer);
				yMovement = -Mathf.Abs(Mathf.Abs(xMovement) - 1);

				timer += sideBobbingSpeed;
				
				if (timer > Mathf.PI * 2) {
					timer = timer - (Mathf.PI * 2);
				}
			}

			var totalMovement = Mathf.Clamp(aVert + aHorz, 0, 1);

			if (xMovement != 0) {
				xMovement = xMovement * totalMovement;
				calcPosition.x = initPos.x + xMovement * sideBobbingAmount;
			}
			else {
				calcPosition.x = initPos.x;
			}

			if (yMovement != 0) {
				yMovement = yMovement * totalMovement;
				calcPosition.y = initPos.y + yMovement * sideBobbingAmount * 2;
			}
			else {
				calcPosition.y = initPos.y;
			}

			var totalFrontX = Mathf.Clamp(frontBobMovementAmount, -maxFrontBobMovement, maxFrontBobMovement);
			var totalFrontY = Mathf.Clamp(frontBobMovementAmount, -maxFrontBobMovement, maxFrontBobMovement);
			var totalFrontZ = Mathf.Clamp(frontBobMovementAmount, -maxFrontBobMovement, maxFrontBobMovement);

			calcPosition.x += totalFrontX;
			calcPosition.y -= totalFrontY;
			calcPosition.z -= totalFrontZ;

            calcPosition.z = Mathf.Clamp(calcPosition.z, -maxFrontBobMovement, maxFrontBobMovement);

			transform.localPosition = Vector3.Lerp(transform.localPosition, calcPosition, Time.deltaTime * INTERNAL_MULTIPLIER * bobMultiplier);
		}

        public void OnInput(object sender)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            aHorz = Mathf.Abs(horizontal);
            aVert = Mathf.Abs(vertical);

            movingX = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
            movingY = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        }

        public void OnPocces(PlayerControllerBase sender)
        {
            
        }
    }
}
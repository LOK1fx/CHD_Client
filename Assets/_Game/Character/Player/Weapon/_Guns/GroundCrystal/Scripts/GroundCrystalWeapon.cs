using UnityEngine;

namespace LOK1game.Weapon
{
    public class GroundCrystalWeapon : BaseWeapon
    {
        [SerializeField] private float _activeTime = 3f;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private GroundCrystal _crystalPrefab;
        [SerializeField] private GameObject _crystalPreviewPrefab;

        private GameObject _currentCrystalPreview;

        private RaycastHit _hit;
        private Player.Player _player;

        private void LateUpdate()
        {
            if(_currentCrystalPreview != null)
            {
                var camera = _player.Camera.GetRecoilCameraTransform();
                var hasHit = Raycast(camera);

                if (hasHit)
                {
                    _currentCrystalPreview.transform.position = _hit.point;
                }

                _currentCrystalPreview.SetActive(hasHit);
            }
        }

        protected override void Equip(Player.Player player)
        {
            _player = player;

            _currentCrystalPreview = Instantiate(_crystalPreviewPrefab);
        }

        protected override void Atack(Player.Player player, PlayerHand hand)
        {
            var camera = player.Camera.GetRecoilCameraTransform();

            player.Camera.AddCameraOffset(camera.forward * data.ShootCameraOffset.z);

            if(Raycast(camera))
            {
                var crystal = Instantiate(_crystalPrefab, _hit.point, Quaternion.identity);

                crystal.Activate(_activeTime);
            }
        }

        private bool Raycast(Transform camera)
        {
            if (Physics.Raycast(camera.position, camera.forward, out _hit, data.ShootDistance, _groundMask, QueryTriggerInteraction.Ignore))
            {
                return true;
            }

            return false;
        }

        protected override void Dequip(Player.Player player)
        {
            if(_currentCrystalPreview != null)
            {
                Destroy(_currentCrystalPreview);
            }
        }
    }
}
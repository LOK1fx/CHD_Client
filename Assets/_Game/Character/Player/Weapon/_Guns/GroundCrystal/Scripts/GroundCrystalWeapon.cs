using System.Collections;
using System.Collections.Generic;
using LOK1game.Player;
using UnityEngine;

namespace LOK1game.Weapon
{
    public class GroundCrystalWeapon : BaseGun
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
                var camera = _player.PlayerCamera.GetRecoilCameraTransform();

                if (Raycast(camera))
                {
                    _currentCrystalPreview.transform.position = _hit.point;
                }
            }
        }

        public override void Equip(Player.Player player)
        {
            _player = player;

            _currentCrystalPreview = Instantiate(_crystalPreviewPrefab);
        }

        public override void Shoot(Player.Player player, PlayerHand hand)
        {
            var camera = _player.PlayerCamera.GetRecoilCameraTransform();

            if(Raycast(camera))
            {
                var crystal = Instantiate(_crystalPrefab, _hit.point, Quaternion.identity);

                crystal.Activate(_activeTime);
            }
        }

        public override void UpdateAds(Player.Player player)
        {
        }

        private bool Raycast(Transform camera)
        {
            if (Physics.Raycast(camera.position, camera.forward, out _hit, data.ShootDistance, _groundMask, QueryTriggerInteraction.Ignore))
            {
                return true;
            }

            return false;
        }
    }
}
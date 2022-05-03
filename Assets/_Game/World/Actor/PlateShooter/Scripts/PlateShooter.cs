using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.World
{
    public class PlateShooter : Actor
    {
        [SerializeField] private TrainingPlate _platePrefab;

        [Space]
        [SerializeField] private float _launchForce = 70f;
        [SerializeField] private float _fireRate;

        private float _timeToNextShoot;

        [Space]
        [SerializeField] private Transform _firePoint;

        private void Update()
        {
            if (Time.time > _timeToNextShoot)
            {
                _timeToNextShoot = Time.time + 1f / _fireRate;

                Shoot();
            }
        }

        public void Shoot()
        {
            var plate = Instantiate(_platePrefab, _firePoint.position, _firePoint.rotation);

            plate.Shoot(_firePoint.forward, _launchForce);
        }
    }
}
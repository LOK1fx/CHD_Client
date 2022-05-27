using LOK1game.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.Weapon
{
    [RequireComponent(typeof(BaseWeapon), typeof(AudioSource))]
    public class WeaponAudio : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _shootClips = new List<AudioClip>();

        private AudioSource _source;
        private BaseWeapon _weapon;

        private void Awake()
        {
            _weapon = GetComponent<BaseWeapon>();
            _source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _weapon.OnWeaponAttack += OnWeaponAttack;
        }

        private void OnWeaponAttack()
        {
            _source.PlayOneShot(Audio.GetRandomClip(_shootClips.ToArray()));
        }
    }
}
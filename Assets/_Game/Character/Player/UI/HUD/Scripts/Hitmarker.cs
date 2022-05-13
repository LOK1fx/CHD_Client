using UnityEngine;
using LOK1game.Game.Events;
using LOK1game.New.Networking;

namespace LOK1game.UI
{
    //Note: v. 2021.3.1f1 unity animation controller isn't working
    //Setting up the animator controller is impossible
    [RequireComponent(typeof(Animation))] // [RequireComponent(typeof(Animator))]
    public class Hitmarker : MonoBehaviour
    {
        private const string TRIGGER_ON_HIT = "Hit";
        private const string TRIGGER_ON_CRIT_HIT = "CritHit";

        [SerializeField] private float _maxRotationAngleOnHit = 5f;

        private Animation _animator; //Animator

        private void Awake()
        {
            _animator = GetComponent<Animation>(); //Animator
        }

        private void Start()
        {
            EventManager.AddListener<OnPlayerHitCHD>(OnHit);
        }

        private void OnHit(OnPlayerHitCHD evt)
        {
            if(NetworkManager.Instance != null)
            {
                if (NetworkManager.Instance.Client.Id == evt.PlayerId) { return; }
            }

            var angle = Random.Range(-_maxRotationAngleOnHit, _maxRotationAngleOnHit);

            transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            if (evt.Crit)
            {
                //Note: v. 2021.3.1f1 unity animation controller isn't working
                //Setting up the animator controller is impossible
                //_animator.SetTrigger(TRIGGER_ON_CRIT_HIT);
            }
            else
            {
                _animator.Stop();
                _animator.Play();
            }
        }
    }
}
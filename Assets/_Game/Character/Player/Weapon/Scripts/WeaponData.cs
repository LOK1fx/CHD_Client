using UnityEngine;

namespace LOK1game.Weapon
{
    public enum EWeaponType : ushort
    {
        Primary = 1,
        Secondary,
        Utility,
        Ability,
    }

    public enum EGunBurstMode
    {
        Semi,
        Auto,
        Burst
    }

    [CreateAssetMenu(fileName = "new WeapomData", menuName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public EWeaponId Id => _id;
        public EWeaponType Type => _type;

        [SerializeField] private EWeaponId _id = EWeaponId.None;
        [SerializeField] private EWeaponType _type = EWeaponType.Primary;

        [Space]
        [SerializeField] private KeyCode _useKey = KeyCode.Mouse0;
        [SerializeField] private KeyCode _altUseKey = KeyCode.Mouse1;

        public KeyCode UseKey => _useKey;
        public KeyCode AltUseKey => _altUseKey;

        [Space]
        [SerializeField] private int _damage = 20;
        [SerializeField] private float _startBulletForce = 80f;
        [SerializeField] private int _bulletsPerShoot = 6;
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private float _shootDistance = 1000f;
        [SerializeField] private float _adsSpeed = 8f;
        [SerializeField] private bool _shootsFromMuzzle = true;

        public int Damage => _damage;
        public float StartBulletForce => _startBulletForce;
        public int BulletsPerShoot => _bulletsPerShoot;
        public float FireRate => _fireRate;
        public float ShootDistance => _shootDistance;
        public float AdsSpeed => _adsSpeed;
        public bool ShootsFromMuzzle => _shootsFromMuzzle;

        public EGunBurstMode BurstMode = EGunBurstMode.Semi;
        public PlayerHand.Side Hand = PlayerHand.Side.Right;

        [Space]
        [SerializeField] private float _bloom = 25;

        [Range(0.1f, 25f)]
        [SerializeField] private float _bloomXMultiplier = 1f;
        [Range(0.1f, 25f)]
        [SerializeField] private float _bloomYMultiplier = 1f;

        public float Bloom => _bloom;

        public float BloomXMultiplier => _bloomXMultiplier;
        public float BloomYMultiplier => _bloomYMultiplier;

        [Header("Recoil")]
        [SerializeField] private float _shootFovChange = 0.4f;
        [SerializeField] private Vector3 _shootCameraOffset;
        [SerializeField] private Vector3 _recoilCameraRotation;

        public float ShootFovChange => _shootFovChange;
        public Vector3 ShootCameraOffset => _shootCameraOffset;
        public Vector3 RecoilCameraRotation => _recoilCameraRotation;

        [Space]
        [SerializeField] private AnimatorOverrideController _animatorController;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private BaseWeapon _gunPrefab;
        [SerializeField] private GameObject _shellPrefab;

        public AnimatorOverrideController AnimatorController => _animatorController;
        public Projectile ProjectilePrefab => _projectilePrefab;
        public BaseWeapon GunPrefab => _gunPrefab;
        public GameObject ShellPrefab => _shellPrefab;

#if UNITY_EDITOR

        [ContextMenu("FindAndSetPrefab")]
        public void Editor_FindAndSetPrefab()
        {
            _gunPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>($"{Constants.Editor.WEAPON_PREFAB_PATH}/{name}/Prefabs/{name}.prefab").GetComponent<BaseWeapon>();
        }

        public void Editor_SetData(EWeaponType type, BaseWeapon prefab)
        {
            _gunPrefab = prefab;
            _type = type;
        }

#endif
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    [RequireComponent(typeof(Duck))]
    public class DuckMovement : MonoBehaviour
    {
        public UnityAction<DestinationPoint> OnChangeDistanationPoint;

        public DuckMovementZone CurrentMovementZone { get; private set; }
        public DestinationPoint CurrentDestinationPoint { get; private set; }

        public Vector3 Velocity { get; private set; }

        private Vector3 _oldPosition;
        private Vector3 _actualPosition;

        [Range(0f, 35f)]
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _rotateSpeed = 2f;
        [SerializeField] private float _achivePointThreshold = 0.1f;

        private Duck _duck;

        private void Awake()
        {
            _duck = GetComponent<Duck>();
        }

        private void Update()
        {
            if(CurrentMovementZone == null) { return; }

            if(CurrentDestinationPoint == null)
            {
                SetDistanation(GetRandomPoint());
            }

            if(Vector3.Distance(transform.position, CurrentDestinationPoint.Position) < _achivePointThreshold)
            {
                SetDistanation(GetRandomPoint());
            }

            var point = CurrentDestinationPoint;
            var lookRotation = Quaternion.LookRotation(-point.Position);

            _oldPosition = transform.position;

            var position = Vector3.MoveTowards(transform.position, point.Position, Time.deltaTime * _moveSpeed);

            transform.position = position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * _rotateSpeed);

            _actualPosition = transform.position;

            Velocity = _actualPosition - _oldPosition;
        }

        public void SetDistanation(DestinationPoint point)
        {
            if(point.IsFull())
            {
                throw new System.Exception("Point is full!");
            }

            CurrentDestinationPoint = point;

            OnChangeDistanationPoint?.Invoke(CurrentDestinationPoint);
        }

        public void SetMovementZone(DuckMovementZone zone)
        {
            CurrentMovementZone = zone;

            var point = zone.GetRandomPoint();

            zone.TryToSetPositionToPoint(_duck, point);         
        }

        private DestinationPoint GetRandomPoint()
        {
            var point = CurrentMovementZone.GetRandomPoint();

            if(point.IsFull() || point == CurrentDestinationPoint)
            {
                return GetRandomPoint();
            }

            return point;
        }

        private Vector3 MovementLerp(Vector3 current, Vector3 target, float time)
        {
            var x = Lerp(current.x, target.x, time);
            var y = Lerp(current.y, target.y, time);
            var z = Lerp(current.z, target.z, time);

            return new Vector3(x, y, z);
        }

        private float Lerp(float current, float target, float time)
        {
            return current + time * (target - current);
        }
    }
}
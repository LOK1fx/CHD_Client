using System.Collections.Generic;
using UnityEngine;

namespace LOK1game.New.Networking
{
    public class Interpolator : MonoBehaviour
    {
        [SerializeField] private float _movementThreshold = 0.05f;

        [SerializeField] private float _timeElapsed = 0f;
        [SerializeField] private float _timeToReachTarget = 0.05f;

        private readonly List<TransformUpdate> _futureTransformUpdates = new List<TransformUpdate>();

        private float _squareMovementThreshold;
        private TransformUpdate _to;
        private TransformUpdate _from;
        private TransformUpdate _previous;

        private void Start()
        {
            _squareMovementThreshold = _movementThreshold * _movementThreshold;
            _to = new TransformUpdate(NetworkManager.Instance.ServerTick, false, transform.position);
            _from = new TransformUpdate(NetworkManager.Instance.InterpolationTick, false, transform.position);
            _previous = new TransformUpdate(NetworkManager.Instance.InterpolationTick, false, transform.position);
        }

        private void Update()
        {
            for (int i = 0; i < _futureTransformUpdates.Count; i++)
            {
                if (NetworkManager.Instance.ServerTick >= _futureTransformUpdates[i].Tick)
                {
                    if (_futureTransformUpdates[i].IsTeleport)
                    {
                        _to = _futureTransformUpdates[i];
                        _from = _to;
                        _previous = _to;

                        transform.position = _to.Position;
                    }
                    else
                    {
                        _previous = _to;
                        _to = _futureTransformUpdates[i];
                        _from = new TransformUpdate(NetworkManager.Instance.InterpolationTick, false, transform.position);
                    }

                    _futureTransformUpdates.RemoveAt(i);
                    i--;
                    _timeElapsed = 0f;
                    _timeToReachTarget = (_to.Tick - _from.Tick) * Time.fixedDeltaTime;
                }
            }

            _timeElapsed += Time.deltaTime;

            InterpolatePosition(_timeElapsed / _timeToReachTarget);
        }

        private void InterpolatePosition(float lerpAmount)
        {
            if ((_to.Position - _previous.Position).sqrMagnitude < _squareMovementThreshold)
            {
                if (_to.Position != _from.Position)
                {
                    transform.position = Vector3.Lerp(_from.Position, _to.Position, lerpAmount);
                }

                return;
            }

            transform.position = Vector3.Lerp(_from.Position, _to.Position, lerpAmount);
        }

        public void NewUpdate(ushort tick, bool isTeleport, Vector3 position)
        {
            if (tick <= NetworkManager.Instance.InterpolationTick && !isTeleport)
            {
                return;
            } 

            for (int i = 0; i < _futureTransformUpdates.Count; i++)
            {
                if (tick < _futureTransformUpdates[i].Tick)
                {
                    _futureTransformUpdates.Insert(i, new TransformUpdate(tick, isTeleport, position));

                    return;
                }
            }

            _futureTransformUpdates.Add(new TransformUpdate(tick, isTeleport, position));
        }
    }
}
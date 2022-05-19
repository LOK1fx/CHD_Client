using UnityEngine;
using RiptideNetworking;
using LOK1game.Player;

namespace LOK1game.New.Networking
{
    public class NetworkPlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

        private PlayerMovement _movement;

        private bool[] _inputs;

        //ClientPrediction
        private StatePayload[] _stateBuffer;
        private InputPayload[] _inputBuffer;
        private StatePayload _latestServerState;
        private StatePayload _lastProcessedState;

        private Vector3 _position;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
        }

        private void Start()
        {
            _inputs = new bool[5];
            _stateBuffer = new StatePayload[Constants.Network.CLIENT_INPUT_BUFFER_SIZE];
            _inputBuffer = new InputPayload[Constants.Network.CLIENT_INPUT_BUFFER_SIZE];

            _movement.OnMovementProcessed += OnMovementProcessed;
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.W))
            {
                _inputs[0] = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                _inputs[1] = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                _inputs[2] = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _inputs[3] = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _inputs[4] = true;
            }
        }

        private void FixedUpdate()
        {
            var inputAxis = new Vector2();

            if(_inputs[0])
            {
                inputAxis.y += 1;
            }
            if (_inputs[1])
            {
                inputAxis.x -= 1;
            }
            if (_inputs[2])
            {
                inputAxis.y -= 1;
            }
            if (_inputs[3])
            {
                inputAxis.x += 1;
            }

            if(!_latestServerState.Equals(default(StatePayload)) &&
                (_lastProcessedState.Equals(default(StatePayload)) ||
                !_latestServerState.Equals(_lastProcessedState)))
            {
                HandleServerReconciliation();
            }

            var bufferIndex = NetworkManager.Instance.ServerTick % Constants.Network.CLIENT_INPUT_BUFFER_SIZE;

            var inputPayload = new InputPayload
            {
                Tick = NetworkManager.Instance.ServerTick,
                Input = inputAxis.normalized
            };

            _inputBuffer[bufferIndex] = inputPayload;
            _stateBuffer[bufferIndex] = ProcessMovement(inputPayload);

            SendInput(inputPayload);

            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = false;
            }
        }

        public void OnLand(Vector3 velocity, bool local)
        {
            if(local)
            {
                
            }
            else
            {

            }
        }

        private void HandleServerReconciliation()
        {
            _lastProcessedState = _latestServerState;

            var serverStateBufferIndex = _latestServerState.Tick % Constants.Network.CLIENT_INPUT_BUFFER_SIZE;
            var positionError = Vector3.Distance(_latestServerState.Position, _stateBuffer[serverStateBufferIndex].Position);

            if(positionError > 0.001f)
            {
                Debug.Log("HandleServerReconciliation.");

                transform.position = _latestServerState.Position;

                _stateBuffer[serverStateBufferIndex] = _latestServerState;

                var tickToProcess = _latestServerState.Tick + 1;

                while(tickToProcess < NetworkManager.Instance.ServerTick)
                {
                    var statePayload = ProcessMovement(_inputBuffer[tickToProcess]);
                    var bufferIndex = tickToProcess % Constants.Network.CLIENT_INPUT_BUFFER_SIZE;

                    _stateBuffer[bufferIndex] = statePayload;

                    tickToProcess++;
                }
            }
        }

        private void OnMovementProcessed()
        {
            _position = transform.position;
        }

        public void OnServerMovementState(StatePayload serverState)
        {
            _latestServerState = serverState;
        }

        private StatePayload ProcessMovement(InputPayload input)
        {
            _movement.SetAxisInput(input.Input);

            return new StatePayload()
            {
                Tick = input.Tick,
                Position = _position
            };
        }

        #region Messages

        private void SendInput(InputPayload input)
        {
            var message = Message.Create(MessageSendMode.unreliable, EClientToServerId.Input);

            message.AddBools(_inputs, false);
            message.AddVector2(input.Input);
            message.AddUShort(input.Tick);
            message.AddVector3(_cameraTransform.forward);

            NetworkManager.Instance.Client.Send(message);
        }

        #endregion
    }

}
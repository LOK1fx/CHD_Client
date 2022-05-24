using UnityEngine;

namespace LOK1game
{
    public abstract class PopupText : MonoBehaviour
    {
        private const string PATH = "PopupText";

        [SerializeField] protected float _disappearSpeed = 3f;
        [SerializeField] private Vector3 _textMoveDirection;
        [SerializeField] private float _textMoveSpeed = 15f;

        protected PopupTextParams _params;
        protected Vector3 _offset;
        protected float _alpha = 1;

        protected float _disappearTimer;

        public static PopupText Spawn<T>(Vector3 position, Transform parent, PopupTextParams textParams) where T : PopupText
        {
            return BaseSpawn<T>(position, parent, textParams);
        }

        public static PopupText Spawn<T>(Vector3 position, PopupTextParams textParams) where T : PopupText
        {
            return BaseSpawn<T>(position, null, textParams);
        }

        private static PopupText BaseSpawn<T>(Vector3 position, Transform parent, PopupTextParams textParams) where T : PopupText
        {
            var go = Instantiate(Resources.Load<T>(PATH), position, Quaternion.identity, parent);

            go.Show(textParams);

            return go;
        }

        public abstract void Show(PopupTextParams textParams);

        protected void UpdateOffset()
        {
            _offset += (_textMoveDirection.normalized * _textMoveSpeed) * Time.deltaTime;
        }

        protected void UpdateAlpha()
        {
            _disappearTimer -= Time.deltaTime;
            if (_disappearTimer < 0)
            {
                _alpha -= _disappearSpeed * Time.deltaTime;

                if (_alpha < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public struct PopupTextParams
    {
        public string Text { get; private set; }
        public Color TextColor { get; private set; }
        public float DisappearTime { get; private set; }

        public PopupTextParams(string text, float disappearTime)
        {
            Text = text;
            TextColor = Color.white;
            DisappearTime = disappearTime;
        }

        public PopupTextParams(string text, Color color)
        {
            Text = text;
            TextColor = color;
            DisappearTime = 2f;
        }

        public PopupTextParams(string text, float disappearTime, Color color)
        {
            Text = text;
            TextColor = color;
            DisappearTime = disappearTime;
        }
    }
}
using UnityEngine;
using LOK1game.Game.Events;
using LOK1game.UI;
using System.Collections.Generic;

namespace LOK1game
{
    public class PopupTextTester : MonoBehaviour
    {
        private List<PopupText> _spawnedTexts = new List<PopupText>();
        private Vector3 _spawnedTextInitialPosition;

        private void Awake()
        {
            EventManager.AddListener<OnPlayerHitCHDEvent>(OnHit);
        }

        private void LateUpdate()
        {
            if(_spawnedTexts.Count > 0)
            {
                var position = Camera.main.WorldToScreenPoint(_spawnedTextInitialPosition);

                foreach (var text in _spawnedTexts)
                {
                    text.SetPosition(position);
                }
            }
        }

        private void OnHit(OnPlayerHitCHDEvent evt)
        {
            var textParams = new PopupTextParams(evt.Damage.ToString(), 1f, Color.white);

            //PopupText.Spawn<PopupText3D>(evt.HitPosition, textParams);
            _spawnedTexts.Add(PopupText.Spawn<PopupText2D>(Camera.main.WorldToScreenPoint(evt.HitPosition), PlayerHud.Instance.transform, textParams));
            _spawnedTextInitialPosition = evt.HitPosition;
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<OnPlayerHitCHDEvent>(OnHit);
        }
    }
}
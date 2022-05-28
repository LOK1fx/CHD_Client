using System.Collections.Generic;
using UnityEngine;
using LOK1game.Game.Events;

namespace LOK1game.UI
{
    public class FarmCrystalScorePopups : MonoBehaviour
    {
        private List<PopupText2D> _popupTexts = new List<PopupText2D>();
        private List<Vector3> _popupTextsPositions = new List<Vector3>();

        private void Awake()
        {
            EventManager.AddListener<OnFarmCrystalCHDEvent>(OnHit);
        }

        private void LateUpdate()
        {
            if (_popupTexts.Count > 0)
            {
                var camera = Camera.main;

                for (int i = 0; i < _popupTexts.Count; i++)
                {
                    var position = _popupTextsPositions[i];

                    _popupTexts[i].SetPosition(camera.WorldToScreenPoint(position));
                }
            }
        }

        private void OnHit(OnFarmCrystalCHDEvent evt)
        {
            var position = Camera.main.WorldToScreenPoint(evt.HitPosition);
            var textParams = new PopupTextParams($"+{evt.Score}", 1f, Color.white);
            var text = PopupText.Spawn<PopupText2D>(position, PlayerHud.Instance.transform, textParams) as PopupText2D;

            _popupTexts.Add(text);
            _popupTextsPositions.Add(evt.HitPosition);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<OnFarmCrystalCHDEvent>(OnHit);
        }
    }
}
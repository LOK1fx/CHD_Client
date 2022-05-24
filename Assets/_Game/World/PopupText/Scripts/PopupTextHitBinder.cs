using UnityEngine;
using LOK1game.Game.Events;
using LOK1game.UI;

namespace LOK1game
{
    public class PopupTextHitBinder : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.AddListener<OnPlayerHitCHD>(OnHit);
        }

        private void OnHit(OnPlayerHitCHD evt)
        {
            var textParams = new PopupTextParams(evt.Damage.ToString(), 1f, Color.white);

            //PopupText.Spawn<PopupText3D>(evt.HitPosition, textParams);
            var text = PopupText.Spawn<PopupText2D>(Camera.main.WorldToScreenPoint(evt.HitPosition), PlayerHud.Instance.transform, textParams);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener<OnPlayerHitCHD>(OnHit);
        }
    }
}
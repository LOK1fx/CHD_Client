using UnityEngine;

namespace LOK1game.Game.Events
{
    public static class Events
    {
        public static OnPlayerHitCHD OnPlayerHit = new OnPlayerHitCHD();
        public static OnFarmCrystalCHD OnFarmCrystalCHD = new OnFarmCrystalCHD();
    }

    public class OnPlayerHitCHD : GameEvent
    {
        public ushort PlayerId;
        public Vector3 HitPosition;
        public bool Crit;
        public int Damage;
    }

    public class OnFarmCrystalCHD : GameEvent
    {
        public int Score;
        public Vector3 HitPosition;
    }
}
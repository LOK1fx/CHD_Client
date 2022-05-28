using UnityEngine;

namespace LOK1game.Game.Events
{
    public static class Events
    {
        public static OnGameStateChangedEvent OnGameStateChanged = new OnGameStateChangedEvent();
        public static OnPlayerHitCHDEvent OnPlayerHit = new OnPlayerHitCHDEvent();
        public static OnFarmCrystalCHDEvent OnFarmCrystalCHD = new OnFarmCrystalCHDEvent();
    }

    public class OnGameStateChangedEvent : GameEvent
    {
        public EGameState PreviousState;
        public EGameState NewGameState;
    }

    public class OnPlayerHitCHDEvent : GameEvent
    {
        public ushort PlayerId;
        public Vector3 HitPosition;
        public bool Crit;
        public int Damage;
    }

    public class OnFarmCrystalCHDEvent : GameEvent
    {
        public int Score;
        public Vector3 HitPosition;
    }
}
using UnityEngine;

namespace LOK1game.Game.Events
{
    public static class Events
    {
        public static OnLaptopUseEvent OnLaptopUseEvent = new OnLaptopUseEvent();
        public static OnCameraPoccesEvent OnCameraPoccesEvent = new OnCameraPoccesEvent();
        public static OnMaskUseEvent OnMaskUseEvent = new OnMaskUseEvent();
        public static OnAnimatronicChangeRoomEvent OnAnimatronicChangeRoom = new OnAnimatronicChangeRoomEvent();
        public static OnPlayerHitCHD OnPlayerHit = new OnPlayerHitCHD();
    }

    public class OnPlayerHitCHD : GameEvent
    {
        public ushort PlayerId;
        public Vector3 HitPosition;
        public bool Crit;
        public int Damage;
    }

    public class OnAnimatronicChangeRoomEvent : GameEvent
    {
        public int PreviousRoomId;
        public int NewRoomId;
    }

    public class OnLaptopUseEvent : GameEvent
    {
        public bool IsOpen;
    }

    public class OnCameraPoccesEvent : GameEvent
    {
        public int Id;
    }

    public class OnMaskUseEvent : GameEvent
    {
        public bool Dress;
    }
}
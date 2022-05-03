using UnityEngine;

namespace LOK1game
{
    public struct Damage
    {
        public enum Type
        {
            Normal,
            Lazer,
            Void,
            Hit
        }

        public Actor Sender { get; private set; }
        public Type DamageType { get; private set; }
        public int Value { get; private set; }

        public Vector3 HitPoint { get; set; }
        public Vector3 HitNormal { get; set; }

        public Damage(int value)
        {
            Sender = null;
            Value = value;
            DamageType = Type.Normal;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Damage(int value, Type type)
        {
            Sender = null;
            Value = value;
            DamageType = type;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Damage(int value, Type type, Actor sender)
        {
            Sender = sender;
            Value = value;
            DamageType = type;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }

        public Damage(int value, Actor sender)
        {
            Sender = sender;
            Value = value;
            DamageType = Type.Normal;

            HitPoint = Vector3.zero;
            HitNormal = Vector3.zero;
        }
    }
}
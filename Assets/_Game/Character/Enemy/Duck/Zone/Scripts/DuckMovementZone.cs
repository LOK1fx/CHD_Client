using System.Collections.Generic;
using UnityEngine;

namespace LOK1game
{
    public class DuckMovementZone : MonoBehaviour
    {
        public List<DestinationPoint> Points = new List<DestinationPoint>();

        public DestinationPoint GetRandomPoint()
        {
            if(Points == null)
            {
                Debug.Log("Points is null");
            }

            var point = Points[Random.Range(0, Points.Count)];

            Debug.Log(point.ToString());

            if(point.IsFull())
            {
                return GetRandomPoint();
            }
            else
            {
                return point;
            }   
        }

        public bool TryToSetPositionToPoint(Actor actor, DestinationPoint point)
        {
            if(point.IsFull())
            {
                return false;
            }

            point.AddActor(actor);
            actor.transform.position = point.Position;

            return true;
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var point in Points)
            {
                Gizmos.DrawCube(point.Position, Vector3.one);
            }

            var bounds = new Bounds(Points[0].Position, Vector3.zero);

            for (int i = 0; i < Points.Count; i++)
            {
                bounds.Encapsulate(Points[i].Position);
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }

    [System.Serializable]
    public class DestinationPoint
    {
        public List<Actor> ActorsOnPoint = new List<Actor>();
        public int MaxActorsAtPoint = 1;
        public bool OnGround;

        public Vector3 Position;

        public DestinationPoint(int maxActorsAtPoint, Vector3 position)
        {
            MaxActorsAtPoint = maxActorsAtPoint;
            Position = position;

            ActorsOnPoint = new List<Actor>();
        }

        public void AddActor(Actor actor)
        {
            if(IsFull())
            {
                throw new System.Exception("DestinationPoint is full!");
            }

            ActorsOnPoint.Add(actor);
        }

        public void RemoveActor(Actor actor)
        {
            ActorsOnPoint.Remove(actor);
        }

        public bool IsFull()
        {
            if(ActorsOnPoint.Count >= MaxActorsAtPoint)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Point at {Position}\nHolds{MaxActorsAtPoint}";
        }
    }
}
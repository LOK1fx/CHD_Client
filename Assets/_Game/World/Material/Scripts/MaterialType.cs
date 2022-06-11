using UnityEngine;

/// <summary>
/// Defines a type of a world object/actor
/// </summary>
public enum EMaterialType
{
    Default,
    Concrete,
    Glass,
    Grass,
    Meat,
    Metal,
    Water,
}

[RequireComponent(typeof(Collider))]
public class MaterialType : MonoBehaviour
{
    public EMaterialType Material => _material;

    [SerializeField] private EMaterialType _material;
}
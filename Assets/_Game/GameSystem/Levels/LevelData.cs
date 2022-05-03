using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new LevelData", menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public Sprite LevelImage;
    public int BuildIndex;
    public string Name;
    public bool IsCompleted;
}
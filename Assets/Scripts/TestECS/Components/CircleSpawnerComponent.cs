using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct CircleSpawner : ISharedComponentData
{
    public GameObject projectile;
    [Range(0,999)]
    public int projectileNumber;
    [Range(0,360)]
    public float angleStep;
    public float projectileSpeed;
    public bool isSpawnTimed;
    public float spawnInterval;
    public CircleParameter spawnParameter;
}

public class CircleSpawnerComponent : SharedComponentDataWrapper<CircleSpawner> { }
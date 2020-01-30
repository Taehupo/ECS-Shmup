using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct Lifetime : IComponentData
{
    public float lifeTime;
    public float lifeTimer;
}

public class LifetimeComponent : ComponentDataWrapper<Lifetime> { }
using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct Velocity : IComponentData
{
    public float3 velocity;
}

public class VelocityComponent : ComponentDataWrapper<Velocity>
{
   
}

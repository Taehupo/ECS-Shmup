using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class VelocitySystem : JobComponentSystem
{
    [BurstCompile]
    struct VelocityJob : IJobProcessComponentData<Position, Velocity>
    {
        [ReadOnly] public float dT;

        public void Execute(ref Position position, [ReadOnly] ref Velocity velocity)
        {
            position.Value += velocity.velocity*dT;
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new VelocityJob()
        {
            dT = Time.deltaTime
        };

        return job.Schedule(this, inputDependencies);
    }
}

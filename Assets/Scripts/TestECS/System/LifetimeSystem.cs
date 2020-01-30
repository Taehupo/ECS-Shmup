using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class JobBarrier : BarrierSystem { }

public class LifetimeSystem : JobComponentSystem
{
    [Inject] JobBarrier barrier;

    [BurstCompile]
    struct LifeTimeJob : IJobProcessComponentDataWithEntity<Lifetime>
    {
        [ReadOnly] public float dT;
        [ReadOnly] public EntityCommandBuffer Cmd;

        public void Execute(Entity entity, int id, ref Lifetime life)
        {
            life.lifeTimer += dT;

            if (life.lifeTimer > life.lifeTime)
            {
                Cmd.DestroyEntity(entity);
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new LifeTimeJob()
        {
            dT = Time.deltaTime,
            Cmd = barrier.CreateCommandBuffer()
        };

        return job.Schedule(this, inputDependencies);
    }
}

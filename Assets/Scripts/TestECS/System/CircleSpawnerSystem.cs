using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CircleSpawnerSystem : ComponentSystem
{
    ComponentGroup m_Spawners;
    float timer;

    protected override void OnCreateManager()
    {
        m_Spawners = GetComponentGroup(typeof(CircleSpawner), typeof(Position));
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;
        using (var spawners = m_Spawners.ToEntityArray(Allocator.TempJob))
        {
            foreach (var spawner in spawners)
            {
                var Data = EntityManager.GetSharedComponentData<CircleSpawner>(spawner);
                if (Data.isSpawnTimed && Data.spawnInterval < timer)
                {
                    switch (Data.spawnParameter)
                    {
                        case CircleParameter.DEFAULT:
                            CircleSpawnNonEqualDistribution(spawner, Data);
                            break;
                        case CircleParameter.EQUALDISTRIBUTION:
                            CircleSpawnEqualDistribution(spawner, Data);
                            break;
                        case CircleParameter.ANGLERANGE:
                            break;
                        case CircleParameter.MOVINGANGLERANGE:
                            break;
                        case CircleParameter.PROGRESSIVECIRCLE:
                            break;
                    }
                    timer = 0.0f;
                }
                else if(!Data.isSpawnTimed)
                {
                    switch (Data.spawnParameter)
                    {
                        case CircleParameter.DEFAULT:
                            CircleSpawnNonEqualDistribution(spawner, Data);
                            break;
                        case CircleParameter.EQUALDISTRIBUTION:
                            CircleSpawnEqualDistribution(spawner, Data);
                            break;
                        case CircleParameter.ANGLERANGE:
                            break;
                        case CircleParameter.MOVINGANGLERANGE:
                            break;
                        case CircleParameter.PROGRESSIVECIRCLE:
                            break;
                    }
                }
                
            }
        }
    }

    void CircleSpawnNonEqualDistribution(Entity spawner, CircleSpawner Data)
    {
        int projNum = 0;
        for (float i = 0.0f; i < 360.0f && projNum < Data.projectileNumber; i += Data.angleStep)
        {
            var projectile = EntityManager.GetSharedComponentData<CircleSpawner>(spawner).projectile;
            var entity = EntityManager.Instantiate(projectile);

            var position = EntityManager.GetComponentData<Position>(spawner);
            float3 newPos = position.Value;
            newPos.x += math.cos(i * Mathf.Deg2Rad);
            newPos.y += math.sin(i * Mathf.Deg2Rad);

            float3 velocity = (newPos - position.Value) * Data.projectileSpeed;

            Position newPosition;
            Velocity newVel;
            newVel.velocity = velocity;
            newPosition.Value = newPos;

            EntityManager.SetComponentData(entity, newPosition);
            EntityManager.SetComponentData(entity, newVel);
            projNum++;
        }
    }

    void CircleSpawnEqualDistribution(Entity spawner, CircleSpawner Data)
    {
        for (float i = 0.0f; i < 360.0f ; i += 360.0f/Data.projectileNumber)
        {
            var projectile = EntityManager.GetSharedComponentData<CircleSpawner>(spawner).projectile;
            var entity = EntityManager.Instantiate(projectile);

            var position = EntityManager.GetComponentData<Position>(spawner);
            float3 newPos = position.Value;
            newPos.x += math.cos(i * Mathf.Deg2Rad);
            newPos.y += math.sin(i * Mathf.Deg2Rad);

            float3 velocity = (newPos - position.Value) * Data.projectileSpeed;

            Position newPosition;
            Velocity newVel;
            newVel.velocity = velocity;
            newPosition.Value = newPos;

            EntityManager.SetComponentData(entity, newPosition);
            EntityManager.SetComponentData(entity, newVel);
        }
    }
}

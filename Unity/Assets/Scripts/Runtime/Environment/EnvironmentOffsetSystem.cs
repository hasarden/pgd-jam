using Runtime.Difficulty.Components;
using Runtime.Environment.Components;
using Runtime.Environment.Configuration;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Runtime.Environment
{
    public class EnvironmentOffsetSystem : JobComponentSystem
    {
        protected override void OnCreate()
        {
            var entity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(entity, new EnvironmentOffsetComponent());
            
            RequireSingletonForUpdate<EnvironmentConfiguration>();
            RequireSingletonForUpdate<EnvironmentOffsetComponent>();
            
            RequireSingletonForUpdate<DifficultyStateComponent>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var singleton = GetSingleton<EnvironmentOffsetComponent>();
            var configuration = GetSingleton<EnvironmentConfiguration>();
            var difficulty = GetSingleton<DifficultyStateComponent>();

            singleton.AdditionalOffset += Time.DeltaTime * configuration.Speed * difficulty.Value;
            singleton.AdditionalOffset = math.modf(singleton.AdditionalOffset, out var i);
            singleton.Offset += (int) i;
            
            SetSingleton(singleton);
            
            return inputDeps;
        }
    }
}
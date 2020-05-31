using Runtime.Environment.Components;
using Runtime.Environment.Configuration;
using Runtime.Obstacles.Configuration;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Runtime.Obstacles
{
    public class ObstacleMotionSystem : JobComponentSystem
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<EnvironmentOffsetComponent>();
            RequireSingletonForUpdate<EnvironmentConfiguration>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var environment = GetSingleton<EnvironmentOffsetComponent>();
            var configuration = GetSingleton<EnvironmentConfiguration>();
            
            return Entities
                .ForEach((ref ObstacleComponent component, ref Translation translation) =>
                {
                    var delta = component.Offset - environment.Offset;
                    translation.Value = new float3(
                        delta - environment.AdditionalOffset,
                        component.Ceil ? configuration.CeilHeight : 0.0f,
                        0.0f
                    );
                })
                .Schedule(inputDeps);
        }
    }
}

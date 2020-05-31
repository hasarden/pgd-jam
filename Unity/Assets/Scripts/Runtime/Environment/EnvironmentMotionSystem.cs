using Runtime.Environment.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Runtime.Environment
{
    public class EnvironmentMotionSystem : JobComponentSystem
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<EnvironmentOffsetComponent>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var singleton = GetSingleton<EnvironmentOffsetComponent>();

            return Entities
                .ForEach((ref EnvironmentBlockComponent blockComponent, ref Translation translation) =>
                {
                    var delta = blockComponent.Offset - singleton.Offset;
                    translation.Value = new float3(
                        delta - singleton.AdditionalOffset,
                        0.0f,
                        0.0f
                    );
                })
                .Schedule(inputDeps);
        }
    }
}
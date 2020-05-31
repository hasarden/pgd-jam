using Runtime.Difficulty.Components;
using Runtime.Player.Components;
using Runtime.Player.Configuration;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Runtime.Player
{
    public class PlayerAnimationSystem : JobComponentSystem
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<PlayerConfiguration>();
            RequireSingletonForUpdate<DifficultyStateComponent>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var configuration = GetSingleton<PlayerConfiguration>();
            var speed = configuration.AnimationSpeed;

            var step = math.fmod(Time.ElapsedTime, speed * 2.0f);
            var factor = step > speed
                ? 1.0f - (step - speed) / speed
                : step / speed;
            
            var one = new float3(1.0f, 1.0f, 1.0f);
            var scale = math.lerp(
                one,
                one * configuration.AnimationScale,
                (float)factor
            );

            return Entities
                .WithAll<TagPlayer>()
                .WithNone<JumpStateComponent>()
                .ForEach((ref NonUniformScale scaleComponent, ref PlayerOrientationComponent orientation) =>
                {
                    var entryScale = scale;
                    if (!orientation.Grounded)
                        entryScale.y = -entryScale.y;
                    
                    scaleComponent.Value = entryScale;
                })
                .Schedule(inputDeps);
        }
    }
}
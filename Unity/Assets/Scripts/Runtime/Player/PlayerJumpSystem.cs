using Runtime.Difficulty.Components;
using Runtime.Environment.Configuration;
using Runtime.Player.Components;
using Runtime.Player.Configuration;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Runtime.Player
{
    public class PlayerJumpSystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<PlayerConfiguration>();
            RequireSingletonForUpdate<EnvironmentConfiguration>();
        }

        protected override void OnUpdate()
        {
            var difficulty = GetSingleton<DifficultyStateComponent>();
            var environment = GetSingleton<EnvironmentConfiguration>();
            var configuration = GetSingleton<PlayerConfiguration>();
            var jumpDuration = configuration.JumpDuration / difficulty.Value;
            var deltaTime = Time.DeltaTime;

            Entities
                .WithAll<TagPlayer>()
                .ForEach((
                    Entity entity,
                    ref JumpStateComponent jumpState,
                    ref Translation translation,
                    ref NonUniformScale scale,
                    ref PlayerOrientationComponent orientation
                ) =>
                {
                    jumpState.Duration += deltaTime;

                    if (jumpState.StartScale.x < float.Epsilon)
                        jumpState.StartScale = scale.Value;

                    if (jumpState.Duration < jumpDuration)
                    {
                        // update scale
                        var middleScale = configuration.JumpScale;
                        if (!orientation.Grounded)
                            middleScale.y = -middleScale.y;

                        if (jumpState.Duration < jumpDuration / 2.0f)
                        {
                            var factor = jumpState.Duration / jumpDuration * 2.0f;
                            scale.Value = math.lerp(jumpState.StartScale, middleScale, factor);
                        }
                        else
                        {
                            var targetScale = orientation.Grounded
                                ? new float3(1.0f, -1.0f, 1.0f)
                                : new float3(1.0f, 1.0f, 1.0f);

                            var factor = jumpState.Duration / jumpDuration * 2.0f - 1.0f;
                            scale.Value = math.lerp(middleScale, targetScale, factor);
                        }

                        // update motion
                        var motionFactor = jumpState.Duration / jumpDuration;
                        var adjustedFactor = orientation.Grounded
                            ? motionFactor
                            : 1.0f - motionFactor;

                        var position = translation.Value;
                        position.y = math.lerp(0.0f, environment.CeilHeight, adjustedFactor);
                        translation.Value = position;
                    }
                    else
                    {
                        PostUpdateCommands.RemoveComponent<JumpStateComponent>(entity);

                        orientation.Grounded = !orientation.Grounded;

                        var position = translation.Value;
                        position.y = orientation.Grounded ? 0.0f : environment.CeilHeight;
                        translation.Value = position;

                        scale.Value = orientation.Grounded
                            ? new float3(1.0f, 1.0f, 1.0f)
                            : new float3(1.0f, -1.0f, 1.0f);
                    }
                });
        }
    }
}
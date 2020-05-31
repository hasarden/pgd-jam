using Runtime.Difficulty.Components;
using Runtime.Environment.Components;
using Runtime.Loop.Components;
using Runtime.Obstacles.Configuration;
using Runtime.Player.Components;
using Runtime.UI.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Runtime.Loop
{
    public class LoopSystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            var entity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(entity, new RestartEventComponent());
            
            RequireSingletonForUpdate<RestartEventComponent>();
            RequireSingletonForUpdate<EnvironmentOffsetComponent>();
            RequireSingletonForUpdate<LoadingStateComponent>();
        }

        protected override void OnUpdate()
        {
            var restart = GetSingleton<RestartEventComponent>();
            if (!restart.Triggered)
                return;

            var offset = GetSingleton<EnvironmentOffsetComponent>();
            offset.Offset = 0;
            offset.AdditionalOffset = 0.0f;
            SetSingleton(offset);

            var loading = GetSingleton<LoadingStateComponent>();
            loading.Duration = 0.0f;
            SetSingleton(loading);

            var obstacles = GetSingleton<ObstacleSpawnState>();
            obstacles.LastObstacleOffset = 0;
            SetSingleton(obstacles);

            var difficulty = GetSingleton<DifficultyStateComponent>();
            difficulty.Reset = true;
            SetSingleton(difficulty);
            
            Entities
                .WithAll<EnvironmentBlockComponent>()
                .ForEach(entity => PostUpdateCommands.DestroyEntity(entity));

            Entities
                .WithAll<ObstacleComponent>()
                .ForEach(entity => PostUpdateCommands.DestroyEntity(entity));
            
            Entities
                .ForEach((Entity entity, ref JumpStateComponent jumpState) =>
                {
                    PostUpdateCommands.RemoveComponent(entity, ComponentType.ReadWrite<JumpStateComponent>());
                });
            
            Entities.ForEach((ref PlayerOrientationComponent orientation) => { orientation.Grounded = true; });
            
            Entities
                .WithAll<TagPlayer>()
                .ForEach((ref Translation translation, ref Rotation rotation, ref NonUniformScale scale) =>
                {
                    translation.Value = float3.zero;
                    rotation.Value = quaternion.identity;
                    scale.Value = new float3(1.0f, 1.0f, 1.0f);
                });

            restart.Triggered = false;
            SetSingleton(restart);
        }
    }
}
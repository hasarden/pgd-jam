using Runtime.Loop.Components;
using Runtime.Player.Components;
using Runtime.Player.Configuration;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny;
using Unity.Transforms;
using Unity.U2D.Entities.Physics;

namespace Runtime.Player
{
    public class PlayerObstacleDetectionSystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            RequireSingletonForUpdate<RestartEventComponent>();
            RequireSingletonForUpdate<PlayerConfiguration>();
        }

        protected override void OnUpdate()
        {
            var physicsWorldSystem = World.GetExistingSystem<PhysicsWorldSystem>();
            var physicsWorld = physicsWorldSystem.PhysicsWorld;
            
            var configuration = GetSingleton<PlayerConfiguration>();
            var restartEvent = GetSingleton<RestartEventComponent>();
            
            Entities
                .WithAll<TagPlayer>()
                .WithNone<JumpStateComponent>()
                .ForEach((ref PlayerOrientationComponent orientation, ref Translation translation) =>
                {
                    var filter = new CollisionFilter
                    {
                        BelongsTo = ~0u,
                        CollidesWith = ~0u
                    };
                    
                    var distance = new float2(configuration.RayDistance, 0.0f);
                    var offset = new float2(0.0f, 0.5f);
                    offset.y *= orientation.Grounded ? 1.0f : -1.0f;

                    var forward = new RaycastInput
                    {
                        Start = translation.Value.xy + offset,
                        End = translation.Value.xy + offset + distance,
                        Filter = filter
                    };

                    var backward = new RaycastInput
                    {
                        Start = translation.Value.xy + offset,
                        End = translation.Value.xy + offset - distance,
                        Filter = filter
                    };

                    if (physicsWorld.CastRay(forward) || physicsWorld.CastRay(backward))
                        restartEvent.Triggered = true;
                });

            SetSingleton(restartEvent);
        }
    }
}
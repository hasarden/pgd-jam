using Runtime.Difficulty.Components;
using Runtime.Environment.Components;
using Runtime.Obstacles.Configuration;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Runtime.Obstacles
{
    public class ObstacleSpawnSystem : ComponentSystem
    {
        private Random _random;
        
        protected override void OnCreate()
        {
            var entity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(entity, new ObstacleSpawnState());
            
            RequireSingletonForUpdate<ObstacleSpawnState>();
            RequireSingletonForUpdate<EnvironmentOffsetComponent>();
            RequireSingletonForUpdate<ObstacleConfiguration>();
            
            _random = new Random(1);
        }

        protected override void OnUpdate()
        {
            var state = GetSingleton<ObstacleSpawnState>();
            var environment = GetSingleton<EnvironmentOffsetComponent>();
            var configuration = GetSingleton<ObstacleConfiguration>();
            var difficulty = GetSingleton<DifficultyStateComponent>();
            
            var offscreenOffset = environment.Offset + configuration.OffscreenOffset;
            var lastSpawnOffsetDelta = offscreenOffset - state.LastObstacleOffset;
            var obstaclesPeriod = configuration.SpawnPeriod - difficulty.Value * configuration.SpawnPeriodDifficultyFactor;
            
            if (lastSpawnOffsetDelta < obstaclesPeriod)
                return;

            var configurationEntity = GetSingletonEntity<ObstacleConfiguration>();
            var obstacleVariants = EntityManager.GetBuffer<ObstacleBufferEntry>(configurationEntity);
            var source = obstacleVariants[_random.NextInt(obstacleVariants.Length)];

            var ceil = _random.NextBool();
            if (ceil == state.LastObstacleOrientation)
            {
                var switchChance = 0.2f * difficulty.Value;
                if (_random.NextFloat() < switchChance)
                    ceil = !ceil;
            }
            
            var entity = PostUpdateCommands.Instantiate(source.Entity);
            PostUpdateCommands.AddComponent(entity, new ObstacleComponent {Offset = offscreenOffset, Ceil = ceil});
            PostUpdateCommands.SetComponent(
                entity,
                new Rotation()
                {
                    Value = ceil 
                        ? quaternion.Euler(0.0f, 0.0f, math.PI)
                        : quaternion.identity
                });

            state.LastObstacleOrientation = ceil;
            state.LastObstacleOffset = offscreenOffset;
            SetSingleton(state);
        }
    }
}

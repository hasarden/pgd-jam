using Runtime.Environment.Components;
using Runtime.Environment.Configuration;
using Unity.Entities;
using Unity.Mathematics;

namespace Runtime.Environment
{
    public class EnvironmentSpawnSystem : ComponentSystem
    {
        private Random _random;
        
        protected override void OnCreate()
        {
            _random = new Random(1);
            
            RequireSingletonForUpdate<EnvironmentConfiguration>();
            RequireSingletonForUpdate<EnvironmentOffsetComponent>();
        }

        protected override void OnUpdate()
        {
            var singleton = GetSingleton<EnvironmentOffsetComponent>();
            var configuration = GetSingleton<EnvironmentConfiguration>();
            var configurationEntity = GetSingletonEntity<EnvironmentConfiguration>();
            var blockVariants = EntityManager.GetBuffer<EnvironmentBlockBufferEntry>(configurationEntity);
            
            var forward = 0;
            var backward = 0;

            var requiredForward = singleton.Offset + configuration.UnitsRequestedForward;
            var requiredBackward = singleton.Offset - configuration.UnitsRequestedBackward;
            
            Entities.ForEach((Entity entity, ref EnvironmentBlockComponent block) =>
            {
                forward = math.max(forward, block.Offset + block.Size);
                backward = math.min(backward, block.Offset);
                
                if (block.Offset + block.Size < requiredBackward)
                    PostUpdateCommands.DestroyEntity(entity);
            });

            while (requiredForward > forward)
                forward += SpawnBlock(forward, false);

            while (requiredBackward < backward)
                backward -= SpawnBlock(backward, true);

            int SpawnBlock(int offset, bool offsetBySize)
            {
                var variant = blockVariants[_random.NextInt(blockVariants.Length)];
                
                var entity = PostUpdateCommands.Instantiate(variant.Entity);
                var spawnAtOffset = offsetBySize
                    ? offset - variant.Size
                    : offset;
                
                PostUpdateCommands.SetComponent(entity, new EnvironmentBlockComponent(variant.Size) {Offset = spawnAtOffset});

                return variant.Size;
            }
        }
    }
}
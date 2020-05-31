using Unity.Entities;

namespace Runtime.Obstacles.Configuration
{
    public struct ObstacleBufferEntry : IBufferElementData
    {
        public Entity Entity;

        public ObstacleBufferEntry(Entity entity)
        {
            Entity = entity;
        }
    }
}
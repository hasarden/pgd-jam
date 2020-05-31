using Unity.Entities;

namespace Runtime.Environment.Components
{
    public struct EnvironmentBlockBufferEntry : IBufferElementData
    {
        public Entity Entity;
        public int Size;

        public EnvironmentBlockBufferEntry(Entity entity, int size)
        {
            Entity = entity;
            Size = size;
        }
    }
}
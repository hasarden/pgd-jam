using Unity.Entities;

namespace Runtime.Environment.Components
{
    public struct EnvironmentBlockComponent : IComponentData
    {
        public int Offset;
        public int Size;

        public EnvironmentBlockComponent(int size)
        {
            Size = size;
            Offset = 0;
        }
    }
}
using Unity.Entities;

namespace Runtime.Environment.Components
{
    public struct EnvironmentOffsetComponent : IComponentData
    {
        public int Offset;
        public float AdditionalOffset;
    }
}
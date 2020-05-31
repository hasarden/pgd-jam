using Unity.Entities;

namespace Runtime.Loop.Components
{
    public struct RestartEventComponent : IComponentData
    {
        public bool Triggered;
    }
}
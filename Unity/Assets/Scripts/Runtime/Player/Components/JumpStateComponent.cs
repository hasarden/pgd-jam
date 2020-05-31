using Unity.Entities;
using Unity.Mathematics;

namespace Runtime.Player.Components
{
    public struct JumpStateComponent : IComponentData
    {
        public float3 StartScale;
        
        public float Duration;
    }
}
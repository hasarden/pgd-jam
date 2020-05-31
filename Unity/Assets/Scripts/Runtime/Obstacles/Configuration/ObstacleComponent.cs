using Unity.Entities;

namespace Runtime.Obstacles.Configuration
{
    public struct ObstacleComponent : IComponentData
    {
        public int Offset;
        public bool Ceil;
    }
}
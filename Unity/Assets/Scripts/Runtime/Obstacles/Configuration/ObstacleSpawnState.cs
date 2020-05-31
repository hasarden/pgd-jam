using Unity.Entities;

namespace Runtime.Obstacles.Configuration
{
    public struct ObstacleSpawnState : IComponentData
    {
        public int LastObstacleOffset;
        public bool LastObstacleOrientation;
    }
}
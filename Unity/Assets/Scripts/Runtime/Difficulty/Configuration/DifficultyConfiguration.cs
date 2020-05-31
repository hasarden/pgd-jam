using Unity.Entities;

namespace Runtime.Difficulty.Configuration
{
    public struct DifficultyConfiguration : IComponentData
    {
        public float Speed;

        public DifficultyConfiguration(float speed)
        {
            Speed = speed;
        }
    }
}
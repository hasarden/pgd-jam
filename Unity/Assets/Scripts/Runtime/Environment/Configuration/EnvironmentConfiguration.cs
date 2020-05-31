using Unity.Entities;

namespace Runtime.Environment.Configuration
{
    public struct EnvironmentConfiguration : IComponentData
    {
        public float Speed;

        public int UnitsRequestedForward;
        public int UnitsRequestedBackward;
        
        public float CeilHeight;

        public EnvironmentConfiguration(
            float speed, 
            int unitsRequestedForward, 
            int unitsRequestedBackward, 
            float ceilHeight
        )
        {
            Speed = speed;
            UnitsRequestedForward = unitsRequestedForward;
            UnitsRequestedBackward = unitsRequestedBackward;
            CeilHeight = ceilHeight;
        }
    }
}
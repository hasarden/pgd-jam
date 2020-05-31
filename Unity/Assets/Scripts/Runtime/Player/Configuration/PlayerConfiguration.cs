using Unity.Entities;
using Unity.Mathematics;

namespace Runtime.Player.Configuration
{
    public struct PlayerConfiguration : IComponentData
    {
        public float AnimationSpeed;
        public float3 AnimationScale;

        public float JumpDuration;
        public float3 JumpScale;

        public float RayDistance;
        
        public PlayerConfiguration(
            float animationSpeed, 
            float3 animationScale, 
            float jumpDuration,
            float3 jumpScale, 
            float rayDistance
        )
        {
            AnimationSpeed = animationSpeed;
            AnimationScale = animationScale;
            JumpDuration = jumpDuration;
            JumpScale = jumpScale;
            RayDistance = rayDistance;
        }
    }
}
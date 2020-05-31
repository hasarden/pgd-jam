using Unity.Entities;

namespace Runtime.Player.Components
{
    public struct PlayerOrientationComponent : IComponentData
    {
        public bool Grounded;

        public PlayerOrientationComponent(bool grounded)
        {
            Grounded = grounded;
        }
    }
}
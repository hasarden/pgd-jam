using Unity.Entities;

namespace Runtime.UI.Components
{
    public struct LoadingStateComponent : IComponentData
    {
        public float Duration;
    }
}
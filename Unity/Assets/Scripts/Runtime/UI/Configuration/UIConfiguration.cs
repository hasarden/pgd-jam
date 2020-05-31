using Unity.Entities;

namespace Runtime.UI.Configuration
{
    public struct UIConfiguration : IComponentData
    {
        public float LoadingStableDuration;
        public float LoadingFadeDuration;

        public UIConfiguration(float loadingStableDuration, float loadingFadeDuration)
        {
            LoadingStableDuration = loadingStableDuration;
            LoadingFadeDuration = loadingFadeDuration;
        }
    }
}
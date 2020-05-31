using Unity.Entities;

namespace Runtime.Difficulty.Components
{
    public struct DifficultyStateComponent : IComponentData
    {
        public float Value;
        public float LinerValue;
        
        public bool Reset;
    }
}
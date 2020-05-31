using Runtime.Difficulty.Components;
using Runtime.Difficulty.Configuration;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Tiny;

namespace Runtime.Difficulty
{
    public class DifficultySystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            var entity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(entity, new DifficultyStateComponent {Value = 1.0f});
            
            RequireSingletonForUpdate<DifficultyConfiguration>();
            RequireSingletonForUpdate<DifficultyStateComponent>();
        }

        protected override void OnUpdate()
        {
            var state = GetSingleton<DifficultyStateComponent>();
            var configuration = GetSingleton<DifficultyConfiguration>();

            if (state.Reset)
            {
                state.Value = 1.0f;
                state.LinerValue = 1.0f;
                state.Reset = false;
            }
            else
            {
                state.LinerValue += Time.DeltaTime * configuration.Speed;
                state.Value = math.sqrt(state.LinerValue);
            }
            
            SetSingleton(state);
        }
    }
}
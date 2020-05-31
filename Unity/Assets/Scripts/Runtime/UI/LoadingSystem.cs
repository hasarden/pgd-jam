using Runtime.UI.Components;
using Runtime.UI.Configuration;
using Unity.Entities;
using Unity.Mathematics;
using Unity.U2D.Entities;

namespace Runtime.UI
{
    public class LoadingSystem : ComponentSystem
    {
        protected override void OnCreate()
        {
            var entity = EntityManager.CreateEntity();
            EntityManager.AddComponentData(entity, new LoadingStateComponent());
            
            RequireSingletonForUpdate<UIConfiguration>();
            RequireSingletonForUpdate<LoadingStateComponent>();
        }

        protected override void OnUpdate()
        {
            var configuration = GetSingleton<UIConfiguration>();
            var singleton = GetSingleton<LoadingStateComponent>();

            singleton.Duration += Time.DeltaTime;
            
            SetSingleton(singleton);

            var loadingFactor = singleton.Duration > configuration.LoadingStableDuration
                ? (singleton.Duration - configuration.LoadingStableDuration) / configuration.LoadingFadeDuration
                : 0.0f;

            loadingFactor = math.min(1.0f, loadingFactor);
            
            Entities
                .WithAll<TagLoadingScreenEntry>()
                .ForEach((ref SpriteRenderer renderer) =>
                {
                    var color = renderer.Color;
                    color.a = math.lerp(1.0f, 0.0f, loadingFactor);
                    renderer.Color = color;
                });
        }
    }
}
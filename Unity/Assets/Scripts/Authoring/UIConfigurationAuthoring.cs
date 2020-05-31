using Runtime.UI.Configuration;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class UIConfigurationAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float _loadingStableDuration;
        [SerializeField] private float _loadingFadeDuration;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new UIConfiguration(
                    _loadingStableDuration,
                    _loadingFadeDuration
                )
            );
            
            
        }
    }
}
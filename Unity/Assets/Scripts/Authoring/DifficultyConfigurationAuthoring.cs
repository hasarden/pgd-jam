using Runtime.Difficulty.Configuration;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class DifficultyConfigurationAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float _speed;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new DifficultyConfiguration(_speed)
            );
        }
    }
}
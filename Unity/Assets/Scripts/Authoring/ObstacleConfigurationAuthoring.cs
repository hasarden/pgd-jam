using System.Collections.Generic;
using Runtime.Obstacles.Configuration;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class ObstacleConfigurationAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        [SerializeField] private int _offscreenOffset;
        [SerializeField] private int _spawnPeriod;
        [SerializeField] private float _spawnPeriodDifficultyFactor;
        [SerializeField] private float _skipChance;

        [SerializeField] private ObstacleAuthoring[] _obstacles;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new ObstacleConfiguration(
                    _offscreenOffset,
                    _spawnPeriod,
                    _spawnPeriodDifficultyFactor,
                    _skipChance
                )
            );

            var buffer = dstManager.AddBuffer<ObstacleBufferEntry>(entity);
            foreach (var obstacle in _obstacles)
            {
                var obstacleEntity = conversionSystem.GetPrimaryEntity(obstacle.gameObject);
                var bufferEntry = new ObstacleBufferEntry(obstacleEntity);

                buffer.Add(bufferEntry);
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            foreach (var obstacle in _obstacles)
                referencedPrefabs.Add(obstacle.gameObject);
        }
    }
}

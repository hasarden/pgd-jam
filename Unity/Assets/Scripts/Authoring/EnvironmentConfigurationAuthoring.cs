using System.Collections.Generic;
using Runtime.Environment.Components;
using Runtime.Environment.Configuration;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class EnvironmentConfigurationAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        [SerializeField] private float _speed;

        [SerializeField] private int _unitsRequestedForward;
        [SerializeField] private int _unitsRequestedBackward;
        
        [SerializeField] private int _ceilHeight = 3;
        
        [SerializeField] private EnvironmentBlockAuthoring[] _blocks;

        public EnvironmentBlockAuthoring[] Blocks => _blocks;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new EnvironmentConfiguration(
                    _speed,
                    _unitsRequestedForward,
                    _unitsRequestedBackward,
                    _ceilHeight
                )
            );
            
            var buffer = dstManager.AddBuffer<EnvironmentBlockBufferEntry>(entity);
            foreach (var block in _blocks)
            {
                var blockEntity = conversionSystem.GetPrimaryEntity(block.gameObject);
                var bufferEntry = new EnvironmentBlockBufferEntry(blockEntity, block.Size);

                buffer.Add(bufferEntry);
            }
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            foreach (var block in _blocks)
                referencedPrefabs.Add(block.gameObject);
        }
    }
}
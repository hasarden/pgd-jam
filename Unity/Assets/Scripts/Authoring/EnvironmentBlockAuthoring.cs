using Runtime.Environment.Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class EnvironmentBlockAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private int _size;

        public int Size => _size;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new EnvironmentBlockComponent(_size)
            );
        }
    }
}
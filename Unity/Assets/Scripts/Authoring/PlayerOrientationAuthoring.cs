using Runtime.Player.Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class PlayerOrientationAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private bool _grounded;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new PlayerOrientationComponent(_grounded));
        }
    }
}
using Runtime.Player.Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class TagPlayerAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<TagPlayer>(entity);
        }
    }
}
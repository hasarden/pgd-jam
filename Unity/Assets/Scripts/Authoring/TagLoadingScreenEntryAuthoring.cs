using Runtime.UI.Components;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class TagLoadingScreenEntryAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new TagLoadingScreenEntry());
        }
    }
}
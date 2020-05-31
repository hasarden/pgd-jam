using Runtime.Player.Configuration;
using Unity.Entities;
using UnityEngine;

namespace Authoring
{
    public class PlayerConfigurationAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private float _animationSpeed;
        [SerializeField] private Vector3 _animationScale;

        [SerializeField] private float _jumpDuration;
        [SerializeField] private Vector3 _jumpScale;

        [SerializeField] private float _rayDistance;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(
                entity,
                new PlayerConfiguration(
                    _animationSpeed,
                    _animationScale,
                    _jumpDuration,
                    _jumpScale,
                    _rayDistance
                )
            );
        }
    }
}
using Unity.Entities;

namespace Runtime.Obstacles.Configuration
{
    public struct ObstacleConfiguration : IComponentData
    {
        public int OffscreenOffset;
        public int SpawnPeriod;
        
        public float SkipChance;
        public float SpawnPeriodDifficultyFactor;
        
        public ObstacleConfiguration(
            int offscreenOffset, 
            int spawnPeriod, 
            float spawnPeriodDifficultyFactor, 
            float skipChance
        )
        {
            OffscreenOffset = offscreenOffset;
            SpawnPeriod = spawnPeriod;
            SpawnPeriodDifficultyFactor = spawnPeriodDifficultyFactor;
            SkipChance = skipChance;
        }
    }
}

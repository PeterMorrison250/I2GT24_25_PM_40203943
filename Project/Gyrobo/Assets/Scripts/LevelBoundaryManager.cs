using System.Collections.Generic;

namespace Gyrobo
{
    public static class LevelBoundaryManager
    {
        public static Dictionary<string, LevelBoundary> LevelBoundaryDictionary = new Dictionary<string, LevelBoundary>()
        {
            {"Level1",  new LevelBoundary{MinX = 20, MaxX = -20, MinY = -20, MaxY = 20}},
            {"Level2",  new LevelBoundary{MinX = 20, MaxX = -70, MinY = -20, MaxY = 20}},
        };
    }
}
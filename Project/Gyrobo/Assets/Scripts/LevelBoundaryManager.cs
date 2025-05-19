using System.Collections.Generic;

namespace Gyrobo
{
    public static class LevelBoundaryManager
    {
        public static Dictionary<string, LevelBoundary> LevelBoundaryDictionary = new Dictionary<string, LevelBoundary>()
        {
            {"Sandbox",  new LevelBoundary{MinX = 20, MaxX = -20, MinY = -20, MaxY = 20}},
            {"Corridor",  new LevelBoundary{MinX = 20, MaxX = -70, MinY = -20, MaxY = 20}},
            {"LaserButton",  new LevelBoundary{MinX = 20, MaxX = -30, MinY = -20, MaxY = 20}},
            {"Elevator", new LevelBoundary{MinX = 20, MaxX = -30, MinY = 0, MaxY = 60}},
            {"HeliumElevator", new LevelBoundary{MinX = 20, MaxX = -80, MinY = 0, MaxY = 100}},
            {"TitleScreen", new LevelBoundary{MinX = 20, MaxX = -30, MinY = 0, MaxY = 60}},
            {"GravityLaser", new LevelBoundary{MinX = 40, MaxX = -30, MinY = 0, MaxY = 60}},
        };
    }
}
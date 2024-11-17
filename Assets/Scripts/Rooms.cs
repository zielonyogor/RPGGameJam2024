using System.Collections.Generic;
using UnityEngine;

public enum Room
{
    Hub,
    Past,
    Present,
    Future
}

public class Rooms
{
    private static readonly Dictionary<Room, VisionMode> roomVisionMode = new Dictionary<Room, VisionMode>
    {
        {Room.Hub, VisionMode.Default},
        {Room.Past, VisionMode.Sepia},
        {Room.Present, VisionMode.Default},
        {Room.Future, VisionMode.Cyberpunk}
    };

    private static readonly Dictionary<Room, TerrainType> roomTerrain = new Dictionary<Room, TerrainType>
    {
        {Room.Hub, TerrainType.Foliage},
        {Room.Past, TerrainType.Wood},
        {Room.Present, TerrainType.Stone},
        {Room.Future, TerrainType.Metal}
    };

    private static readonly Dictionary<Room, Vector3> roomPosition = new Dictionary<Room, Vector3>
    {
        {Room.Hub, new Vector3(0, 0, 0)},
        {Room.Past, new Vector3(-32, 0, 0)},
        {Room.Present, new Vector3(0, 32, 0)},
        {Room.Future, new Vector3(32, 0, 0)}
    };

    public static VisionMode GetVisionMode(Room room)
    {
        return roomVisionMode[room];
    }

    public static TerrainType GetTerrain(Room room)
    {
        return roomTerrain[room];
    }

    public static Vector3 GetPosition(Room room)
    {
        return roomPosition[room];
    }
}
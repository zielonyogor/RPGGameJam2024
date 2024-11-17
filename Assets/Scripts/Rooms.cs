using System.Collections.Generic;
using UnityEngine;

public enum Room
{
    Hub,
    Past,
    Present,
    Future
}

public enum Era
{
    Past,
    Present,
    Future
}

public class Rooms
{
    public static readonly int RoomSize = 32;

    private static readonly Dictionary<Room, TerrainType> roomTerrain = new Dictionary<Room, TerrainType>
    {
        {Room.Hub, TerrainType.Stone},
        {Room.Past, TerrainType.Foliage},
        {Room.Present, TerrainType.Grass},
        {Room.Future, TerrainType.Metal}
    };

    private static readonly Dictionary<Room, Vector3> roomPosition = new Dictionary<Room, Vector3>
    {
        {Room.Hub, new Vector3(0, 0, 0)},
        {Room.Past, new Vector3(0, -32, 0)},
        {Room.Present, new Vector3(0, -64, 0)},
        {Room.Future, new Vector3(0, -96, 0)}
    };

    public static Era GetEra(Room room)
    {
        switch (room)
        {
            case Room.Past:
                return Era.Past;
            case Room.Present:
                return Era.Present;
            case Room.Future:
                return Era.Future;
            default:
                return Era.Present;
        }
    }

    public static TerrainType GetTerrain(Room room)
    {
        return roomTerrain[room];
    }

    public static Vector3 GetPosition(Room room)
    {
        return roomPosition[room];
    }

    public static bool IsInsideRoom(Room room, Vector3 position)
    {
        Vector3 roomPosition = GetPosition(room);
        return position.x >= roomPosition.x - RoomSize / 2 &&
               position.x < roomPosition.x + RoomSize / 2 &&
               position.y >= roomPosition.y - RoomSize / 2 &&
               position.y < roomPosition.y + RoomSize / 2;
    }

    public static Room GetRoomAtPosition(Vector3 position)
    {
        foreach (Room room in roomPosition.Keys)
        {
            if (IsInsideRoom(room, position))
            {
                return room;
            }
        }
        return Room.Hub;
    }
}
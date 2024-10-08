using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages Player interactions with Mini Games within Worlds
/// and ... (e.g., saving)
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum LocationType
    {
        SpawnPoint,
        House,
        School,
        Airport,
        Aquarium,
        Cinemas,
    }

    private readonly Dictionary<string, Vector3> locations = new Dictionary<string, Vector3>
    {
        { LocationType.SpawnPoint.ToString(), new Vector3( 225f, 0.12f, 45f )},
        { LocationType.House.ToString(), new Vector3( 200f, 0.1f, -28f )},
        { LocationType.School.ToString(), new Vector3( 222f, 0.12f, 60f )},
        { LocationType.Airport.ToString(), new Vector3( 188f, 2f, 54f )},
        { LocationType.Aquarium.ToString(), new Vector3( 174f, 2f, 6f )},
        { LocationType.Cinemas.ToString(), new Vector3( 224f, 2f, 4f )},

    };

    public void TeleportPlayer(string loc)
    {
        var player = WorldManager.PlayerInstance;
        Vector3 newLoc;
        if (locations.TryGetValue(loc, out newLoc))
        {
            player.transform.position = newLoc;
            Debug.Log("WorldManager TeleportPlayer: Teleporting to " + loc);
            return;
        }
        Debug.Log(loc);
        Debug.LogError("WorldManager TeleportPlayer: Unable to teleport to " + loc);
    }
}

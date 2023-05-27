using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public MatchSettings matchSettings;

    public static GameManager instance;

    [SerializeField] private GameObject sceneCamera;

    public delegate void OnPlayerKilledCallback(string player, string source);

    public OnPlayerKilledCallback onPlayerKilledCallback;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Debug.LogError("Plus d'une instance de GameManager dans la scène");
    }

    public void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera == null)
        {
            return;
        }
        
        sceneCamera.SetActive(isActive);
    }
    
    public static void RegisterPlayer(string netID, Player player)
    // cette methode enregistre le ID du player dans un dictionnaire
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId,player);
        player.transform.name = playerId;
    }

    public static void UnRegisterPlayer(string playerId)
    // cette methode supprime dans le dictionnaire le ID du player 
    {
        players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId)
    // cette methode récupere le script du joueur
    {
        return players[playerId];
    }


    public static Player[] GetAllPlayer()
    {
        return players.Values.ToArray();
    }
}

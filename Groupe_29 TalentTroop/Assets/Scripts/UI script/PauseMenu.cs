using System;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PauseMenu : NetworkBehaviour
{
    public static bool isOn = false;

    private NetworkManager networkManager;
    
    private void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void LeaveRoomButton()
    {
        if (isClientOnly)
        {
            networkManager.StopClient();
        }
        else
        {
            networkManager.StopHost();
        }
    }
}

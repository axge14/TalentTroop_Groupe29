using System;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;
    

    [SerializeField] 
    private string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private GameObject playerUIPrefabs;
    
    [HideInInspector]
    public GameObject playerUIInstance;
    
    private void Start()
    {
        if (!isLocalPlayer)
        {
            // désactivation des composants si ce joueur n'est pas le notre
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            // création du UI du joueur local
            playerUIInstance = Instantiate(playerUIPrefabs);
            // Configuration du UI
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if(ui == null)
            {
                Debug.LogError("Pas de component PlayerUI sur playerUIInstance");
            }
            else
            {
                ui.SetController(GetComponent<Player>());
            }

            GetComponent<Player>().Setup();
            
        }
    }

    [Command]
    void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username+ " has joined !");
            player.username = username;
        }
    }
    

    public override void OnStartClient()
    // méthode qui s'éxecute automatiquement quand un player rejoint la partie
    {
        base.OnStartClient();
        
        RegisterPlayerAndSetUsername();
    }

    // dans le cas ou le build est uniquement un serveur 
    public override void OnStartServer()
    {
        base.OnStartServer();
        RegisterPlayerAndSetUsername();
    }

    private void RegisterPlayerAndSetUsername()
    {
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
                
        GameManager.RegisterPlayer(netId,player);
        CmdSetUsername(transform.name, UserAccountManager.LoggedInUsername);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    // méthode lu quand un joueur quitte le serveur
    {
        Destroy(playerUIInstance);

        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
        }
        
        GameManager.UnRegisterPlayer(transform.name);
    }
}

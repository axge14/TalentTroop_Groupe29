using System;
using UnityEngine;
using Mirror;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour
{
    [SyncVar]
    private bool _isDead = false;

    [SerializeField] 
    private float maxHealth = 100f;
    
    [SyncVar]
    public float currentHealth;

    public int kills;
    public int deaths;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    
    [SerializeField] private GameObject[] disableGameObjectOnDeath;

    private bool[] wasEnabledOnStart;

    [SerializeField] private GameObject deathEffect;

    private bool firstSetup = true;
    
    [SyncVar] public string username = "Player";

    [SerializeField] private AudioClip destroySound;

    public bool isDead{
        get {return _isDead;}
        protected set { _isDead = value;}
    }
 
    public void Setup()
    {
        if (isLocalPlayer)
        {
            // changement de caméra
            GameManager.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
        }
        
        CmdBroadCastNewPlayerSetup();
    }

    [Command(requiresAuthority = false)]
    private void CmdBroadCastNewPlayerSetup()
    {
        RpcSetupPlayerOnAllClient();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClient()
    {
        if (firstSetup)
        {
            wasEnabledOnStart = new bool[disableOnDeath.Length];
                    
            for (int i = 0; i < disableOnDeath.Length; i++)
            {
                wasEnabledOnStart[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }
        
        SetDefault();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
    }


    public void SetDefault()
    {
        isDead = false;
        currentHealth = maxHealth;

        // ré-active les scripts du joueur lors de sa mort
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabledOnStart[i];
        }
        
        // ré-active les GameObject du joueur lors de sa mort
        for (int i = 0; i < disableGameObjectOnDeath.Length;i++)
        {
            disableGameObjectOnDeath[i].SetActive(true); 
        }

        // reactive le collider du joueur
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }
    }
    
    [ClientRpc]
    public void RpcTakeDamage(float amount, string sourceID)
    {
        if (isDead)
        {
            return;
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        
        
        currentHealth -= amount;
        Debug.Log(transform.name + " a maintenant " + currentHealth + "point de vie");

        if (currentHealth <= 0)
        {
            Die(sourceID);
            audioSource.PlayOneShot(destroySound);
        }
    }

    private void Die(string sourceID)
    {
        isDead = true;

        Player sourcePlayer = GameManager.GetPlayer(sourceID);

        if (sourcePlayer != null)
        {
            sourcePlayer.kills++;
            GameManager.instance.onPlayerKilledCallback.Invoke(username, sourcePlayer.username);
        }
        
        deaths++;
        
        // désactive les components du joueur lors de sa mort
        for (int i = 0; i < disableOnDeath.Length;i++)
        {
            disableOnDeath[i].enabled = false;
        }
        
        // désactive les GameObject du joueur lors de sa mort
        for (int i = 0; i < disableGameObjectOnDeath.Length;i++)
        {
            disableGameObjectOnDeath[i].SetActive(false); 
        }
        
        // désactive le collider du joueur
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
        
        // apparition du systeme de particule de la mort
        GameObject _gfxIns = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);

        // changement de caméra
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
        }
    
        Debug.Log(transform.name + " a été éliminé");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTimer);
        
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        yield return new WaitForSeconds(0.1f);

        Setup();
    }
}

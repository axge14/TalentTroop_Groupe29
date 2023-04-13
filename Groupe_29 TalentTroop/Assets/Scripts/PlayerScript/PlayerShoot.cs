using System;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


public class PlayerShoot : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeapon weapon;

    [SerializeField]
    private GameObject weaponGFX;

    [SerializeField]
    private string weaponLayerName = "Weapon";
    
    [SerializeField]
    private Camera cam;

    [SerializeField] private LayerMask mask;
    

    
    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("Pas de caméra renseignée sur le systeme de tir");
            this.enabled = false;
        }

        weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,weapon.range,mask))
        {
            if (hit.collider.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name,weapon.damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string playerName, float damage)
    {
        Debug.Log(playerName + "a été touché");

        Player player = GameManager.GetPlayer(playerName);
        player.RpcTakeDamage(damage);
    }
}
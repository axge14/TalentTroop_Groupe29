using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Player player;
    private PlayerController controller;
    private WeaponManager weaponManager;
    
    [SerializeField] public GameObject pauseMenu;
    
    [SerializeField] public GameObject scoreBoard;

    [SerializeField] private Text ammoText;

    [SerializeField] private Text healthText;
    
    public void SetController(Player _player)
    {
        player = _player;
        controller = _player.GetComponent<PlayerController>();
        weaponManager = _player.GetComponent<WeaponManager>();
    }
    
    private void Update()
    {
        SetHealthAmount(player.currentHealth);
        SetAmmoAmount(weaponManager.currentMagazineSize);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreBoard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreBoard.SetActive(false);
        }
    }

    private void Start()
    {
        PauseMenu.isOn = false;
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }

    void SetAmmoAmount(int _ammount)
    {
        ammoText.text = _ammount.ToString();
    }

    void SetHealthAmount(float _amount)
    {
        healthText.text = _amount.ToString();
    }
}

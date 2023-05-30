using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{

    [SerializeField] private GameObject playerScoreBoardItem;

    [SerializeField] private Transform playerScoreBoardList;
    
    private void OnEnable()
    {
        // recuperer une array de tous les joueurs du serveur

        Player[] players = GameManager.GetAllPlayer();

        foreach (Player player in players)
        {
            GameObject itemGO = Instantiate(playerScoreBoardItem, playerScoreBoardList);
            PlayerScoreBoardItem item = itemGO.GetComponent<PlayerScoreBoardItem>();

            if (item != null)
            {
                item.Setup(player);
            }
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in playerScoreBoardList)
        {
            Destroy(child.gameObject);
        }
    }
}

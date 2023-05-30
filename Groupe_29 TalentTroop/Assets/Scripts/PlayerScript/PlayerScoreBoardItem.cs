using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBoardItem : MonoBehaviour
{
    [SerializeField] private Text usernameText;
    [SerializeField] private Text killsText;
    [SerializeField] private Text deathsText;

    public void Setup(Player player)
    {
        usernameText.text = player.username;
        killsText.text = "Kills : " + player.kills;
        deathsText.text = "Deaths : " + player.deaths;
    }
    
}

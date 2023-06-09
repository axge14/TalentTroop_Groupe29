using UnityEngine;
using UnityEngine.UI;

public class PlayerNameplate : MonoBehaviour
{
    [SerializeField] private Text usernameText;

    [SerializeField] private Player player;
    
    void Update()
    {
        usernameText.text = player.username;
    }
}

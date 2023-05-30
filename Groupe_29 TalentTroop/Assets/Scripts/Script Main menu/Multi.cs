using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Multi : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    
    public string lobbySceneName = "Login";

    private void Start()
    {
        playButton.onClick.AddListener(PlayButtonOnClick);
    }

    private void PlayButtonOnClick()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
    
}

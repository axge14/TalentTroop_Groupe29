using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private Button quitButton;

    private void Start()
    {
        // Récupère le composant Button attaché au GameObject
        quitButton = GetComponent<Button>();

        // Ajoute un écouteur d'événement pour le clic sur le bouton
        quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        // Quitte l'application
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

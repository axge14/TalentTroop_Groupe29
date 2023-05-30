using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private Button quitButton;

    private void Start()
    {
        // R�cup�re le composant Button attach� au GameObject
        quitButton = GetComponent<Button>();

        // Ajoute un �couteur d'�v�nement pour le clic sur le bouton
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

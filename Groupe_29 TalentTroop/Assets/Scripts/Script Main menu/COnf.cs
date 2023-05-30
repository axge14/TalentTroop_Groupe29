using UnityEngine;
using UnityEngine.UI;

public class COnf : MonoBehaviour
{

    [SerializeField]
    private Button showButton;

    [SerializeField]
    private GameObject[] objectsToShow;

    private void Start()
    {
        showButton.onClick.AddListener(ShowObjectsOnClick);
        HideObjects();
    }

    private void ShowObjectsOnClick()
    {
        ShowObjects();
    }

    private void ShowObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }

    private void HideObjects()
    {
        foreach (GameObject obj in objectsToShow)
        {
            obj.SetActive(false);
        }
    }
}

using UnityEngine;

public class Set : MonoBehaviour
{
    [SerializeField]
    private GameObject currentCanvas;

    [SerializeField]
    private GameObject targetCanvas;

    public void OnButtonClick()
    {
        // D�sactiver le Canvas actuel
        currentCanvas.SetActive(false);

        // Activer le Canvas cible
        targetCanvas.SetActive(true);
    }
}

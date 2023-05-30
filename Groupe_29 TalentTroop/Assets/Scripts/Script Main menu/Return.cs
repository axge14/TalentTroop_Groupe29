using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Return : MonoBehaviour
{
    [SerializeField]
    private Button noButton;


    [SerializeField]
    private List<GameObject> elementList = new List<GameObject>();


    private void Start()
    {
        noButton = GetComponent<Button>();

        noButton.onClick.AddListener(Clear);
    }

    private void Clear()
    {
        foreach (GameObject element in elementList)
        {
            element.SetActive(false);
        }
    }
}

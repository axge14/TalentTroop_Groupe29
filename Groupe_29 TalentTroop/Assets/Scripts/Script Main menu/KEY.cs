using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KEY : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI configText;

    [SerializeField]
    private GameObject panelConfig;

    private KeyCode toucheAvancer = KeyCode.Z; // Touche par défaut

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    toucheAvancer = keyCode;
                    configText.text = keyCode.ToString();
                    PlayerPrefs.SetString("ToucheAvancer", keyCode.ToString());
                    panelConfig.SetActive(false);
                    break;
                }
            }
        }
    }
}

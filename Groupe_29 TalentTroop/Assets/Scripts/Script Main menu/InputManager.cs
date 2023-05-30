using UnityEngine;

public class InputManager : MonoBehaviour
{

    
    public KeyCode forwardKey = KeyCode.W;

    private static InputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeForwardKey()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    forwardKey = keyCode;
                    break;
                }
            }
        }
    }
}

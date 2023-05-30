using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundController : MonoBehaviour
{
    public AudioSource soundManager;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        soundManager.volume = value;
    }

    public void PlayHoverSound()
    {
        if (hoverSound != null)
        {
            soundManager.PlayOneShot(hoverSound);
        }
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            soundManager.PlayOneShot(clickSound);
        }
    }
}

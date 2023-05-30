using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    [SerializeField]
    private AudioClip hoverSound;  // Son lorsque la souris passe sur le bouton

    [SerializeField]
    private AudioClip clickSound;  // Son lorsque le bouton est cliqu�

    private AudioSource audioSource;

    private void Start()
    {
        // R�cup�rer le composant AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Jouer le son lorsque la souris passe sur le bouton
        audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Jouer le son lorsque le bouton est cliqu�
        audioSource.PlayOneShot(clickSound);
    }
}

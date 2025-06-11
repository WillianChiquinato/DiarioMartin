using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioBotaoHover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioBotaoHover != null)
        {
            audioBotaoHover.Play();
        }
    }
}

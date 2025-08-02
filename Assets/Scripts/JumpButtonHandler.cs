using UnityEngine.EventSystems;
using UnityEngine;

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool jumpRequested = false;
    public bool isButtonHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        jumpRequested = true;
        isButtonHeld = true;  // Butona basýldýðýnda basýlý tutma durumu baþlasýn
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;  // Buton býrakýldýðýnda basýlý tutma durumu sonlansýn
    }
}

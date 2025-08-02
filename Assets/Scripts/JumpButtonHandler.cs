using UnityEngine.EventSystems;
using UnityEngine;

public class JumpButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool jumpRequested = false;
    public bool isButtonHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        jumpRequested = true;
        isButtonHeld = true;  // Butona bas�ld���nda bas�l� tutma durumu ba�las�n
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;  // Buton b�rak�ld���nda bas�l� tutma durumu sonlans�n
    }
}

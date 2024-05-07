using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeWhileHold : MonoBehaviour, IPointerEnterHandler
{
    public CameraMover camMover;

    public HandController handController;

    public int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(handController.MovingObject != null)
        camMover.MoveCameraToCarriage(index);
    }
}

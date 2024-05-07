using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JailManager : MonoBehaviour, IPointerEnterHandler
{
    public HandController handController;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (handController.MovingObject != null)
        {
            handController.DeleteOnUp = true;
        }
    }
}

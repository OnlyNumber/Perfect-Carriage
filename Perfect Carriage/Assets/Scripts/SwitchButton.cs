using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    public Button MyButton;

    public Image SwitchImage;

    [SerializeField]
    private Sprite _switchOnSprite;

    [SerializeField]
    private Sprite _switchOffSprite;

    public void SwitchSprites(bool state)
    {
        if(state)
        {
            SwitchImage.sprite = _switchOnSprite;
        }
        else
        {
            SwitchImage.sprite = _switchOffSprite;
        }


    }
}

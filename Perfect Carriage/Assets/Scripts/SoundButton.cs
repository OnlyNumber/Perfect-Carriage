using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioButton;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySoundAtPoint(audioButton);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinActions : MonoBehaviour
{
    public AudioClip WinAudio; 

    public ParticleSystem particleSystem;

    public PanelActivator panelActivator;

    private void Awake()
    {
        panelActivator.OnShowPanel += () => particleSystem.Play();

        panelActivator.OnShowPanel += () => SoundManager.Instance.PlaySoundAtPoint(WinAudio);


    }


}

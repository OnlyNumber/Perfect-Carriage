using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public SwitchButton SwitchMusic;

    public SwitchButton SwitchSound;

    public AudioSource backgroundMusic;

    private ShopSkinContainer _shopSkinContainer;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (DataController.Instance.IsLoaded)
        {
            Initialize(DataController.Instance.GetComponent<ShopSkinContainer>());
        }
        else
        {
            DataController.Instance.OnDataLoaded += ()=> Initialize(DataController.Instance.GetComponent<ShopSkinContainer>());
        }
    }

    public void Initialize(ShopSkinContainer shopSkinContainer)
    {
        _shopSkinContainer = shopSkinContainer;

        backgroundMusic.volume = DataController.Instance.SaveData.MusicVolume;

        backgroundMusic.Play();

        SwitchMusic.SwitchSprites(System.Convert.ToBoolean(DataController.Instance.SaveData.MusicVolume));

        SwitchSound.SwitchSprites(System.Convert.ToBoolean(DataController.Instance.SaveData.SoundVolume));

        SwitchMusic.MyButton.onClick.AddListener(ChangeMusicVolume);

        SwitchSound.MyButton.onClick.AddListener(ChangeSoundVolume);

        DataController.Instance.OnSkinChanged += ChangeMusic;

        ChangeMusic();

    }

    public void ChangeMusic()
    {
        backgroundMusic.clip = _shopSkinContainer.BackgroundMusics[DataController.Instance.SaveData.CurrentSkins[(int)SkinType.music]];

        backgroundMusic.Play();
    }

    public void ChangeMusicVolume()
    {
        if(System.Convert.ToBoolean(DataController.Instance.SaveData.MusicVolume))
        {
            DataController.Instance.SaveData.MusicVolume = 0;
        }
        else
        {
            DataController.Instance.SaveData.MusicVolume = 1;
        }

        backgroundMusic.volume = DataController.Instance.SaveData.MusicVolume;


        SwitchMusic.SwitchSprites(System.Convert.ToBoolean(DataController.Instance.SaveData.MusicVolume));
    }

    public void ChangeSoundVolume()
    {
        if (System.Convert.ToBoolean(DataController.Instance.SaveData.SoundVolume))
        {
            DataController.Instance.SaveData.SoundVolume = 0;
        }
        else
        {
            DataController.Instance.SaveData.SoundVolume = 1;
        }

        SwitchSound.SwitchSprites(System.Convert.ToBoolean(DataController.Instance.SaveData.SoundVolume));
    }

    public void PlaySoundAtPoint(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, DataController.Instance.SaveData.SoundVolume);
    }

    private void OnDestroy()
    {
        DataController.Instance.OnSkinChanged -= ChangeMusic;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    //public static string SAVE_DATA = "SaveData";

    public static DataController Instance;

    public SaveData SaveData;

    public System.Action OnDataLoaded;

    public bool IsLoaded = false;

    public System.Action OnChangeCoins;

    public System.Action OnSkinChanged;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        Load();
    }


    public void Load()
    {
        if (PlayerPrefs.HasKey(ConstantData.SAVE_DATA))
        {
            SaveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(ConstantData.SAVE_DATA));
        }
        else
        {
            SaveData = new SaveData();
        }

        IsLoaded = true;

        OnDataLoaded?.Invoke();
    }

    public void Save()
    {
        PlayerPrefs.SetString(ConstantData.SAVE_DATA, JsonUtility.ToJson(SaveData));


    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public bool CoinsChange(int changeCoins)
    {
        if (SaveData.Coins + changeCoins < 0)
        {
            return false;
        }

        SaveData.Coins += changeCoins;
        OnChangeCoins?.Invoke();

        return true;
    }

    public void ChangeSkin(SkinType type, int index)
    {
        SaveData.OpenedSkins[(int)type].Skins[index] = true;

        SaveData.CurrentSkins[(int)type] = index;

        Debug.Log("Change skin");

        OnSkinChanged?.Invoke();
    }

}

[System.Serializable]
public class SaveData
{
    public int UpgradeIndexTime;

    public List<CarriageData> CariageDatas = new List<CarriageData>();

    public int Coins;

    public int SoundVolume;

    public int MusicVolume;

    public List<int> SkillCooldowns = new List<int>();

    public List<WrapSkinClass> OpenedSkins = new List<WrapSkinClass>();

    public List<int> CurrentSkins = new List<int>();

    public bool IsFirstLog = true;

    public SaveData()
    {
        Coins = 200;

        SoundVolume = 0;

        MusicVolume = 0;

        for (int i = 0; i < 3; i++)
        {
            SkillCooldowns.Add(0);
        }

        for (int i = 0; i < ConstantData.SKINS_TYPES_COUNT; i++)
        {
            OpenedSkins.Add(new WrapSkinClass());
            CurrentSkins.Add(0);
        }

        for (int i = 0; i < ConstantData.DEFAULT_CARRIAGE_AMOUNT; i++)
        {
            CariageDatas.Add(new CarriageData());
        }
    }

    

}

[System.Serializable]
public class WrapSkinClass
{
    public List<bool> Skins = new List<bool>();

    public WrapSkinClass()
    {

    }

}

public enum SkinType
{
    mainBackground,
    trainBackground,
    music
}


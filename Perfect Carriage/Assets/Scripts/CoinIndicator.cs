using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinIndicator : MonoBehaviour
{
    [SerializeField]
    private List<TMP_Text> _coinTexts = new List<TMP_Text>();

    private void Start()
    {
        if (DataController.Instance.IsLoaded)
        {
            Initialize();
        }
        else
        {
            DataController.Instance.OnDataLoaded += Initialize;
        }
    }

    public void Initialize()
    {
        ChangeIndicatorsText();

        DataController.Instance.OnChangeCoins += ChangeIndicatorsText;

    }

    public void ChangeIndicatorsText()
    {
        foreach (var item in _coinTexts)
        {
            item.text = DataController.Instance.SaveData.Coins.ToString();
        }
    }

}

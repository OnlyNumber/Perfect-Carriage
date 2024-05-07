using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageShop : MonoBehaviour
{
    public RectTransform CarriageContent;

    public CarriageShopItem CarriageShopItemPrefab;

    public List<CarriageShopItem> CarriageShopItemsPool = new List<CarriageShopItem>();

    public UpgradeShopItem upgradePanelActivator;

    public CarriageFabric carriageFabric;

    public TMPro.TMP_Text CarriageCostText;

    private int CurrentUpgradeIndex;



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
        upgradePanelActivator.UpgradeTypeButton.onClick.AddListener(UpgradeType);

        upgradePanelActivator.UpgradeCostButton.onClick.AddListener(UpgradeCostPerPassanger);

        upgradePanelActivator.UpgradeAmountButton.onClick.AddListener(UpgradePlacesAmount);



        for (int i = 0; i < DataController.Instance.SaveData.CariageDatas.Count; i++)
        {
            int index;

            CarriageShopItemsPool.Add(Instantiate(CarriageShopItemPrefab, CarriageContent));

            index = i;



            CarriageShopItemsPool[i].CarriageImage.sprite = GetTypeSprite(DataController.Instance.SaveData.CariageDatas[i].type);

            CarriageShopItemsPool[i].CarriageButton.onClick.AddListener(() => ShowUpgrades(index));
        }

        int CarriageIndex = DataController.Instance.SaveData.CariageDatas.Count - (ConstantData.DEFAULT_CARRIAGE_AMOUNT - 1);

        CarriageCostText.text = (ConstantData.CARRIAGE_COST * (CarriageIndex)).ToString();


        ChangeContentSize();

    }

    public Sprite GetTypeSprite(PassangerType type)
    {
        switch (type)
        {
            case PassangerType.economy:
                {
                    return carriageFabric.EconomSprite;
                }
            case PassangerType.standard:
                {
                    return carriageFabric.StandardSprite;
                }
            case PassangerType.VIP:
                {
                    return carriageFabric.VIPSprite;
                }
        }

        return carriageFabric.EconomSprite;

    }


    public void ChangeContentSize()
    {
        float carriagesCount = DataController.Instance.SaveData.CariageDatas.Count;

        float carriageSHeight = CarriageShopItemPrefab.GetComponent<RectTransform>().rect.height;

        float spacing = CarriageContent.GetComponent<UnityEngine.UI.VerticalLayoutGroup>().spacing;

        CarriageContent.sizeDelta = new Vector2(0, carriagesCount * (carriageSHeight + spacing));
    }

    public void UpgradeType()
    {
        if (!DataController.Instance.CoinsChange(-ConstantData.UPGRADE_TYPE_COST * (int)DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].type))
        {
            return;
        }

        DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].type += 1;
        UpdateInfo(CurrentUpgradeIndex);
    }

    public void UpgradeCostPerPassanger()
    {
        if (!DataController.Instance.CoinsChange(-ConstantData.UPGRADE_PLACE_COST * (1 + DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradeCostPerPassanger)))
        {
            return;
        }

        DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradeCostPerPassanger += 1;

        UpdateInfo(CurrentUpgradeIndex);
    }

    public void UpgradePlacesAmount()
    {
        if (!DataController.Instance.CoinsChange(-ConstantData.UPGRADE_PLACE_AMOUNT_COST * (1 + DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradePlacesAmount)))
        {
            return;
        }

        DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradePlacesAmount += 1;
        UpdateInfo(CurrentUpgradeIndex);
    }

    public void ShowUpgrades(int i)
    {
        upgradePanelActivator.panelActivator.Activate(true);

        CurrentUpgradeIndex = i;

        UpdateInfo(CurrentUpgradeIndex);

    }

    public void UpdateInfo(int i)
    {
        upgradePanelActivator.CarriageIcon.sprite = GetTypeSprite(DataController.Instance.SaveData.CariageDatas[i].type);

        CarriageShopItemsPool[i].CarriageImage.sprite = GetTypeSprite(DataController.Instance.SaveData.CariageDatas[i].type);

        upgradePanelActivator.TypeText.text = "Type: " + DataController.Instance.SaveData.CariageDatas[i].type.ToString();

        upgradePanelActivator.CostText.text = "Cost passanger: x" + (1 + 0.1f * DataController.Instance.SaveData.CariageDatas[i].UpgradeCostPerPassanger);

        upgradePanelActivator.AmountText.text = "Amount passanger: " + (ConstantData.DEFAULT_TRAIN_PLACES + DataController.Instance.SaveData.CariageDatas[i].UpgradePlacesAmount);

        if (DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].type != PassangerType.VIP)
        {
            upgradePanelActivator.UpgradeTypeButton.GetComponentInChildren<TMPro.TMP_Text>().text =
                (ConstantData.UPGRADE_TYPE_COST * (int)DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].type).ToString();
            upgradePanelActivator.UpgradeTypeButton.interactable = true;

        }
        else
        {
            upgradePanelActivator.UpgradeTypeButton.GetComponentInChildren<TMPro.TMP_Text>().text = "MAX";
            upgradePanelActivator.UpgradeTypeButton.interactable = false;
        }

        if (DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradePlacesAmount != 5)
        {
            upgradePanelActivator.UpgradeAmountButton.GetComponentInChildren<TMPro.TMP_Text>().text =
            (ConstantData.UPGRADE_PLACE_AMOUNT_COST * (1 + DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradePlacesAmount)).ToString();
            upgradePanelActivator.UpgradeAmountButton.interactable = true;
        }
        else
        {
            upgradePanelActivator.UpgradeAmountButton.GetComponentInChildren<TMPro.TMP_Text>().text = "MAX";
            upgradePanelActivator.UpgradeAmountButton.interactable = false;
        }

        if (DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradeCostPerPassanger != 10)
        {

            upgradePanelActivator.UpgradeCostButton.GetComponentInChildren<TMPro.TMP_Text>().text =
                    (ConstantData.UPGRADE_PLACE_COST * (1 + DataController.Instance.SaveData.CariageDatas[CurrentUpgradeIndex].UpgradeCostPerPassanger)).ToString();
            upgradePanelActivator.UpgradeCostButton.interactable = true;
        }
        else
        {
            upgradePanelActivator.UpgradeCostButton.GetComponentInChildren<TMPro.TMP_Text>().text = "MAX";
            upgradePanelActivator.UpgradeCostButton.interactable = false;
        }
    }

    public void BuyCarriage()
    {
        int CarriageIndex = DataController.Instance.SaveData.CariageDatas.Count - (ConstantData.DEFAULT_CARRIAGE_AMOUNT - 1);

        if (!DataController.Instance.CoinsChange(-ConstantData.CARRIAGE_COST * CarriageIndex))
        {
            return;
        }

        DataController.Instance.SaveData.CariageDatas.Add(new CarriageData());

        int index = CarriageShopItemsPool.Count;

        CarriageShopItemsPool.Add(Instantiate(CarriageShopItemPrefab, CarriageContent));

        CarriageShopItemsPool[index].CarriageButton.onClick.AddListener(() => ShowUpgrades(index));

        CarriageCostText.text = (ConstantData.CARRIAGE_COST * (CarriageIndex + 1)).ToString();

        ChangeContentSize();

    }




}

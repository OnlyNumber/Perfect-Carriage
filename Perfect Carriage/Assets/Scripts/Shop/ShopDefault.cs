using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDefault : MonoBehaviour
{
    protected DataController Player;

    [SerializeField]
    protected SkinType ShopType;

    protected List<ShopItem> ShopItems = new List<ShopItem>();

    [SerializeField]
    private ShopItem _itemPrefab;

    [SerializeField]
    private RectTransform _contentTransform;

    private void Start()
    {
        if (DataController.Instance.IsLoaded)
        {
            Initialize(DataController.Instance, DataController.Instance.GetComponent<ShopSkinContainer>());
        }
        else
        {
            DataController.Instance.OnDataLoaded += () => Initialize(DataController.Instance, DataController.Instance.GetComponent<ShopSkinContainer>());
        }

    }

    public void Initialize(DataController player, ShopSkinContainer skinContainer)
    {

        ShopItem transfer;

        while (player.SaveData.OpenedSkins[(int)ShopType].Skins.Count < skinContainer.GetCount(ShopType))
        {
            player.SaveData.OpenedSkins[(int)ShopType].Skins.Add(false);
        }

        while (player.SaveData.OpenedSkins[(int)ShopType].Skins.Count > skinContainer.GetCount(ShopType))
        {
            player.SaveData.OpenedSkins[(int)ShopType].Skins.RemoveAt(player.SaveData.OpenedSkins[(int)ShopType].Skins.Count - 1);
        }

        Player = player;

        _contentTransform.rect.size.Set(_contentTransform.rect.width, 50 + (_itemPrefab.GetComponent<RectTransform>().rect.height + 50) * ShopItems.Count);

        for (int i = 0; i < skinContainer.GetCount(ShopType); i++)
        {
            transfer = Instantiate(_itemPrefab, _contentTransform);

            int index = i;

            ShopItems.Add(transfer);

            switch (ShopType)
            {
                case SkinType.trainBackground:
                    {
                        transfer.ItemImage.sprite = skinContainer.TrainBackgrounds[i];
                        break;
                    }

                /*case SkinType.mainBackground:
                    {
                        transfer.ItemImage.sprite = skinContainer.BuildingsInfoFabrics[i].Get(BuildingType.Residential).Icon;
                        break;
                    }*/

                case SkinType.mainBackground:
                    {
                        transfer.ItemImage.sprite = skinContainer.MainBackgrounds[i];
                        break;
                    }
            }

            /* if (ShopType == SkinType.background)
             {
                 transfer.ItemImage.sprite = skinContainer.Backgrounds[i];
             }*/



            transfer.CostText.text = (i * 100).ToString();

            if (Player.SaveData.OpenedSkins[(int)ShopType].Skins[i])
            {
                if (Player.SaveData.CurrentSkins[(int)ShopType] == i)
                {
                    transfer.AcceptImage.gameObject.SetActive(true);
                }

                transfer.CostGO.SetActive(false);
            }

            transfer.ShopButton.onClick.AddListener(() => BuyOrEquip(ShopType, index, index * 300));
        }

        if (!Player.SaveData.OpenedSkins[(int)ShopType].Skins[0])
        {
            Player.SaveData.OpenedSkins[(int)ShopType].Skins[0] = true;
            BuyOrEquip(ShopType, 0, 0);
        }

    }


    public void BuyOrEquip(SkinType type, int index, float cost)
    {
        if (Player.SaveData.OpenedSkins[(int)type].Skins[index])
        {
            ShopItems[index].CostGO.SetActive(false);

            ShopItems[Player.SaveData.CurrentSkins[(int)type]].AcceptImage.gameObject.SetActive(false);

            Player.ChangeSkin(type, index);

            ShopItems[Player.SaveData.CurrentSkins[(int)type]].AcceptImage.gameObject.SetActive(true);

            return;
        }


        if (Player.CoinsChange(-(int)cost))
        {
            Player.SaveData.OpenedSkins[(int)type].Skins[index] = true;

            ShopItems[index].CostGO.SetActive(false);

            BuyOrEquip(type, index, cost);
        }

    }

}

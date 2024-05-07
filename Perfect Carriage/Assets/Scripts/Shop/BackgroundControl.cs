using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    private DataController _player;
    private ShopSkinContainer _shopSkinContainer;

    [SerializeField]
    private List<UnityEngine.UI.Image> _background;

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

    public void Initialize(DataController player, ShopSkinContainer shopSkinContainer)
    {
        _player = player;

        _shopSkinContainer = shopSkinContainer;

        _player.OnSkinChanged += ChangeBackground;

        ChangeBackground();
    }

    public void ChangeBackground()
    {
        foreach (var item in _background)
        {
            item.sprite = _shopSkinContainer.MainBackgrounds[_player.SaveData.CurrentSkins[(int)SkinType.mainBackground]];

        }
    }

    private void OnDestroy()
    {
        _player.OnSkinChanged -= ChangeBackground;
    }


}

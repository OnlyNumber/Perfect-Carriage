using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTrainControl : MonoBehaviour
{
    private DataController _player;
    private ShopSkinContainer _shopSkinContainer;

    [SerializeField]
    private List<GameObject> _background;

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
        _background[_player.SaveData.CurrentSkins[(int)SkinType.trainBackground]].SetActive(true);
    }

    private void OnDestroy()
    {
        _player.OnSkinChanged -= ChangeBackground;
    }
}

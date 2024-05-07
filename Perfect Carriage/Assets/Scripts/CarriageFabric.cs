using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CarriageFabric")]
public class CarriageFabric : ScriptableObject
{

    public CarriageControl prefab;

    public Sprite EconomSprite;

    public Sprite StandardSprite;

    public Sprite VIPSprite;

    public List<Sprite> sprites = new List<Sprite>();

    public CarriageControl Get(PassangerType type)
    {
        switch (type)
        {
            
            case PassangerType.economy:
                {
                    return Get(type, EconomSprite);
                }
            case PassangerType.standard:
                {
                    return Get(type, StandardSprite);

                }
            case PassangerType.VIP:
                {
                    return Get(type, VIPSprite);

                }
            default:
                break;
        }

        return Get(type, EconomSprite);
    }

    private CarriageControl Get(PassangerType type, Sprite sprite)
    {
        CarriageControl transfer = Instantiate(prefab);

        transfer.CarriageType = type;
        transfer.spriteRenderer.sprite = sprite;

        return transfer;

    }

}

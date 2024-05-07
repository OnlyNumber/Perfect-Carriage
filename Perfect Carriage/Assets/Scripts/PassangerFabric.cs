using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PassangerFabric")]
public class PassangerFabric : ScriptableObject
{

    //public Passanger prefab;

    public List<Passanger> sprites = new List<Passanger>();

    public Passanger Get(PassangerType type)
    {
        /*switch (type)
        {

            case PassangerType.freeRider:
                {
                    Passanger transfer = Instantiate(sprites[(int)type]);

                    transfer.type = type;

                    return transfer;
                }

            case PassangerType.economy:
                {
                    Passanger transfer = Instantiate(sprites[(int)type]);

                    transfer.type = type;

                    return transfer;
                }
            case PassangerType.standard:
                {
                    Passanger transfer = Instantiate(sprites[(int)type]);

                    transfer.type = type;

                    return transfer;

                }
            case PassangerType.VIP:
                {
                    Passanger transfer = Instantiate(sprites[(int)type]);

                    transfer.type = type;

                    return transfer;

                }
            default:
                break;
        }*/

        Passanger transfer = Instantiate(sprites[(int)type]);

        transfer.type = type;

        return transfer;

        //return Instantiate(sprites[0]);
    }

   /* private Passanger Get(PassangerType type/*, Sprite sprite)
    {
        Passanger transfer = Instantiate(sprites[(int)type]);

        transfer.type = type;
        //transfer.sprite.sprite = sprite;

        return transfer;

    }*/

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passanger : MonoBehaviour
{
    public PassangerType type;

    public CarriageControl myCarriage;

    public void ChangeCarriage(CarriageControl carriage)
    {
        if(myCarriage != null)
        {
            myCarriage.RemovePassanger(this);
        }

        if(carriage == null)
        {
            return;
        }


        transform.SetParent(carriage.transform);

        myCarriage = carriage;

        carriage.AddPasssanger(this);

    }
    

}

public enum PassangerType
{
    freeRider,
    economy,
    standard,
    VIP
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CarriageData
{
    public PassangerType type;

    public int UpgradePlacesAmount;

    public int UpgradeCostPerPassanger;

    public CarriageData()
    {
        type = PassangerType.economy;
    }
}

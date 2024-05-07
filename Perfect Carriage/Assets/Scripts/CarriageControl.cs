using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageControl : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public PassangerType CarriageType;

    public List<Passanger> Passangers = new List<Passanger>();

    public float CostPassangerIncreacer;

    public int CanAccommodate;

    public TMPro.TMP_Text amountText;

    public void RemovePassanger(Passanger myPassanger)
    {
        Passangers.Remove(myPassanger);

        amountText.text = Passangers.Count + " | " + CanAccommodate;

    }

    public void AddPasssanger(Passanger myPassanger)
    {
        Passangers.Add(myPassanger);
        amountText.text = Passangers.Count + " | " + CanAccommodate;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    public CarriageFabric carriageFabric;

    public Transform trainPoint;

    public List<CarriageControl> CarriagesPool = new List<CarriageControl>();

    public float distanceBetweenCarriages;

    //public int carriagesAmount;

    public System.Action OnCreateCarriages;

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

    private void Initialize()
    {
        for (int i = 0; i < DataController.Instance.SaveData.CariageDatas.Count; i++)
        {
            CarriageControl transferCariage = carriageFabric.Get(DataController.Instance.SaveData.CariageDatas[i].type);

            transferCariage.transform.SetParent(trainPoint);

            transferCariage.CostPassangerIncreacer = 0.8f + DataController.Instance.SaveData.CariageDatas[i].UpgradeCostPerPassanger * 0.1f; 

            transferCariage.CanAccommodate = DataController.Instance.SaveData.CariageDatas[i].UpgradePlacesAmount + ConstantData.DEFAULT_TRAIN_PLACES;

            CarriagesPool.Add(transferCariage);

            transferCariage.transform.position = new Vector3(distanceBetweenCarriages * i, 0, 0);
        }

        OnCreateCarriages?.Invoke();

    }

}

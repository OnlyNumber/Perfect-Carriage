using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassangerSpawner : MonoBehaviour
{
    [SerializeField]
    private PassangerFabric _passangerFabric;

    //public int amountCariages;

    public int amountPassangers;

    public float distance;

    public TrainController trainController;

    public float IncreaseCoeficient = 1;

    public SkillsControl skillsControl;

    private void Awake()
    {
        trainController.OnCreateCarriages += SpawnCarrages;
    }

    public void SpawnCarrages()
    {
        bool isThiefSpawned;

        Passanger transferPassanger;

        PassangerType passangerType;

        Dictionary<PassangerType, int> carriagesTypeAmount = new Dictionary<PassangerType, int>();

        if(skillsControl.skillCooldown[2] == DataController.Instance.SaveData.SkillCooldowns[2])
        {
            IncreaseCoeficient = 1.5f;
        }


        carriagesTypeAmount.Add(PassangerType.economy, 1);
        carriagesTypeAmount.Add(PassangerType.standard, 0);
        carriagesTypeAmount.Add(PassangerType.VIP, 0);

        for (int index = 0; index < DataController.Instance.SaveData.CariageDatas.Count; index++)
        {
                switch (DataController.Instance.SaveData.CariageDatas[index].type)
                {

                    case PassangerType.economy:
                        {
                            carriagesTypeAmount[PassangerType.economy]++;
                            break;
                        }
                    case PassangerType.standard:
                        {
                            carriagesTypeAmount[PassangerType.economy]++;
                            carriagesTypeAmount[PassangerType.standard]++;
                            break;
                        }
                    case PassangerType.VIP:
                        {
                            carriagesTypeAmount[PassangerType.economy]++;
                            carriagesTypeAmount[PassangerType.standard]++;
                            carriagesTypeAmount[PassangerType.VIP]++;
                            break;
                        }

                }
        }


        for (int i = 0; i < DataController.Instance.SaveData.CariageDatas.Count; i++)
        {
            amountPassangers = (int)(Random.Range(2, (DataController.Instance.SaveData.CariageDatas[i].UpgradePlacesAmount + ConstantData.DEFAULT_TRAIN_PLACES)) * IncreaseCoeficient); //* 1.5f);

            isThiefSpawned = false;

            for (int y = 0; y < amountPassangers; y++)
            {
                if (!isThiefSpawned)
                {

                    if (Random.Range(0, 2) < 1)
                    {
                        passangerType = PassangerType.freeRider;
                    }
                    else
                    {
                        passangerType = GetPassangerByType(carriagesTypeAmount);
                    }

                    transferPassanger = _passangerFabric.Get(passangerType);

                    isThiefSpawned = true;
                }
                else
                {
                    passangerType = GetPassangerByType(carriagesTypeAmount);

                    transferPassanger = _passangerFabric.Get(passangerType);
                }


                transferPassanger.transform.position = new Vector3(distance * i + Random.Range(-(distance - 1) / 2, (distance - 1) / 2), Random.Range(-2f, -4f), 0);

                transferPassanger.ChangeCarriage(trainController.CarriagesPool[i]);
            }

        }

    }

    public PassangerType GetPassangerByType(Dictionary<PassangerType, int> passangerChances)
    {
        int sum = 0;

        for (PassangerType type = PassangerType.economy; type <= PassangerType.VIP; type++)
        {
            //Debug.Log("Sum: " + passangerChances[type]);
            sum += passangerChances[type];
        }



        //Debug.Log("Sum equal: " + sum);

        sum += 1;

        int randomChance = Random.Range(1, sum);

        for (PassangerType type = PassangerType.economy; type <= PassangerType.VIP; type++)
        {
            if (GetPreviousSum(passangerChances, type) <= randomChance && randomChance <= (GetPreviousSum(passangerChances, type) + passangerChances[type]))
            {
                //Debug.Log("Success: " + randomChance);
                return type;
            }
            /*else
            {
                Debug.Log(type + " Error: " + GetPreviousSum(passangerChances, type) + " < " + randomChance + " < "+ (GetPreviousSum(passangerChances, type) + passangerChances[type]) + " passanger Chance " + passangerChances[type]);
            }*/

        }

        //Debug.Log("Error: " + randomChance);

        return PassangerType.economy;
    }

    public int GetPreviousSum(Dictionary<PassangerType, int> passangerChances, PassangerType passangerType)
    {
        int PreviousSum = 0;

        for (PassangerType type = PassangerType.economy; type < passangerType; type++)
        {
            PreviousSum += passangerChances[type];
        }

        //Debug.Log("PreviousSum " + PreviousSum);

        return PreviousSum;

    }


}

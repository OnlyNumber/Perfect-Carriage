using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsControl : MonoBehaviour
{
    public TrainController train;

    public GameManager gameManager;

    public List<int> skillCooldown = new List<int>()
    {
        3,
        5,
        4
    };

    public List<UnityEngine.UI.Button> skillButtons;




    private void Start()
    {
        gameManager.OnFinishGame += MinusCooldown;

        if (DataController.Instance.IsLoaded)
        {
            Initialize();
        }
        else
        {
            DataController.Instance.OnDataLoaded += Initialize;
        }
    }

    public void MinusCooldown()
    {
        for (int i = 0; i < DataController.Instance.SaveData.SkillCooldowns.Count; i++)
        {
            DataController.Instance.SaveData.SkillCooldowns[i]--;
        }
    }

    public void Initialize()
    {
        List<System.Action> skillsList = new List<System.Action>()
        {
            IncreaseType,
            SortAllPassangers,
            IncreasePassangersCount
        };

        for (int i = 0; i < 3; i++)
        {
            if (DataController.Instance.SaveData.SkillCooldowns[i] > 0)
            {
                skillButtons[i].interactable = false;
            }
            else
            {
                skillButtons[i].interactable = true;

                int index = i;

                skillButtons[i].onClick.AddListener(skillsList[i].Invoke);
                skillButtons[i].onClick.AddListener(() => skillButtons[index].interactable = false);
                
            }
        }
    }

    public void IncreaseType()
    {
        DataController.Instance.SaveData.SkillCooldowns[0] = skillCooldown[0];

        Debug.Log("IncreaseType");

        foreach (var item in train.CarriagesPool)
        {
            if (item.CarriageType != PassangerType.VIP)
            {
                item.CarriageType++;
            }
        }
    }

    public void SortAllPassangers()
    {
        DataController.Instance.SaveData.SkillCooldowns[1] = skillCooldown[1];


        List<Passanger> passangersPool = new List<Passanger>();

        foreach (var carriage in train.CarriagesPool)
        {
            foreach (var passanger in carriage.Passangers)
            {
                if (passanger.type != PassangerType.freeRider)
                    passangersPool.Add(passanger);
            }
        }

        foreach (var item in passangersPool)
        {
            item.ChangeCarriage(null);
        }

        int carriageIndex = 0;

        bool isInterrupted = false;

        bool isChanged = false;

        int NumberToBreak = 0;

        do
        {
            //carriageIndex = 0;
            isChanged = false;

            for (int i = 0; i < passangersPool.Count; i++)
            {
                if (train.CarriagesPool[carriageIndex].Passangers.Count < train.CarriagesPool[carriageIndex].CanAccommodate &&
                    train.CarriagesPool[carriageIndex].CarriageType == passangersPool[i].type)
                {
                    Debug.Log("Bliaaaaaaaaaaa: " + i);

                    passangersPool[i].ChangeCarriage(train.CarriagesPool[carriageIndex]);

                    passangersPool[i].transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), passangersPool[i].transform.localPosition.y);

                    passangersPool.RemoveAt(i);
                    isChanged = true;
                    isInterrupted = true;
                    break;

                }
                else if (train.CarriagesPool[carriageIndex].Passangers.Count >= train.CarriagesPool[carriageIndex].CanAccommodate)
                {
                    Debug.Log("if pool is over changed");
                    carriageIndex++;
                    isChanged = true;
                    break;
                }
            }

            if (passangersPool.Count > 0 && !isChanged)
            {
                Debug.Log("Is stopped changing");
                carriageIndex++;
            }

            if (NumberToBreak == 250)
            {
                Debug.Log("First Not work eternal loop");
                break;
            }
            else
            {
                Debug.Log("Numberrr ++");
                NumberToBreak++;
            }

            if (isChanged)
            {
                Debug.Log("Carriage count: " + carriageIndex);
            }

        } while (carriageIndex != train.CarriagesPool.Count && isInterrupted && passangersPool.Count != 0);

        if (passangersPool.Count == 0)
        {
            return;
        }

        carriageIndex = 0;

        isInterrupted = false;

        isChanged = false;

        NumberToBreak = 0;

        do
        {
            //carriageIndex = 0;
            isChanged = false;

            for (int i = 0; i < passangersPool.Count; i++)
            {
                if (train.CarriagesPool[carriageIndex].Passangers.Count < train.CarriagesPool[carriageIndex].CanAccommodate &&
                    train.CarriagesPool[carriageIndex].CarriageType >= passangersPool[i].type)
                {
                    passangersPool[i].ChangeCarriage(train.CarriagesPool[carriageIndex]);

                    passangersPool[i].transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), passangersPool[i].transform.localPosition.y);

                    passangersPool.RemoveAt(i);
                    isChanged = true;
                    isInterrupted = true;
                    break;

                }
                else if (train.CarriagesPool[carriageIndex].Passangers.Count > train.CarriagesPool[carriageIndex].CanAccommodate)
                {
                    carriageIndex++;
                    isChanged = true;
                    break;
                }
            }

            if (passangersPool.Count > 0 && !isChanged)
            {
                carriageIndex++;
            }

            if (NumberToBreak == 250)
            {
                Debug.Log("Not work eternal loop");
                break;
            }
            else
            {
                NumberToBreak++;
            }

            Debug.Log("Carriage count: " + carriageIndex);


        } while (carriageIndex != train.CarriagesPool.Count && isInterrupted);

        if (passangersPool.Count == 0)
        {
            Debug.Log("Return after second");

            return;
        }

        //Debug.Log("No retrun after second");

        //return;

        carriageIndex = 0;

        isInterrupted = false;

        isChanged = false;

        NumberToBreak = 0;

        do
        {
            //for (int i = 0; i < passangersPool.Count; i++)
            //{
            if (train.CarriagesPool[carriageIndex].Passangers.Count < train.CarriagesPool[carriageIndex].CanAccommodate)
            {
                passangersPool[0].ChangeCarriage(train.CarriagesPool[carriageIndex]);

                passangersPool[0].transform.localPosition = new Vector3(Random.Range(-1.5f, 1.5f), passangersPool[0].transform.localPosition.y);

                passangersPool.RemoveAt(0);

                //break;
            }
            else
            {
                carriageIndex++;
                //break;
            }

            //}

            if (NumberToBreak == 250)
            {
                Debug.Log("Third Not work eternal loop");
                return;
            }
            else
            {
                Debug.Log("Carriage index: " + carriageIndex);

                NumberToBreak++;
            }


        } while (carriageIndex != train.CarriagesPool.Count && passangersPool.Count != 0);

        carriageIndex = 0;

        NumberToBreak = 0;

        if (passangersPool.Count == 0)
        {
            Debug.Log("Return after third");

            return;
        }

        do
        {
            passangersPool[0].ChangeCarriage(train.CarriagesPool[carriageIndex]);

            passangersPool[0].transform.position = new Vector3(Random.Range(-1.5f, 1.5f), passangersPool[0].transform.localPosition.y);

            passangersPool.RemoveAt(0);

            carriageIndex++;

            if (carriageIndex == train.CarriagesPool.Count)
            {
                carriageIndex = 0;
            }

            if (NumberToBreak == 250)
            {
                Debug.Log("Fourth Not work eternal loop");
                break;
            }
            else
            {
                NumberToBreak++;
            }

        } while (passangersPool.Count != 0);
    }

    public void IncreasePassangersCount()
    {
        DataController.Instance.SaveData.SkillCooldowns[2] = skillCooldown[2];
    }


}

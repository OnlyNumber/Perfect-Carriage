using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float needTime;

    public double currentTime;

    public TMPro.TMP_Text _timerText;

    public Image _timerBar;

    public List<int> info = new List<int>();

    public TrainController trainController;

    public bool IsGame;

    public PanelActivator ResultPanel;

    public TMPro.TMP_Text ResultText;

    public float SumReward = 0;

    public Dictionary<PassangerType, float> RewardPassangerType = new Dictionary<PassangerType, float>()
    {
        {PassangerType.economy, 10 },
        {PassangerType.standard, 20 },
        {PassangerType.VIP, 30 },

    };

    public System.Action OnFinishGame;

    private void Start()
    {
        currentTime = needTime;
    }

    private void Update()
    {
        if (IsGame)
        {
            currentTime -= Time.deltaTime;

            _timerText.text = System.TimeSpan.FromSeconds(currentTime).ToString("\\:mm\\:ss");

            if (currentTime <= 0)
            {

                FinishGame();
            }
        }

    }

    public void ChangeIsGame(bool state)
    {
        IsGame = state;
    }

    public void FinishGame()
    {
        IsGame = false;

        ResultPanel.Activate(true);

        int RightPassangers = 0;

        int WrongPassangers = 0;

        int GetExceedingLimitPassanger = 0;

        int CurrentLimitCarriage = 0;

        int FreeRidersCount = 0;

        for (int i = 0; i < trainController.CarriagesPool.Count; i++)
        {
            RightPassangers += GetRightPassangers(trainController.CarriagesPool[i]);
            WrongPassangers += GetWrongPassangers(trainController.CarriagesPool[i]);

            CurrentLimitCarriage = GetExceedingLimitPassangers(trainController.CarriagesPool[i]);

            if (CurrentLimitCarriage > 0)
            {
                GetExceedingLimitPassanger += CurrentLimitCarriage;

            }

            FreeRidersCount += GetFreeRiders(trainController.CarriagesPool[i]);

        }

        ResultText.text += "Right passangers: " + RightPassangers + "\n \n";
        ResultText.text += "Wrong passangers: " + WrongPassangers + "\n \n";

        ResultText.text += "Exceeding the limit passangers: " + GetExceedingLimitPassanger + "\n \n";

        float PenaltyPercentage = FreeRidersCount;

        PenaltyPercentage *= 0.1f;

        if(PenaltyPercentage > 0.5f )
        {
            PenaltyPercentage = 0.5f;
        }


        ResultText.text += "Free riders: " + FreeRidersCount + " Penalty: " + PenaltyPercentage * 100 + "%"+  "\n \n";

        ResultText.text += "Reward for game: " + (int)(SumReward * (1 - PenaltyPercentage)) + "\n \n";

        Debug.Log(SumReward);

        DataController.Instance.CoinsChange((int)(SumReward * (1 - PenaltyPercentage)));

        OnFinishGame?.Invoke();

    }

    public int GetFreeRiders(CarriageControl carriage)
    {
        int freeRidersCount = 0;

        for (int i = 0; i < carriage.CanAccommodate; i++)
        {
            if (i >= carriage.Passangers.Count)
            {
                break;
            }

            if (carriage.Passangers[i] != null && PassangerType.freeRider == carriage.Passangers[i].type)
            {
                freeRidersCount++;
            }
        }

        return freeRidersCount;
    }

    public int GetRightPassangers(CarriageControl carriage)
    {
        int rightPassangers = 0;

        for (int i = 0; i < carriage.CanAccommodate; i++)
        {
            if (i >= carriage.Passangers.Count)
            {
                break;
            }

            if (carriage.Passangers[i] != null && carriage.CarriageType <= carriage.Passangers[i].type)
            {
                SumReward += RewardPassangerType[carriage.Passangers[i].type] * carriage.CostPassangerIncreacer;
                rightPassangers++;
            }
        }
        return rightPassangers;
    }

    public int GetWrongPassangers(CarriageControl carriage)
    {
        int wrongPassangers = 0;

        for (int i = 0; i < carriage.CanAccommodate; i++)
        {
            if (i >= carriage.Passangers.Count)
            {
                break;
            }

            if (carriage.Passangers[i] != null && carriage.CarriageType != carriage.Passangers[i].type)
            {
                wrongPassangers++;
            }


        }
        return wrongPassangers;
    }

    public int GetExceedingLimitPassangers(CarriageControl carriage)
    {
        return (carriage.Passangers.Count - carriage.CanAccommodate);
    }


    public void CreateCarriages(List<int> info)
    {



    }




}

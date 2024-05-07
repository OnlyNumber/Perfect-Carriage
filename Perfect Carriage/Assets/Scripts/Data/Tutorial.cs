using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PanelActivator TutorialPanel;

    private void Start()
    {
        DataController.Instance.OnDataLoaded += Initialize;
    }

    public void Initialize()
    {
        if (DataController.Instance.SaveData.IsFirstLog)
        {
            DataController.Instance.SaveData.IsFirstLog = false;
            TutorialPanel.gameObject.SetActive(true);
        }

    }

}

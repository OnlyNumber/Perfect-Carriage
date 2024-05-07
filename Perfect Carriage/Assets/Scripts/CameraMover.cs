using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public Transform Camera;

    public TrainController trainController;

    public Vector3 ChangePosition;

    public float timeToChangePosition;

    public int CurrentIndex;

    private void Start()
    {
        ChangePosition = new Vector3(CurrentIndex * 3.65f, 0, -10);
    }

    public void MoveCameraToCarriage(int index)
    {
        if(CurrentIndex + index < 0 || CurrentIndex + index >= DataController.Instance.SaveData.CariageDatas.Count)
        {
            return;
        }

        CurrentIndex += index;
        ChangePosition = new Vector3(CurrentIndex * 3.65f, 0,-10);

    }


    private void Update()
    {
        Camera.position = Vector3.Lerp(Camera.position, ChangePosition, timeToChangePosition * Time.deltaTime);
    }




    public void ChangeXPosition(float x)
    {
        Camera.position += new Vector3(x, 0, 0);
    }
}

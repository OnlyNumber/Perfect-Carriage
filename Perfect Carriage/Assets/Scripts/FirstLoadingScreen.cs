using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLoadingScreen : MonoBehaviour
{
    [SerializeField]
    private LoadingSceneController _loadingScreen;

    void Start()
    {
        _loadingScreen.LoadMainScene();
    }

    
}

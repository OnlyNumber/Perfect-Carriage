using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    public PanelActivator LoadingPanel;

    public Image LoadingBar;

    public float SpeedLoadingBar;

    private float indicator;
    
    public void LoadMainScene()
    {
        StartCoroutine(LoadSmth(ConstantData.MAIN_MENU_SCENE));
    }

    public void LoadGameScene()
    {
        StartCoroutine( LoadSmth(ConstantData.GAME_SCENE));
    }


    public IEnumerator LoadSmth(string nameScene)
    {
        var scene = SceneManager.LoadSceneAsync(nameScene);

        scene.allowSceneActivation = false;

        LoadingPanel.Activate(true);

        indicator = 0;

        yield return new WaitForSeconds(LoadingPanel.DurationMove);

        while (!scene.isDone)
        {
            if(scene.progress >= 0.9f && LoadingBar.rectTransform.rect.width >= 700)
            {
                scene.allowSceneActivation = true;
            }
            else
            {
                indicator += 700 * SpeedLoadingBar * Time.deltaTime;

                LoadingBar.rectTransform.sizeDelta = new Vector2(indicator, LoadingBar.rectTransform.rect.height);

                //LoadingBar.rectTransform.rect.Set(0,0, indicator, LoadingBar.rectTransform.rect.height);
            }

            yield return null;
        }



    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{ 
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickVSButton()
    {
        SceneManager.LoadScene("GameScene 1");
    }

    public void OnClickReturnButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClickRetryButton()
    {
        Scene loadScene = SceneManager.GetActiveScene();        
        SceneManager.LoadScene(loadScene.name);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneChange : MonoBehaviour
{
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ChangeHowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void ChangeRankingScene()
    {
        SceneManager.LoadScene("Ranking");
    }
    public void ChangeMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

}

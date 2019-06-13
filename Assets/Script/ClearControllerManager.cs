using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClearControllerManager : MonoBehaviour
{
    public Valve.VR.SteamVR_Input_Sources handType;

    public GameObject LaserPrefab;
    ClearLaser laser;
    public GameObject Gage;
    public Text ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        int Stage = PlayerPrefs.GetInt("Stage");
        float playerTime = PlayerPrefs.GetFloat("PlayerTime");
        Stage stage = new Stage();
        Ranking ranking = new Ranking();
        stage.SaveStage(Stage + 1);
        ranking.SaveRanking(new Rank(Stage-1, (int)playerTime));
        Debug.Log(Stage + " " + playerTime);
        laser = Instantiate(LaserPrefab, this.transform, false).AddComponent<ClearLaser>();
        ScoreText.text = "Stage " + Stage + "     " + string.Format("{0:D3}", (int)(playerTime / 60)) + ":" + string.Format("{0:D2}", (int)(playerTime % 60));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hit_ob = laser.OnLaser();
        if (hit_ob != null)
        {
            if (hit_ob.name == "Button")
            {
                if (Gage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (Gage.GetComponent<Image>().fillAmount < 1)
                    Gage.GetComponent<Image>().fillAmount += 0.01f;
                else
                    SceneManager.LoadScene("StageSelect");
            }
            else
                Gage.GetComponent<Image>().fillAmount = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingControllerManager : MonoBehaviour
{
    public Valve.VR.SteamVR_Input_Sources handType;

    public GameObject LaserPrefab;
    RankingLaser laser;
    public GameObject exitGage;
    public Text rankText;
    public Text buttonText;
    public Text stageText;

    private List<Rank>[] last_ranking;

    private int max_stage = 3;
    private int now_stage = 0;
    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(LaserPrefab, this.transform, false).AddComponent<RankingLaser>();


        last_ranking = new List<Rank>[max_stage];
        for (int j = 0; j < max_stage; j++)
        {
            last_ranking[j] = new List<Rank>();
        }
        Ranking ranking = new Ranking();


        List<Rank> rankings = ranking.LoadRanking();

        for (int j = 0; j < max_stage; j++)
        {
            for (int i = 0; i < rankings.Count; i++)
            {
                if (rankings[i].stage == j)
                {
                    last_ranking[j].Add(rankings[i]);
                }
            }
        }

        for (int j = 0; j < max_stage; j++)
        {
            last_ranking[j].Sort((x, y) => x.time.CompareTo(y.time));
            for (int i = 0; i < last_ranking[j].Count; i++)
            {
                Debug.Log(j + " " + last_ranking[j][i]);
            }
        }
        Debug.Log(last_ranking[now_stage].Count);
        for (int i = 0; i < last_ranking[now_stage].Count; i++)
        {
            rankText.text += (i + 1) + ". " + last_ranking[now_stage][i].ToString() + "\n";
        }
        stageText.text = "Ranking [ Stange" + (now_stage + 1) + " ]";
        now_stage++;
        if (now_stage == max_stage - 1)
        {
            buttonText.text = "Exit";
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hit_ob = laser.OnLaser();
        if (hit_ob != null)
        {
            
            if (hit_ob.tag == "exit")
            {
                if (exitGage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (exitGage.GetComponent<Image>().fillAmount < 1)
                    exitGage.GetComponent<Image>().fillAmount += 0.01f;
                else
                {
                    if (now_stage == max_stage)
                    {
                        SceneManager.LoadScene("Menu");
                    }
                    else
                    {
                        rankText.text = "";
                        for (int i = 0; i < last_ranking[now_stage].Count; i++)
                        {
                            rankText.text += (i + 1) + ". " + last_ranking[now_stage][i].ToString() + "\n";
                        }
                        stageText.text = "Ranking [ Stange" + (now_stage + 1) + " ]";
                        now_stage++;
                        if (now_stage == max_stage)
                        {
                            buttonText.text = "Exit";
                        }
                    }
                    exitGage.GetComponent<Image>().fillAmount = 0;
                }
            }
            else
            {
                exitGage.GetComponent<Image>().fillAmount = 0;
            }
        }
    }
}

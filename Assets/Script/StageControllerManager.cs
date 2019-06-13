using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageControllerManager : MonoBehaviour
{
    public Valve.VR.SteamVR_Input_Sources handType;

    public GameObject LaserPrefab;
    StageLaser laser;
    public GameObject startGage;
    public GameObject howGage;
    public GameObject rankingGage;
    public GameObject quitGage;
    private List<int> stageList;
    // Start is called before the first frame update
    void Start()
    {
        Stage stageManager = new Stage();
        stageList = stageManager.LoadStage();
        for(int i = 1; i <= 10; i++)
        {
            if (stageList[i - 1] != 0)
            {
                GameObject image = GameObject.Find("Image" + i);
                image.GetComponent<Image>().enabled = false;
            }
        }
        laser = Instantiate(LaserPrefab, this.transform, false).AddComponent<StageLaser>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject hit_ob = laser.OnLaser();
        if (hit_ob != null)
        {
            if (hit_ob.name.StartsWith("Cube"))
            {
                if(stageList[int.Parse(hit_ob.name.Substring(4))-1] != 0)
                {
                    GameObject gage = GameObject.Find(hit_ob.name + "Gage");

                    if (gage.GetComponent<Image>().fillAmount == 0)
                        GetComponent<AudioSource>().Play();
                    if (gage.GetComponent<Image>().fillAmount < 1)
                        gage.GetComponent<Image>().fillAmount += 0.01f;
                    else
                    {
                        Debug.Log("stage" + hit_ob.name.Substring(4));
                        SceneManager.LoadScene("stage" + hit_ob.name.Substring(4));
                    }
                }
            }
            else if (hit_ob.name == "back")
            {
                GameObject back = GameObject.Find("backGage");
                if (back.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (back.GetComponent<Image>().fillAmount < 1)
                    back.GetComponent<Image>().fillAmount += 0.01f;
                else
                    SceneManager.LoadScene("Menu");
            }
            else
            {
                for (int i = 1; i <= 10; i++)
                {
                    GameObject gage = GameObject.Find("Cube" + i + "Gage");
                    gage.GetComponent<Image>().fillAmount = 0;
                }
                GameObject back = GameObject.Find("backGage");
                back.GetComponent<Image>().fillAmount = 0;
            }
        }
    }
}

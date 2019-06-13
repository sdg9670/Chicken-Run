using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControllerManager : MonoBehaviour
{
    public Valve.VR.SteamVR_Input_Sources handType;

    public GameObject LaserPrefab;
    MenuLaser laser;
    public GameObject startGage;
    public GameObject howGage;
    public GameObject rankingGage;
    public GameObject quitGage;

    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(LaserPrefab, this.transform, false).AddComponent<MenuLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hit_ob = laser.OnLaser();
        if (hit_ob != null)
        {
            if (hit_ob.tag == "start")
            {
                if (startGage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (startGage.GetComponent<Image>().fillAmount < 1)
                    startGage.GetComponent<Image>().fillAmount += 0.01f;
                else
                    SceneManager.LoadScene("StageSelect");
            }
            else if (hit_ob.tag == "how")
            {
                if (howGage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (howGage.GetComponent<Image>().fillAmount < 1)
                    howGage.GetComponent<Image>().fillAmount += 0.01f;
                else
                    SceneManager.LoadScene("HowToPlay");
            }
            else if (hit_ob.tag == "ranking")
            {
                if (rankingGage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (rankingGage.GetComponent<Image>().fillAmount < 1)
                    rankingGage.GetComponent<Image>().fillAmount += 0.01f;
                else
                    SceneManager.LoadScene("Ranking");
            }
            else if (hit_ob.tag == "quit")
            {
                if (quitGage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (quitGage.GetComponent<Image>().fillAmount < 1)
                    quitGage.GetComponent<Image>().fillAmount += 0.01f;
                else
                    Application.Quit();
            }
            else
            {
                startGage.GetComponent<Image>().fillAmount = 0;
                howGage.GetComponent<Image>().fillAmount = 0;
                rankingGage.GetComponent<Image>().fillAmount = 0;
                quitGage.GetComponent<Image>().fillAmount = 0;
            }
        }
    }
}

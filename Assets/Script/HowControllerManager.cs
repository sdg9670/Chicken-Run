using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowControllerManager : MonoBehaviour
{
    public Valve.VR.SteamVR_Input_Sources handType;

    public GameObject LaserPrefab;
    HowLaser laser;
    public GameObject nextGage;
    public GameObject FirstHow;
    public GameObject SecondHow;
    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(LaserPrefab, this.transform, false).AddComponent<HowLaser>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hit_ob = laser.OnLaser();
        if (hit_ob != null)
        {
            if (hit_ob.tag == "next")
            {
                if (nextGage.GetComponent<Image>().fillAmount == 0)
                    GetComponent<AudioSource>().Play();
                if (nextGage.GetComponent<Image>().fillAmount < 1)
                    nextGage.GetComponent<Image>().fillAmount += 0.01f;
                else
                {
                    if (FirstHow.activeSelf)
                    {
                        nextGage.GetComponent<Image>().fillAmount = 0;
                        FirstHow.SetActive(false);
                        SecondHow.SetActive(true);
                    }
                    else if(SecondHow.activeSelf)
                    {
                        SceneManager.LoadScene("Menu");
                    }
                }
            }
            else
            {
                nextGage.GetComponent<Image>().fillAmount = 0;
            }
        }
    }
}

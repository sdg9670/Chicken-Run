using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainControllerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    public Valve.VR.SteamVR_Input_Sources handType;
    public Valve.VR.SteamVR_Action_Boolean grapAction;
    public Valve.VR.SteamVR_Action_Boolean backGrap;
    public Valve.VR.SteamVR_Action_Vector2 joystickPos;
    public Valve.VR.SteamVR_Action_Boolean ModeChange;
    public Valve.VR.SteamVR_Action_Boolean useItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (handType == Valve.VR.SteamVR_Input_Sources.RightHand)
        {
            if (grapAction.GetLastStateDown(handType))
            {
                player.GetComponent<PlayerControll>().jump();
            }
            if (backGrap.GetLastStateDown(handType))
            {
                player.transform.position = new Vector3(62, 0, -130);
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            if (ModeChange.GetLastStateDown(handType))
            {
                camera.GetComponent<CameraController>().modeChange();
            }
            if (useItem.GetLastStateDown(handType))
            {
                player.GetComponent<PlayerControll>().UseItem();
            }
        }
        if (handType == Valve.VR.SteamVR_Input_Sources.LeftHand)
        {
            if(grapAction.GetLastStateUp(handType))
            {
                player.GetComponent<PlayerControll>().speedChange(false);
            }
            else if (grapAction.GetLastStateDown(handType))
            {
                player.GetComponent<PlayerControll>().speedChange(true);
            }
            if (ModeChange.GetLastStateDown(handType))
            {
                SceneManager.LoadScene("StageSelect");
            }
            player.GetComponent<PlayerControll>().move(joystickPos.GetAxis(handType).x, joystickPos.GetAxis(handType).y);
        }
    }
}

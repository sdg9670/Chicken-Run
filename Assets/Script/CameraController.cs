using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    private bool mode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!mode)
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, camera.transform.rotation.eulerAngles.y, 0));
            transform.position = player.transform.position + player.transform.rotation * new Vector3(0, 0, 0.6f);
        } else
        {
            player.transform.rotation = Quaternion.Euler(new Vector3(0, camera.transform.rotation.eulerAngles.y, 0));
            transform.position = player.transform.position + player.transform.rotation * new Vector3(0, 2, -8f);
        }
    }

    public void modeChange()
    {
        if (mode)
            mode = false;
        else
            mode = true;
    }
}

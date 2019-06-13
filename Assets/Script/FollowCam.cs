using UnityEngine;
using System.Collections;
public class FollowCam : MonoBehaviour
{
    private bool mode = false;
    public GameObject player;
    private Camera _Cam;
    public Camera Cam
    {
        get
        {
            if (_Cam == null)
            {
                _Cam = GetComponent<Camera>();
            }
            return _Cam;
        }
    }
    public bool isMoving;
    public Vector3 CamOffset = Vector3.zero;
    public Vector3 ZoomOffset = Vector3.zero;
    public float senstivityX = 5;
    public float senstivityY = 1;
    public float minY = -1.5f;
    public float maxY = 50;
    public bool isZooming;
    private float currentX = 0;
    private float currentY = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(mode)
            {
                minY = -10;
                maxY = 80;
                mode = false;
                //player(true);
            }
            else
            {
                minY = -80;
                maxY = 45;
                mode = true;
                //player.SetActive(false);
            }
        }
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");
        currentX = Mathf.Repeat(currentX, 360);
        currentY = Mathf.Clamp(currentY, minY, maxY);
        isMoving = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) ? true : false;
        isZooming = Input.GetMouseButton(1);
        if (isMoving || isZooming)
        {
            UpdatePlayerRotation();
        }
    }
    void UpdatePlayerRotation()
    {
        player.transform.rotation = Quaternion.Euler(0, currentX, 0);
    }
    void LateUpdate()
    {
        Vector3 dist = isZooming ? ZoomOffset : CamOffset;
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        if (mode)
        {
            transform.position = player.transform.position + new Vector3(0.0f, 0f, 0.0f) + rotation * new Vector3(0, 1f, 0.6f);
            transform.LookAt(player.transform.position + rotation * new Vector3(0, 1f, 1f));
        }
        else
        {
            transform.position = player.transform.position + rotation * dist + new Vector3(0.0f, 1.5f, 0.0f);
            transform.LookAt(player.transform.position + new Vector3(0.0f, 1.5f, 0.0f));
        }
        if(!mode)
            CheckWall();
    }
    public LayerMask wallLayer;
    void CheckWall()
    {
        RaycastHit hit;
        Vector3 start = player.transform.position;
        Vector3 dir = transform.position - player.transform.position;
        float dist = CamOffset.z * -1;
        Debug.DrawRay(player.transform.position, dir, Color.green);
        if (Physics.Raycast(player.transform.position, dir, out hit, dist, wallLayer))
        {
            float hitDist = hit.distance;
            Vector3 sphereCastCenter = player.transform.position +(dir.normalized * hitDist) + new Vector3(0.0f, 1.5f, 0.0f);
            transform.position = sphereCastCenter;
        }
    }
}
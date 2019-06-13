using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowLaser : MonoBehaviour
{
    private HowControllerManager _controller = null;
    public Vector3 _hit_point;
    // Start is called before the first frame update
    void Start()
    {
        _controller = this.transform.parent.GetComponent<HowControllerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject OnLaser()
    {
        RaycastHit hit;
        if(Physics.Raycast(_controller.transform.position, transform.forward, out hit, 1000))
        {
            _hit_point = hit.point;
            ShowLaser(hit);
            return hit.collider.gameObject;
        }
        return null;
    }

    private void ShowLaser(RaycastHit hit)
    {
        transform.position = Vector3.Lerp(_hit_point, _controller.transform.position, .5f);
        transform.LookAt(_hit_point);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, hit.distance);
    }
}

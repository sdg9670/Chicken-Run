using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public GameObject expEffect;
    public GameObject lightOb;
    public GameObject audio;
    GameObject player;
    GameObject light;
    float start_y;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("rudy");
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        start_y = transform.position.y;
        if (Physics.Raycast(landingRay, out hit))
        {
            light = Instantiate(lightOb, hit.point, Quaternion.Euler(new Vector3(-90f, 0, 0)));
        }
        light.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0f, 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < start_y-6 && transform.position.y > start_y-15)
            light.GetComponent<Renderer>().material.color = new Color(1f, 1f, 0f, 0.1f);
        else if (transform.position.y < start_y-15)
            light.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.1f);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = null;
        ContactPoint contact = collision.contacts[0];

        effect = Instantiate(expEffect, contact.point, Quaternion.identity);
        Vector3 dis = contact.point - player.transform.position;
        Instantiate(audio, contact.point, Quaternion.identity);

        Destroy(effect, 5.0f);
        Destroy(light);
        Destroy(gameObject);


        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 3f);
        for (int i = 0; i < colliders.Length; ++i)
        {
            Rigidbody target = colliders[i].GetComponent<Rigidbody>();
            if (!target)
            {
                continue;
            }
            if(player.GetComponent<PlayerControll>().useItem != 2)
                target.AddExplosionForce(1000f, this.transform.position, 12f);

        }
    }
}

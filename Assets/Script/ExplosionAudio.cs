using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAudio : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

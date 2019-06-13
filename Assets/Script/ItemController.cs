using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateStart());
    }

    IEnumerator CreateStart()
    {
        yield return new WaitForSeconds(10f);
		Destroy(this.transform.gameObject);
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}

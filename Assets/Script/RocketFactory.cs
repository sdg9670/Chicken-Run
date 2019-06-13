using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFactory : MonoBehaviour
{
    public Transform player;
    public GameObject rocket;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateStart());
    }

    IEnumerator CreateStart()
    {
        do
        {
			if(player.position.y > 20)
			{
				RaycastHit hit;
				for (int i = 0; i < 15; i++)
				{
					float x = Random.Range(-10000.0f, 10000.0f) / 200;
					float z = Random.Range(-10000.0f, 10000.0f) / 200;
					Ray landingRay = new Ray(new Vector3(player.position.x + x, player.position.y, player.position.z + z), Vector3.up);
					float y = player.position.y + 30f;
					if (Physics.Raycast(landingRay, out hit, y))
					{
						y = hit.point.y - 2;
					}
					landingRay = new Ray(new Vector3(player.position.x + x, player.position.y+1f, player.position.z + z), Vector3.down);
					if (Physics.Raycast(landingRay, out hit, 10f) && y > 10)
						Instantiate(rocket, new Vector3(player.position.x + x, y, player.position.z + z), Quaternion.Euler(180f, 0, 0));

				}
            }
            yield return new WaitForSeconds(0.2f);
        } while (true);
    }
}

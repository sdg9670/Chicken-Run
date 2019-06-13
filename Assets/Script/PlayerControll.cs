using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControll : MonoBehaviour
{
    private Transform transform;
    private Animator animator;
    private bool isBottom = true;
    private float moveSpd = 6.0f;
    public AudioClip jumpSound;
    public AudioClip getItemSound;
    public AudioClip useItemSound;
    AudioSource audio;
    public Image itemSlot1;
    public Image itemSlot2;
    public Image useItemImage;
    public Sprite[] image = new Sprite[3];
    private int[] item = new int[2]{0, 0};
    private float buffTime = 0;
    public int useItem = 0;
    private bool double_jump = false;
    public Text playerTimeText;
    
    private float playerTime = 0;

    // Use this for initialization
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        this.transform = GetComponent<Transform>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTime += Time.deltaTime;
        playerTimeText.text = string.Format("{0:D3}", (int)(playerTime / 60)) + ":" + string.Format("{0:D2}", (int)(playerTime % 60));
        if (useItem != 0)
        {
            buffTime += Time.deltaTime;
            if (buffTime < 5.0f)
            {
                useItemImage.fillAmount = 1.0f - 1 / 5.0f * buffTime;
            }
            else
            {
                useItemImage.fillAmount = 0;
                useItem = 0;
                buffTime = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }


        if (!isBottom)
        {
            this.animator.SetTrigger("TriggerJump");
        }
        else
        {
            this.animator.SetTrigger("TriggerWalk");
        }
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(landingRay, out hit, 0.1f))
        {
            isBottom = true;
            double_jump = false;
        }
        else
            isBottom = false;
    }
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.gameObject.tag == "item")
		{
			if(item[0] == 0)
			{
				item[0] = Random.Range(1, 4);
                SetItemImage();
            }
			else if(item[1] == 0)
			{
				item[1] = Random.Range(1, 4);
                SetItemImage();
            }
            Destroy(collision.collider.gameObject);
            audio.clip = getItemSound;
            audio.Play();
            Debug.Log(item[0] + " " + item[1]);
        }
        else if (collision.collider.gameObject.tag == "clearPoint")
        {
            PlayerPrefs.SetInt("Stage", 1);
            PlayerPrefs.SetFloat("PlayerTime", playerTime);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Clear");
        }
        else if (collision.collider.gameObject.tag == "clearPoint2")
        {
            PlayerPrefs.SetInt("Stage", 2);
            PlayerPrefs.SetFloat("PlayerTime", playerTime);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Clear");
        }
        else if (collision.collider.gameObject.tag == "clearPoint3")
        {
            PlayerPrefs.SetInt("Stage", 3);
            PlayerPrefs.SetFloat("PlayerTime", playerTime);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Clear");
        }
    }
	
    void OnCollisionStay(Collision collision)
    {
		
    }
    void OnCollisionExit(Collision collision)
    {
    }

    public void jump()
    {
        if (isBottom)
        {
            audio.clip = jumpSound;
            audio.Play();
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 1000);
            isBottom = false;
        }
        else if (useItem == 1 && !double_jump)
        {
            audio.clip = jumpSound;
            audio.Play();
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
            double_jump = true;
        }
    }

    public void move(float x, float y)
    {
        if(useItem == 3)
        {
            x = -x;
            y = -y;
        }
        if (x > 0.1f || y > 0.1f || x < -0.1f || y < -0.1f)
        {
            // 변위 값을 계산하기 위한 Vector3 변수 : 전,후진과 좌,우 이동 값을 저장
            Vector3 moveDirect = (Vector3.forward * y) + (Vector3.right * x);

            // 게임 오브젝트의 이동 처리를 편하게 할 수 있는 함수
            transform.Translate(moveDirect.normalized * Time.deltaTime * moveSpd, Space.Self);
        }
    }

    public void speedChange(bool mode)
    {

        if (mode)
        {
            this.moveSpd = 12.0f;
            animator.speed = 2.0f;
        }
        else
        {
            this.moveSpd = 6.0f;
            animator.speed = 1.0f;
        }
    }

    public void UseItem()
    {
        if (useItem == 0)
        {
            if (item[0] != 0)
            {
                useItem = item[0];
                useItemImage.sprite = image[item[0] - 1];
                if (item[1] != 0)
                {
                    item[0] = item[1];
                    item[1] = 0;
                }
                else
                    item[0] = 0;
            }
            SetItemImage();
            audio.clip = useItemSound;
            audio.Play();
        }
    }

    private void SetItemImage()
    {
        Debug.Log(item[0] + " " + item[1]);
        if (item[0] == 0)
        {
            itemSlot1.enabled = false;
        }
        else
        {
            itemSlot1.sprite = image[item[0] - 1];
            itemSlot1.enabled = true;
        }
        if (item[1] == 0)
        {
            itemSlot2.enabled = false;
        }
        else
        {
            itemSlot2.sprite = image[item[1] - 1];
            itemSlot2.enabled = true;
        }
    }
}
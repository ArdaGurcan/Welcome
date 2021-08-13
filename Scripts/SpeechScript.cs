using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechScript : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject desk;
    public GameObject coins;
    public GameObject display;
    public GameObject present;
    public GameObject bars;
    public GameObject warning;
    public GameObject[] blocks;
    public GameObject enemy;
    public GameObject displayBlock;
    public GameObject sign;
    public GameObject camera;
    public Sprite angry;
    public Sprite knife;
    public AudioSource musicPlayer;
    public PolygonCollider2D barrier;
    public AudioClip bossmusic;
    public bool paid;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, enemy.transform.position) < 4 && !paid)
        {
            speechBubble.SetActive(true);
            if(Input.GetKey(KeyCode.E))
            {
                StartCoroutine("CoinSounds");
                coins.SetActive(true);
                display.SetActive(false);
                present.SetActive(true);
                paid = true;
            }
        }
        else
        {
            speechBubble.SetActive(false);
        }
        if(camera.transform.position.y <= -16f)
        {
            desk.SetActive(false);
            coins.SetActive(false);
            enemy.GetComponent<SpriteRenderer>().sprite = angry;
        }

    }

	private void OnTriggerEnter2D(Collider2D other)
	{
        //Debug.Log(other.name);
        if(other.tag == "Trigger" && paid && !done)
        {
            barrier.enabled = true;
            done = true;
            bars.GetComponent<Animation>().Play("lock");
            StartCoroutine("AnimationSequence");

        }
	}

    IEnumerator AnimationSequence()
    {
        
        yield return new WaitForSeconds(0.6f);
        musicPlayer.clip = bossmusic;
        warning.SetActive(true);
        enemy.GetComponent<SpriteRenderer>().sprite = angry;
        displayBlock.GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(2f);
        warning.SetActive(false);
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].SetActive(false);
            //blocks[i].GetComponent<Rigidbody2D>().isKinematic = false;
        }
        camera.GetComponent<CameraScript>().followPlayer = true;
    }

    IEnumerator CoinSounds()
    {
        for (int i = 0; i < 3; i++)
        {
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.25f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public bool move;
    Rigidbody2D rb;
    float maxSpeed = 2.7f;
    float acceleration = 0.4f;
    public GameObject cam;
    public BoxCollider2D boxCollider;
    public GameObject final;
    public BoxCollider2D killCollider;
    public GameObject player;



	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
	}
        
	void Update()
    {
        if(move)
        {
            if(rb.velocity.x >= maxSpeed)
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity += new Vector2(acceleration, 0);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if(transform.position.x > 178)
        {
            final.transform.parent.gameObject.SetActive(true);
            player.GetComponent<SimplePlatformController>().hasJetpack = true;
            player.transform.GetChild(1).gameObject.SetActive(false);
            player.transform.GetChild(2).gameObject.SetActive(true);
            boxCollider.enabled = false;
            killCollider.enabled = false;
            rb.constraints = RigidbodyConstraints2D.None;
            gameObject.layer = 0;
            cam.GetComponent<CameraScript>().followPlayerX = true;
            cam.GetComponent<CameraScript>().followEnemy = false;
        }
    }
}

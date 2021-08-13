using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SimplePlatformController : MonoBehaviour
{

    public float speed;
    public float jump;
    public GameObject rayOrigin;
    public float rayCheckDistance;
    Rigidbody2D rb;
    public LayerMask layerMask;
    bool facingRight = true;
    public bool jumping;
    public int coinCount = 3;
    public GroupSpawner spawner;
    public EnemyChecker enemyCheck;
    public GameObject machine;
    public GameObject cam;
    public bool gameOver;
    public Text coins;
    public bool hasJetpack;
    int jumpCount = 0;
    public Sprite jetpackfire;
    public Sprite jetpack;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        coins.text = "" + coinCount;
        if(Input.GetKeyDown(KeyCode.E) && !GetComponent<SpeechScript>().paid)
        {
            coinCount = 0;
        }

        float x = Input.GetAxis("Horizontal");



        if (x > 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (x < 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }

        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance, layerMask);
            if (hit.collider != null)
            {
                jumpCount++;
                jumping = true;
                rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            }
        }

        if(hasJetpack && Input.GetButton("Jump") && jumpCount >= 1)
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = jetpackfire;
            rb.velocity = new Vector2(rb.velocity.x, 5);
        }
        else
        {
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = jetpack;

        }
        RaycastHit2D hit2 = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance, layerMask);
        if ((hit2.collider == null || hit2.collider.tag == "Player") && rb.velocity.y < 5 && !hasJetpack)
        {
            if (jumping)
            {
                jumping = false;
                rb.velocity += new Vector2(rb.velocity.x, -1f);
            }
            rb.velocity += new Vector2(rb.velocity.x, -0.3f);
        }


        rb.velocity = new Vector3(x * speed, rb.velocity.y, 0);

    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="Coin")
        {
            coinCount++;
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
        if(other.tag == "Death")
        {
            Debug.Log("RIP");
            gameOver = true;
            spawner.Place();
            cam.transform.position = new Vector3(0, -37.1875f,cam.transform.position.z);
            transform.position = new Vector3(5, -40.3f);
            transform.localScale = new Vector3(-1, transform.localScale.y);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            machine.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            facingRight = false;
            machine.transform.position = new Vector3(-4.21f, -36.06f);
            StartCoroutine(enemyCheck.Animate());
            coinCount = 0;

        }
	}
}
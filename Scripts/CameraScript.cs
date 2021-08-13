using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Color bgcolor;
    public bool followPlayer;
    public bool followPlayerX;
    public bool followEnemy;
    public GameObject enemy;
    public GameObject player;
    public ScoreManagement scoreManager;
    public GameObject machine;
    bool uploaded;
    bool found;

    void FixedUpdate()
    {
        if(transform.position.x > 178 && !uploaded)
        {
            Debug.Log("Start Calculating");
            uploaded = true;
            scoreManager.CalculateScores();
        }

        if(found)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
        }
        
        if(transform.position.y < -10)
        {
            GetComponent<Camera>().backgroundColor = bgcolor;
        }

        if(followPlayer)
        {
            
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(player.transform.position.y + 3.12f, -37.1815f,0), transform.position.z);

        }

        if(followPlayerX)
        {
            //Debug.Log("player");
            //if (transform.position.x < player.transform.position.x && Mathf.Abs(transform.position.x - player.transform.position.x) > 0.2f)
            //{
            //    transform.position += new Vector3(0.2f, 0);
            //}
            //else if (transform.position.x > player.transform.position.x && Mathf.Abs(transform.position.x - enemy.transform.position.x) > 0.2f)
            //{
            //    transform.position -= new Vector3(0.2f, 0);
            //}
            //else
            //{
            //    transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            //}
            if (!found && transform.position.x < player.transform.position.x && Mathf.Abs(transform.position.x - (player.transform.position.x )) > 0.15f && !(Mathf.Abs(transform.position.x - (player.transform.position.x )) > 10f))
            {
                transform.position += new Vector3(0.025f, 0);
            }
            else if (!found &&transform.position.x > player.transform.position.x && Mathf.Abs(transform.position.x - (player.transform.position.x )) > 0.15f && !(Mathf.Abs(transform.position.x - (player.transform.position.x )) > 10f))
            {
                transform.position -= new Vector3(0.025f, 0);
            }
            else if(!found)
            {
                found = true;
                transform.position = new Vector3(player.transform.position.x , transform.position.y, transform.position.z);
                //transform.SetParent(player.transform);

            }
        }

        if(followEnemy)
        {
            followPlayer = false;
            //Debug.Log("enemy: " + Vector2.Distance(transform.position, enemy.transform.position));
            //Debug.Log((transform.position.x < enemy.transform.position.x + 7f) + " - " + (Mathf.Abs(transform.position.x - (enemy.transform.position.x + 7f)) > 0.2f) + " - " + (Mathf.Abs(transform.position.x - (enemy.transform.position.x + 7f)) > 10f));
            if(transform.position.x < enemy.transform.position.x + 7f && Mathf.Abs(transform.position.x- (enemy.transform.position.x + 7f)) > 0.2f && !player.GetComponent<SimplePlatformController>().gameOver)
            {
                transform.SetParent(null);
                transform.position += new Vector3(0.025f,0);
            }
            else if(transform.position.x > enemy.transform.position.x + 7f && Mathf.Abs(transform.position.x - (enemy.transform.position.x + 7f)) > 0.2f && !player.GetComponent<SimplePlatformController>().gameOver)
            {
                transform.SetParent(null);
                transform.position -= new Vector3(0.025f, 0);
            }
            else if(player.GetComponent<SimplePlatformController>().gameOver)
            {
                transform.SetParent(null);
                player.GetComponent<SimplePlatformController>().gameOver = false;
                transform.position = new Vector3(enemy.transform.position.x + 7f, -37.1875f, -10);
                transform.SetParent(enemy.transform.parent);
                //transform.position = new Vector3(enemy.transform.position.x + 7f, -37.1875f, -10);
            }
            else
            {
                transform.SetParent(enemy.transform.parent);
            }
        }
        else
        {
            transform.SetParent(null);
        }
    }
}

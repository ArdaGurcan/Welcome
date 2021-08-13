using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChecker : MonoBehaviour
{
    public GameObject kill;
    public Camera cam;
    public AudioSource musicPlayer;
    bool started;

	private void OnTriggerEnter2D(Collider2D other)
	{
        if(other.tag == "Enemy")
        {
            cam.transform.position = new Vector3(0, -37.1875f, -10);
            cam.GetComponent<CameraScript>().followPlayer = false;
            cam.GetComponent<CameraScript>().followEnemy = true;
            other.transform.SetParent(transform.parent);

            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Rigidbody2D>().isKinematic = true;
            other.GetComponent<Rigidbody2D>().simulated = false;
            StartCoroutine("Animate");
        }

	}

    public IEnumerator Animate()
    {           
        transform.parent.GetComponent<MachineScript>().move = false;
        yield return new WaitForSeconds(0.25f);
        kill.SetActive(true);
        if(!started)
        {
            musicPlayer.Play();
            started = true;
        }
        yield return new WaitForSeconds(1.5f);
        kill.SetActive(false);
        transform.parent.GetComponent<MachineScript>().move = true;
    }
}

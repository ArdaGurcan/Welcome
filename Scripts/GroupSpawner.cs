using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawner : MonoBehaviour
{

    public GameObject[] easy;
    public GameObject[] intermediate;
    public GameObject[] hard;

	private void Start()
	{
        
        Place();
	}

    public GameObject[] Shuffle(GameObject[] items)
    {
        Random rand = new Random();

        // For each spot in the array, pick
        // a random item to swap into that spot.
        for (int i = 0; i < items.Length - 1; i++)
        {
            int j = Random.Range(i, items.Length);
            GameObject temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
        return items;
    }
 

	public void Place()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            Destroy(transform.GetChild(j).gameObject);
        }
        easy = Shuffle(easy);
        intermediate = Shuffle(intermediate);
        hard = Shuffle(hard);
        int i = 1;
        for (float x = 27; x < 27 + 10 * 16.5f; x += 16.5f)
        {
            if (i <= 4)
            {
                Instantiate(easy[i-1], new Vector3(x, 0), Quaternion.identity, transform);
            }
            else if (i <= 8)
            {
                Instantiate(intermediate[i-5], new Vector3(x, 0), Quaternion.identity, transform);
            }
            else
            {
                Instantiate(hard[i-9], new Vector3(x, 0), Quaternion.identity, transform);
            }
            i++;
        }
    }



}

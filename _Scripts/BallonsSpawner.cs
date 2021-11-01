using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonsSpawner : MonoBehaviour
{
    public GameObject[] ballons;
    public int index;
    public bool startSpawning;
  
    public void SpawnBallon()
    {
        if(startSpawning)
        StartCoroutine(SpawnBallons());
    }
    IEnumerator SpawnBallons()
    {
        yield return new WaitForSeconds(1.2f);
        Instantiate(ballons[index], transform.position, Quaternion.identity);
        index++;
        if(index == ballons.Length)
        {
            index = 0;
        }

        StartCoroutine(SpawnBallons());
    }

    public void StopSpawnBallon()
    {

        StopAllCoroutines();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField]
    private GameObject gunEnemy = null;

    private Transform playerTrs = null;

    [SerializeField]
    private float missionRange = 9;

    [SerializeField, Range(3, 10)]
    private int enemyValue = 5;

    private bool once = true;

    private void Start()
    {
        playerTrs = GameObject.FindWithTag("Target").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(playerTrs.position, this.transform.position) < missionRange&&once)
        {
            MissionStart();
            once = false;
        }
    }

    private void MissionStart()
    {
        int count = 0;

        int range = 3;

        for(int i = 0; i < enemyValue; i++)
        {
            int randomTheta = Random.Range(20, 70);

            GameObject enemyPrefab = Instantiate(gunEnemy, transform);

            count++;

            if (count == 1)
            {
                enemyPrefab.transform.position 
                    += new Vector3(range * Mathf.Sin(randomTheta), 0, range * Mathf.Cos(randomTheta));
            }
            else if (count == 2)
            {
                enemyPrefab.transform.position
                    += new Vector3(range * -Mathf.Sin(randomTheta), 0, range * Mathf.Cos(randomTheta));
            }
            else if (count == 3)
            {
                enemyPrefab.transform.position
                    += new Vector3(range * Mathf.Sin(randomTheta), 0, range * -Mathf.Cos(randomTheta));
            }
            else if (count == 4)
            {
                enemyPrefab.transform.position
                    += new Vector3(range * -Mathf.Sin(randomTheta), 0, range * -Mathf.Cos(randomTheta));
                count = 0;
                range *= 2;
            }
        }
    }
}

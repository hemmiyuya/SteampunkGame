using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField]
    private GameObject gunEnemy = null;

    private Transform playerTrs = null;

    private GameObject audioObj = null;

    [SerializeField]
    private float missionRange = 9;

    [SerializeField, Range(3, 10)]
    private int enemyValue = 5;

    private bool once = true;

    private Transform canvas;
    [SerializeField]
    private GameObject pinPrefab;
    private GameObject pinUI;


    private void Start()
    {
        playerTrs = GameObject.FindWithTag("Target").transform;
        canvas = GameObject.Find("Canvas").transform;
        pinUI = Instantiate(pinPrefab, canvas);
        pinUI.transform.SetParent(canvas);
        pinUI.GetComponent<TargetIndicator>().SetTarget(transform);
        audioObj = GameObject.FindGameObjectWithTag("Audio");
    }

    private void Update()
    {
        if (Vector3.Distance(playerTrs.position, this.transform.position) < missionRange&&once)
        {
            MissionStart();
            pinUI.SetActive(false);
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

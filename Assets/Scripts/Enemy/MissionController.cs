using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    [SerializeField]
    private GameObject gunEnemy = null;
    private GameObject[] enemys = null;

    private Transform playerTrs = null;

    private GameObject audioObj = null;
    private AudioManager audioManager = null;

    [SerializeField]
    private float missionRange = 9;
    [SerializeField]
    private float missionExitRange=40;

    [SerializeField, Range(3, 10)]
    private int enemyValue = 5;

    private bool buttle = false;

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
        audioManager = audioObj.GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Vector3.Distance(playerTrs.position, this.transform.position) < missionRange&&!buttle)
        {
            MissionStart();
            pinUI.SetActive(false);
            buttle = true;
        }
        //バトルから逃げた
        else if (Vector3.Distance(playerTrs.position, this.transform.position) > missionExitRange && buttle)
        {
            //敵を消す
            for (int i = 0; i < enemys.Length; i++) 
            {
                Destroy(enemys[i]);
            }
            audioManager.BgmOn();
            pinUI.SetActive(true);
            buttle = false;
        }
        //バトルに勝利
        else if (buttle&&transform.childCount == 0) MissionClear();
    }

    private void MissionStart()
    {
        //バトルBGMスタート
        audioManager.ButtleStart();
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

        enemys = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            enemys[i]=transform.GetChild(i).gameObject;
        }
    }

    private void MissionClear()
    {
        audioManager.BgmOn();
        Destroy(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INNSelect : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    [SerializeField]
    AudioManager audioManager = default;

    [SerializeField]
    private TimeManager timeManager = default;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            INNAccept();
            player.GetComponent<NPCTalkLength>().NPCTalkEnd();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            INNCancel();
            player.GetComponent<NPCTalkLength>().NPCTalkEnd();
        }
    }

    private void INNAccept()
    {
        timeManager.Sleep();

        gameObject.SetActive(false);
    }

    private void INNCancel()
    {
        audioManager.SEOn(3);
        gameObject.SetActive(false);
    }
}

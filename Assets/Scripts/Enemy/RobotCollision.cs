using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollision  : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    CharacterHp characterHp;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        characterHp = player.GetComponent<CharacterHp>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            characterHp.Dmage(25, 1,transform.position);
        }
    }

}

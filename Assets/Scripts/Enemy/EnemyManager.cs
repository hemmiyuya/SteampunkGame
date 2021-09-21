using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    GameObject player = default;

    Rigidbody playerRig = default;

    [SerializeField]
    float runSpeed = default;

    [SerializeField]
    float walkSpeed = default;

    [SerializeField]
    int enemyHp = 100;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRig = player.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        
    }
}

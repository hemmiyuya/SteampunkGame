using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerObj = default;

    private Rigidbody playerRig = default;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerRig = playerObj.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void Idle()
    {

    }
    private void Walk()
    {

    }

    private void Run()
    {

    }
}

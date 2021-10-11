using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミニマップのプレイヤー追従スクリプト
/// </summary>
public class MiniMapWithPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}

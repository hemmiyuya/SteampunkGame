using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �~�j�}�b�v�̃v���C���[�Ǐ]�X�N���v�g
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

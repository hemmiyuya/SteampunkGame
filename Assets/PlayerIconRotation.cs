using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIconRotation : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTrs=default;

    private void Update()
    {
        var rotatoy = _playerTrs.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.y, rotatoy+180, transform.rotation.z));
    }
}

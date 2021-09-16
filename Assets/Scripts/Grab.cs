using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    private InputManager _inputmanager;

    private Animationmanager _anim;

    private GameObject _player;

    public void InitialSet(GameObject player)
    {
        _player = player;
        _inputmanager = player.GetComponent<InputManager>();
        _anim = player.GetComponent<Animationmanager>();
    }
}

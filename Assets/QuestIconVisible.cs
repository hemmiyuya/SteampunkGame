using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIconVisible : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}

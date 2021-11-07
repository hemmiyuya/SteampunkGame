using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonSESet : MonoBehaviour
{
    [SerializeField]
    private AudioManager _audioManager;


    [SerializeField]
    private UIManager uiManager=default;
    public void DonSet()
    {
        _audioManager.SEOn(4);
    }
    
    public void ClereActiveFalse()
    {
        StartCoroutine(Clere());
    }

    private IEnumerator Clere()
    {
        yield return new WaitForSeconds(2.0f);
        uiManager.QuestClereWindowSetActive(false);
        uiManager.QuestClereEnd();
        yield break;
    }

}

using UnityEngine;

public class Exit : MonoBehaviour
{
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Quit();
    }
}
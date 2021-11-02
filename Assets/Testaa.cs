using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace AAA
{
    public class Testaa : MonoBehaviour
    {
        [SerializeField]
        public static IntReactiveProperty playerHealth = new IntReactiveProperty(100);

        int a = 111;

        void Start()
        {
            playerHealth
                .Where(_ => _ >= a)
                .Subscribe(x => Debug.Log(x));
        }
        /*
        private void FixedUpdate()
        {
            playerHealth.Value += 1;
        }
        */
    }
}

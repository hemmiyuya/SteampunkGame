using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventEffects : MonoBehaviour {
    
    private PlayerSound playerSound;

    public EffectInfo[] Effects;

    [System.Serializable]

    public class EffectInfo
    {
        public GameObject Effect;
        public Transform StartPositionRotation;
        public float DestroyAfter = 10;
        public bool UseLocalPosition = true;
    }

    void Start() {
        playerSound = GetComponent<PlayerSound>();
    }
            
    void InstantiateEffect(int EffectNumber)
    {
        if(Effects == null || Effects.Length <= EffectNumber)
        {
            Debug.LogError("Incorrect effect number or effect is null");
        }

        if (EffectNumber == 6 || EffectNumber == 7)
        {
            Effects[EffectNumber].StartPositionRotation.forward = new Vector3(Effects[EffectNumber].StartPositionRotation.forward.x, 0, Effects[EffectNumber].StartPositionRotation.forward.z);
        }
        else
        {
            playerSound.PlaySE(1);
        }

        var instance = Instantiate(Effects[EffectNumber].Effect, Effects[EffectNumber].StartPositionRotation.position, Effects[EffectNumber].StartPositionRotation.rotation);

        if (Effects[EffectNumber].UseLocalPosition)
        {
            instance.transform.parent = Effects[EffectNumber].StartPositionRotation.transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion();
        }
        Destroy(instance, Effects[EffectNumber].DestroyAfter);
    }
}

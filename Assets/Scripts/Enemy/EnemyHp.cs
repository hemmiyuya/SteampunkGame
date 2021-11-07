using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]
    private int hp = 100;

    EnemySounds sounds;

    private void Start()
    {
        sounds = GetComponent<EnemySounds>();
    }

    public int GetHp()
    {
        return hp;
    }

    public void Damage(int damage)
    {
        hp-=damage;
        sounds.PlaySE(0);
    }

}

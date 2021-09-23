using UnityEngine;

public class Conditions : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj = null;

    [SerializeField]
    private int walkDistance = 15;
    [SerializeField]
    private int runDistance = 30;
    [SerializeField]
    private float minDistance = 3;

    [SerializeField]
    private float randomTime = 3f;

    EnemyController enemyController;

    private float timer = 0;

    private int range = 0;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Target");
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        //—£‚ê‚·‚¬‚é‚Æ‘–‚é
        if(Vector3.Distance(playerObj.transform.position, this.transform.position) > runDistance)
        {
            enemyController.StateManager.State.Value = enemyController.StateRun;
            enemyController.DefaltAnim();
        }
        //­‚µ—£‚ê‚é‚Æ•à‚­
        else if (Vector3.Distance(playerObj.transform.position, this.transform.position) > walkDistance)
        {
            enemyController.StateManager.State.Value = enemyController.StateWalk;
            enemyController.DefaltAnim();
        }
        //‹ß‚·‚¬‚é‚ÆŒã‘Þ‚·‚é
        else if(Vector3.Distance(playerObj.transform.position, this.transform.position) < minDistance)
        {
            enemyController.StateManager.State.Value = enemyController.StateWalk;
            enemyController.ReverseAnim();
        }
        else
        {
            timer += Time.deltaTime;

            if (timer > randomTime) 
            {
                range = Random.Range(0, 3);
                timer = 0;
            }

            if (range == 0)
            enemyController.StateManager.State.Value = enemyController.StateIdle;

            if (range == 1)
            {
                enemyController.StateManager.State.Value = enemyController.StateSideWalk;
                enemyController.DefaltAnim();
            }
            if (range == 2)
            {
                enemyController.StateManager.State.Value = enemyController.StateSideWalk;
                enemyController.ReverseAnim();
            }
        }


    }
}

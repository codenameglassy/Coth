using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    [Header("Enemy Types")]
    public GameObject pigButcher;

    [Header("Spawn Positions")]
    public List<Transform> enemySpawnPos = new List<Transform>();

    [Header("Patrol Points")]
    public List<Transform> patrolPos = new List<Transform>();

    [Header("Active Enemy In Scene")]
    public List<Transform> activeEnemyInScene = new List<Transform>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnEnemy(GameObject enemy)
    {
        int index = Random.Range(0, enemySpawnPos.Count);

        Instantiate(enemy, enemySpawnPos[index].position, Quaternion.identity);
    }

    public void SpawnPigButcher()
    {
        SpawnEnemy(pigButcher);
    }

}

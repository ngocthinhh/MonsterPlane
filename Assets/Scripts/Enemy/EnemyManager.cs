using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemiesContain;
    [SerializeField] private int countEnemy;
    [SerializeField] private float distanceToPlayer;

    [SerializeField] private bool canSpawn;
    [SerializeField] private float spawnAfterTime;
    [SerializeField] private float timeRunning = 0f;


    private void Update()
    {
        if (!canSpawn) return;

        timeRunning += Time.deltaTime;
        if (timeRunning >= spawnAfterTime)
        {
            if (enemiesContain.childCount <  countEnemy)
            {
                Spawn();
            }
            ActiveInPool();
            timeRunning = 0f;
        }
    }

    void Spawn()
    {
        Vector3 locationSpawn = GetLocation();
        GameObject GO = Instantiate(enemyPrefab, locationSpawn, Quaternion.identity, enemiesContain);
        EnemyController enemyController = GO.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Player = player;
        }
    }

    void ActiveInPool()
    {
        foreach (Transform child in  enemiesContain)
        {
            if (child.gameObject.activeSelf) continue;

            Vector3 locationActive = GetLocation();
            child.transform.position = locationActive;
            child.gameObject.SetActive(true);
        }
    }

    Vector3 GetLocation()
    {
        float randNumA = Random.Range(0, 360);
        float a = distanceToPlayer * Mathf.Sin(randNumA * Mathf.Deg2Rad);
        float b = distanceToPlayer * Mathf.Cos(randNumA * Mathf.Deg2Rad);
        return player.transform.position + new Vector3(a, 0, b);
    }

    public void Restart()
    {
        foreach(Transform child in enemiesContain)
        {
            Destroy(child.gameObject);
        }

        timeRunning = 0;
    }
}

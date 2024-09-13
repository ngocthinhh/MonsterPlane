using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public GameObject Player { get { return player; } set { player = value; } }

    private NavMeshAgent agent;

    [SerializeField] private int health = 30;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player == null) return;

        agent.destination = player.transform.position;
    }

    private void OnEnable()
    {
        Restart();
    }

    void Restart()
    {
        health = 30;
    }

    public void ApplyDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);

            ManagerGame.OnPlusScore?.Invoke();
        }
    }
}

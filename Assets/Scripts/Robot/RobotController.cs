using UnityEngine;

public class RobotController : MonoBehaviour
{
    public enum RobotMode
    {
        Guardian,
        Combatant
    }
    [SerializeField] private RobotMode robotMode;
    public RobotMode RobotModePub {  get { return robotMode; } set { robotMode = value; } }

    public RobotInput RobotInput;
    public RobotMovement RobotMovement;
    public RobotAnimation RobotAnimation;
    public RobotUseGun RobotUseGun;

    private void Awake()
    {
        RobotInput = GetComponent<RobotInput>();
        RobotMovement = GetComponent<RobotMovement>();
        RobotAnimation = GetComponent<RobotAnimation>();
        RobotUseGun = GetComponent<RobotUseGun>();

        robotMode = RobotMode.Combatant;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ManagerGame.OnGameOver?.Invoke();
        }
    }
}
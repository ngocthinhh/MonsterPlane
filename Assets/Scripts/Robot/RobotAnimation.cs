using UnityEngine;

public class RobotAnimation : MonoBehaviour
{
    RobotController RobotController;
    Animator Animator;

    Vector3 velocity = Vector3.zero;
    [SerializeField] float speedMoveAnimation = 10;

    private void Awake()
    {
        RobotController = GetComponent<RobotController>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        velocity = Vector3.Lerp(velocity, RobotController.RobotMovement.MovementInput, speedMoveAnimation * Time.deltaTime);
        Animator.SetFloat("Velocity", velocity.magnitude);

        // STATE
        switch (RobotController.RobotModePub)
        {
            case RobotController.RobotMode.Guardian:
                Animator.SetInteger("State", 1);
                break;
            case RobotController.RobotMode.Combatant:
                Animator.SetInteger("State", 2);
                break;
        }

        //
        bool isShoot = RobotController.RobotUseGun.IsTrigger;
        Animator.SetBool("IsShoot", isShoot);
    }
}

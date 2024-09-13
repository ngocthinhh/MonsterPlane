using UnityEngine;

public class RobotInput : MonoBehaviour
{
    RobotController RobotController;

    private void Awake()
    {
        RobotController = GetComponent<RobotController>();
    }

    private void Update()
    {
        // MOVEMENT INPUT
        if (RobotController.RobotMovement)
        {
            Vector3 vector3 = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            RobotController.RobotMovement.MovementInput = vector3;
        }

        // SWITCH STATE
        if (Input.GetKeyUp(KeyCode.Alpha1) && RobotController.RobotModePub != RobotController.RobotMode.Guardian)
        {
            RobotController.RobotModePub = RobotController.RobotMode.Guardian;
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && RobotController.RobotModePub != RobotController.RobotMode.Combatant)
        {
            RobotController.RobotModePub = RobotController.RobotMode.Combatant;
        }

        // TRIGGER SHOOT
        switch (RobotController.RobotModePub)
        {
            case RobotController.RobotMode.Guardian:
                RobotController.RobotUseGun.IsTrigger = false;
                break;
            case RobotController.RobotMode.Combatant:
                RobotController.RobotUseGun.IsTrigger = Input.GetMouseButton(0);
                break;
        }
    }
}

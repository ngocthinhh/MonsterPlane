using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    RobotController RobotController;
    Rigidbody Rigidbody;

    [SerializeField] private Vector3 movementInput;
    [SerializeField] private bool canMovementInput;
    [SerializeField] private float speedMovement;
    [SerializeField] private float speedRotate;
    [SerializeField] private Transform ObjectSupportMovement;

    //

    public Vector3 MovementInput { get { return movementInput; } set { movementInput = value; } }

    public bool CanMovementInput => canMovementInput;

    private void Awake()
    {
        RobotController = GetComponent<RobotController>();
        Rigidbody = GetComponent<Rigidbody>();

    }

    void Start()
    {
        canMovementInput = true;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (canMovementInput)
        {
            Vector3 direction = movementInput;
            direction.Normalize();

            // MOVE
            Vector3 directionForward;
            if (direction.magnitude > 0)
            {
                directionForward = transform.forward;
            }
            else
            {
                directionForward = Vector3.zero;
            }
            Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, directionForward * speedMovement, Time.fixedDeltaTime * speedMovement);

            // ROTATE
            Vector3 locationCamera = new Vector3(ObjectSupportMovement.position.x, 0f, ObjectSupportMovement.position.z);
            Vector3 locationPlayer = new Vector3(transform.position.x, 0f, transform.position.z);
            Vector3 directionFromCamera = locationPlayer - locationCamera;
            if (direction.magnitude > 0)
            {
                Rigidbody.rotation = Quaternion.Slerp(Rigidbody.rotation, Quaternion.LookRotation(direction) * Quaternion.LookRotation(directionFromCamera), Time.fixedDeltaTime * speedRotate);
            }
        }
    }
}

using UnityEngine;

public class ObjectCameraHandle : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform camera;
    [SerializeField] private Transform objectFollow;

    [Header("Only Read Input Mouse")]
    public float mouseX;
    public float mouseY;

    [Header("Sensitivity")]
    [SerializeField] private float sensitivityX = 0.1f;
    [SerializeField] private float sensitivityY = 0.1f;

    [Header("Limit Mouse Y")]
    [SerializeField] private float minLimitMouseY;
    [SerializeField] private float maxLimitMouseY;

    [Header("Scroll Mouse (Zoom In/Out)")]
    [SerializeField] private float scrollMouse;
    [SerializeField] private float speedScroll;
    [SerializeField] private float minLimitZoom;
    [SerializeField] private float maxLimitZoom;

    private void Update()
    {
        SetInputMouse(ref mouseX, ref mouseY, ref scrollMouse);

        RotateHorizontalScreen(mouseX, sensitivityX);
        RotateVerticalScreen(mouseY, sensitivityY, minLimitMouseY, maxLimitMouseY);

        ZoomInOut(scrollMouse, speedScroll, minLimitZoom, maxLimitZoom);
    }

    private void LateUpdate()
    {
        FollowPlayer(playerTransform.position);
    }

    // ======================= FUNC =====================

    void FollowPlayer(Vector3 positionPlayer)
    {
        // ALLWAYS FOLLOW PLAYER
        transform.position = positionPlayer;
    }
    
    void SetInputMouse(ref float mouseX, ref float mouseY, ref float scrollMouse)
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        scrollMouse = Input.GetAxis("Mouse ScrollWheel");
    }

    void RotateVerticalScreen(float mouseY, float sensitivityY, float minLimitMouseY, float maxLimitMouseY)
    {
        float currentMouseYConvert;
        float currentMouseY = transform.eulerAngles.x;
        if (currentMouseY > 180)
        {
            currentMouseYConvert = currentMouseY - 360;
        }
        else
        {
            currentMouseYConvert = currentMouseY;
        }

        if (mouseY > 0 && currentMouseYConvert < minLimitMouseY) return;
        if (mouseY < 0 && currentMouseYConvert > maxLimitMouseY) return;
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, transform.eulerAngles + (Vector3.left * mouseY), Time.deltaTime * sensitivityY);
    }

    void RotateHorizontalScreen(float mouseX, float sensitivityX)
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, transform.eulerAngles + (Vector3.up * mouseX), Time.deltaTime * sensitivityX);
    }

    void ZoomInOut(float scrollMouse, float speedScroll, float minLimitZoom, float maxLimitZoom)
    {
        float distanceFromCamera = Vector3.Distance(objectFollow.position, camera.position);
        if (scrollMouse > 0 && distanceFromCamera < minLimitZoom) return;
        if (scrollMouse < 0 && distanceFromCamera > maxLimitZoom) return;

        Vector3 directionFromObjectFollow = objectFollow.position - camera.position;
        camera.transform.forward = directionFromObjectFollow;
        camera.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + directionFromObjectFollow * scrollMouse, speedScroll * Time.deltaTime);
    }
}

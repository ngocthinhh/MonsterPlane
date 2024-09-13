using UnityEngine;

public class RobotUseGun : MonoBehaviour
{
    RobotController RobotController;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletContain;
    [SerializeField] private int bulletInPoolCount;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform explosionContain;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private AudioSource shootSound;

    [SerializeField] private bool isTrigger;
    public bool IsTrigger { get { return isTrigger; } set { isTrigger = value; } }

    private void Awake()
    {
        RobotController = GetComponent<RobotController>();
    }

    private void Start()
    {
        SpawnBulletIntoPool(bullet, bulletContain, bulletInPoolCount);
    }

    // ======================= FUNC =====================

    void SpawnBulletIntoPool(GameObject bulletObject, Transform bulletContain, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject bullet = Instantiate(bulletObject, bulletContain);
            bullet.SetActive(false);

            GameObject explosion = Instantiate(explosionPrefab, explosionContain);
            explosion.SetActive(false);
        }
    }

    public void Shoot()
    {
        foreach (Transform bullet in bulletContain)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.position = shootPoint.position;
                bullet.rotation = shootPoint.rotation;
                bullet.localEulerAngles -= Vector3.forward ;

                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.velocity = Vector3.zero;
                bulletRb.angularVelocity = Vector3.zero;

                bullet.gameObject.SetActive(true);
                bulletRb.AddForce(bullet.transform.up * 50, ForceMode.VelocityChange);

                shootSound.Play();

                return;
            }
        }
    }
}

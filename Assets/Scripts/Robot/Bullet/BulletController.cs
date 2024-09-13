using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float timeExist = 2f;
    IEnumerator destroyCorotine;

    void OnEnable()
    {
        destroyCorotine = DestroyAfter(timeExist);
        StartCoroutine(destroyCorotine);
    }

    IEnumerator DestroyAfter(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();
            enemyController.ApplyDamage(10);

            ExplosionContainController.OnExplosion?.Invoke(transform.position);

            StopCoroutine(destroyCorotine);
            gameObject.SetActive(false);
        }
    }
}

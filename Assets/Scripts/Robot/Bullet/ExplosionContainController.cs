using System;
using System.Collections;
using UnityEngine;

public class ExplosionContainController : MonoBehaviour
{
    public static Action<Vector3> OnExplosion;

    private void Awake()
    {
        OnExplosion = Callexplosion; 
    }

    void Callexplosion(Vector3 position)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (!child.gameObject.activeSelf)
            {
                child.transform.position = position;
                child.gameObject.SetActive(true);
                StartCoroutine(TurnOffAfter(child.gameObject, 0.5f));
                return;
            }
        }
    }

    IEnumerator TurnOffAfter(GameObject objectActive, float time)
    {
        yield return new WaitForSeconds(time);
        objectActive.SetActive(false);
    }
}

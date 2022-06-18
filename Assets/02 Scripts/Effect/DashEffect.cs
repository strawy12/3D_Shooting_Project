using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    public void StartEffect(Vector3 forward, float duration)
    {
        transform.rotation = Quaternion.LookRotation(-forward);
        gameObject.SetActive(true);
        StartCoroutine(DelayStopEffect(duration));
    }

    private IEnumerator DelayStopEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }


}

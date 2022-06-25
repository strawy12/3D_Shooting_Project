using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimePanel : MonoBehaviour
{
    [SerializeField] protected Image _fillImage;

    private void Awake()
    {
        if(_fillImage == null)
        {
            _fillImage = transform.Find("Fill").GetComponent<Image>();
        }
    }

    public virtual void StartDelay(float delay)
    {
        StopAllCoroutines();
        StartCoroutine(StartDelayCoroutine(delay));
    }
    public IEnumerator StartDelayCoroutine(float delay)
    {
        float time = 0f;
        while (time < delay)
        {
            if (time == 0f)
            {
                _fillImage.fillAmount = 0f;
            }
            else
            {
                _fillImage.fillAmount = time / delay;
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

}

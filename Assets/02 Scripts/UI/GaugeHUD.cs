using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class GaugeHUD : MonoBehaviour
{
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _gaugeSpeed;
    [SerializeField] private float _returnDelayTime;
    [SerializeField] private Image _fillImage;
    [SerializeField] private CanvasGroup _canvasGroup;

    private Transform _targetTrs;

    private Coroutine _returnDelayCoroutine = null;

    private bool _isActive = false;

    private float _value = 0f;


    private void LateUpdate()
    {
        if (_isActive == false) return;

        Vector3 pos = Camera.main.WorldToScreenPoint(_targetTrs.position);
        pos.x = (int)pos.x;
        pos.y = (int)pos.y;

        transform.position = Vector3.Lerp(transform.position, pos + (Vector3)_offset, Time.deltaTime * 10f);

        _value = Mathf.Sin(Time.time * _gaugeSpeed) * 0.5f + 0.5f;
        _fillImage.fillAmount = Mathf.Clamp(_value, 0f, 1f); 
    }

    public void Init(Transform targetTrs, float speed)
    {
        _targetTrs = targetTrs;
        _gaugeSpeed = speed;
        _value = 0f;
        ActiveGauge();
    }

    public float GetValue()
    {
        return _value;
    }

    public void ActiveGauge()
    {
        if (_isActive) return;
        _isActive = true;
        gameObject.SetActive(true);
    }

    public void UnActiveGauge()
    {
        Debug.Log("dd");
        _value = 0f;
        _isActive = false;
        gameObject.SetActive(false);
    }
}

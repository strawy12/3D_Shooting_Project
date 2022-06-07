using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class GaugeHUD : MonoBehaviour
{
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _gaugeSpeed;
    [SerializeField] private Image _fillImage;
    [SerializeField] private CanvasGroup _canvasGroup;
    

    private bool _isActive;

    private float _value = 0f;

    public UnityEvent<float> OnThrowStart;

    private void LateUpdate()
    {
        if (_isActive == false) return;

        Vector3 pos = Camera.main.WorldToScreenPoint(_playerTrs.position);
        pos.x = (int)pos.x;
        pos.y = (int)pos.y;

        transform.position = Vector3.Lerp(transform.position, pos +(Vector3)_offset, Time.deltaTime * 10f);
        _value += _gaugeSpeed * Time.deltaTime;
        _fillImage.fillAmount = Mathf.Clamp(_value, 0f, 1f);

        if(_value > 1f)
        {
            transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }


    public void ActiveGauge()
    {
        if (_isActive) return; 
        _isActive = true;
        gameObject.SetActive(true);
    }

    public void unActiveGauge(bool isStart)
    {
        if (isStart)
        {
            OnThrowStart?.Invoke(_value);
        }
        _value = 0f;
        _isActive = false;
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Ping : PoolableMono
{
    [SerializeField] private TMP_Text _distanceText;
    [SerializeField] private float _constant;
    [SerializeField] private float _deleteDistance;
    
    private Transform _playerTrs;
    private PingUI _pingUI;

    private Camera _mainCam;
    private float _distance;

    private bool _isActive;

    public void InitPing()
    {
        transform.DOScaleY(1f, 0.5f);
        _isActive = true;
        // 사운드
        // 전용 UI 연결
        _pingUI = GameManager.Inst.UI.PopPingUI();
    }

    private void LateUpdate()
    {
        if (_isActive == false) return;
        PlayerDistace();

        Vector2 point = _mainCam.WorldToViewportPoint(transform.position);

        if (point.x < 0f || point.x > 1f || point.y < 0f || point.y > 1f)
        {
            point.x = Mathf.Clamp(point.x, 0.2f, 0.8f);
            point.y = Mathf.Clamp(point.y, 0.2f, 0.8f);
            Vector2 screenPoint = _mainCam.ViewportToScreenPoint(point);
            _pingUI.transform.position = screenPoint;
            _pingUI.ActiveUI(true, _distance);
        }

        else
        {
            _pingUI?.ActiveUI(false);
        }
    }

    private void PlayerDistace()
    {
        Vector3 dir = _playerTrs.position - transform.position;
        dir.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(-dir);

        _distance = dir.magnitude;
        _distanceText.text = $"{(int)_distance}m";

        Vector3 adder = (Vector3.one * _distance) / _constant;
        transform.localScale = Vector3.one + adder;

        if (_distance <= _deleteDistance)
        {
            DeletePing();
        }
    }

    private void DeletePing()
    {
        _isActive = false;
        transform.DOScaleY(0f, 0.5f);
        // 꺼지는 사운드
        // 전용 UI 연동 해제
        GameManager.Inst.UI.PushPingUI(_pingUI);
        _pingUI = null;
        PoolManager.Inst.Push(this);
    }

    public override void Reset()
    {
        if (_playerTrs == null)
            _playerTrs = Define.PlayerRef;
        if (_mainCam == null)
            _mainCam = Define.MainCam;
    }
}

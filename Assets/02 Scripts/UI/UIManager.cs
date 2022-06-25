using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _damagePopupTemp;

    [SerializeField] private float _spreadRange = 300f;
    [SerializeField] private float _maxDropPosY = 100f;
    [SerializeField] private float _jumpPower;
    [SerializeField] private HpBar _playerHpBar;

    [SerializeField] private List<ItemPanel> _itemPanelList = new List<ItemPanel>();
    [SerializeField] private DashPanel _dashPanel = null;
    [SerializeField] private InteractionUI _interationUI = null;


    private Stack<TMP_Text> _damagePopupPool = new Stack<TMP_Text>();
    private InteractionObject _currentInteractionObject;

    private void Update()
    {
        if(_currentInteractionObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _currentInteractionObject.TakeAction();
                _currentInteractionObject = null;
            }
        }
    }

    public void GenerateDamagePopup(Vector3 pos, int damage, bool isCritical)
    {

        pos = Define.MainCam.WorldToScreenPoint(pos);
        TMP_Text damagePopup = PopDamagePopup();
        damagePopup.text = damage.ToString();
        damagePopup.transform.localScale = Vector3.zero;
        damagePopup.rectTransform.position = pos;

        Sequence seq = DOTween.Sequence();
        seq.Append(damagePopup.transform.DOScale(Vector3.one * 1.5f, 0.5f).SetEase(Ease.InOutBounce));
        seq.Join(damagePopup.DOFade(1f, 0.25f));

        int randDir = Random.Range(-1, 2);
        float xPos = pos.x + randDir * Random.Range(1f, _spreadRange);
        float yPos = pos.y - Random.Range(1f, _maxDropPosY);

        seq.Join(damagePopup.transform.DOJump(new Vector2(xPos, yPos), _jumpPower, 1,0.5f));
        seq.Append(damagePopup.transform.DOScale(Vector3.zero, 0.25f));
        seq.Join(damagePopup.DOFade(0f, 0.4f));
        seq.AppendCallback(() => PushDamagePopup(damagePopup));
    }

    private TMP_Text PopDamagePopup()
    {
        TMP_Text damagePopup = null;
        if (_damagePopupPool.Count == 0)
        {
            damagePopup = Instantiate(_damagePopupTemp, _damagePopupTemp.transform.parent);
        }

        else
        {
            damagePopup = _damagePopupPool.Pop();
        }

        damagePopup.gameObject.SetActive(true);

        return damagePopup;
    }

    public ItemPanel FindItemPanel(EItemType type)
    {
        return _itemPanelList.Find(x => x.Type == type);
    }

    private void PushDamagePopup(TMP_Text damagePopup)
    {
        damagePopup.gameObject.SetActive(false);

        _damagePopupPool.Push(damagePopup);
    }

    public void SetHpBar(int hp, int maxhp, int shieldhp)
    {
        _playerHpBar.SetHpBar(hp, maxhp, shieldhp);
    }

    public void StartDashCoolTime(float delay)
    {
        StartCoroutine(_dashPanel.StartDelayCoroutine(delay));
    }

    public void SetDashCount(int cnt)
    {
        _dashPanel.SetCountText(cnt);
    }

    public void ShowInteractionUI(string text, InteractionObject obj)
    {
        if (_currentInteractionObject == obj) return;


        _currentInteractionObject = obj;
        _interationUI.ShowUI(text);
    }

    public void UnShowInteractionUI(InteractionObject obj)
    {
        if (_currentInteractionObject == null) return;
        if (_currentInteractionObject != obj) return;

        _currentInteractionObject = null;
        _interationUI.UnShowUI();

    }
}

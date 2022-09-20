using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _damagePopupTemp;
    [SerializeField] private TMP_Text _killTextTemp;
    [SerializeField] private PingUI _pingUITemp;

    [SerializeField] private float _spreadRange = 300f;
    [SerializeField] private float _maxDropPosY = 100f;
    [SerializeField] private float _jumpPower;
    [SerializeField] private HpBar _playerHpBar;

    [SerializeField] private List<ItemPanel> _itemPanelList = new List<ItemPanel>();
    [SerializeField] private DashPanel _dashPanel = null;
    [SerializeField] private InteractionUI _interationUI = null;
    [SerializeField] private DoorOpenArlamPanel _doorOpenArlamPanel = null;

    [SerializeField] private CanvasGroup _settingPanel;

    [SerializeField] private TextPanel _textPanel;

    private bool _opeSettingPanel;

    private Stack<TMP_Text> _damagePopupPool = new Stack<TMP_Text>();
    private Stack<TMP_Text> _killTextPool = new Stack<TMP_Text>();
    private Stack<PingUI> _pingUIPool = new Stack<PingUI>();
    private InteractionObject _currentInteractionObject;

    private void Update()
    {
        if (_currentInteractionObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _currentInteractionObject.TakeAction();
                _currentInteractionObject = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActiveSettingPanel();
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

        seq.Join(damagePopup.transform.DOJump(new Vector2(xPos, yPos), _jumpPower, 1, 0.5f));
        seq.Append(damagePopup.transform.DOScale(Vector3.zero, 0.25f));
        seq.Join(damagePopup.DOFade(0f, 0.4f));
        seq.AppendCallback(() => PushDamagePopup(damagePopup));
    }

    public void GenerateKillText(string name)
    {
        TMP_Text killText = PopKillText();
        killText.text = $"+ {name} Kill!";
        Sequence seq = DOTween.Sequence();
        seq.Append(killText.DOFade(0f, 2f));
        seq.Join(killText.rectTransform.DOAnchorPosY(-100f, 1f));
        seq.AppendCallback(() => PushKillText(killText));
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

    private TMP_Text PopKillText()
    {
        TMP_Text killText = null;
        if (_killTextPool.Count == 0)
        {
            killText = Instantiate(_killTextTemp, _killTextTemp.transform.parent);
        }

        else
        {
            killText = _killTextPool.Pop();
        }

        killText.color = Color.red;
        killText.rectTransform.anchoredPosition = new Vector2(0f, -180f);

        killText.gameObject.SetActive(true);

        return killText;
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

    private void PushKillText(TMP_Text killText)
    {
        killText.gameObject.SetActive(false);

        _killTextPool.Push(killText);
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

    public void DoorOpen()
    {
        _doorOpenArlamPanel.StartEffect();
    }

    public PingUI PopPingUI()
    {
         
        PingUI pingUI = null;
        if (_pingUIPool.Count == 0)
        {
            pingUI = Instantiate(_pingUITemp, _pingUITemp.transform.parent);
        }

        else
        {
            pingUI = _pingUIPool.Pop();
        }

        pingUI.Init();

        return pingUI;
    }

    public void PushPingUI(PingUI pingUI)
    {
        pingUI.gameObject.SetActive(false);
        _pingUIPool.Push(pingUI);
    }

    public void ActiveSettingPanel()
    {
        _opeSettingPanel = !_opeSettingPanel;
        _settingPanel.DOFade(_opeSettingPanel ? 1f : 0f, 1f).SetUpdate(true);
        Cursor.lockState = _opeSettingPanel ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = _opeSettingPanel ? 0f : 1f;
    }

    public void ShowTextPanel(string text)
    {
        _textPanel.ShowTextPanel(text, "System", Color.red);
        _textPanel.UnShowTextPanel((text.Length * 0.03f) + 3f);
    }
}

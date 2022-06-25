using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPanel : CoolTimePanel
{
    [SerializeField] private EItemType _itemType;
    [SerializeField] private TMP_Text _countText;


    public EItemType Type
    {
        get => _itemType;
    }

    private void Start()
    {
        SetCountText();
    }

    public void SetCountText()
    {
        int cnt = GameManager.Inst.Data.GetItemCount(_itemType);

        _countText.text = cnt.ToString();
    }
}

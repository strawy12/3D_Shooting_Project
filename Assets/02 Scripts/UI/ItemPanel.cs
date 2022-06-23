using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] private EItemType _itemType;
    [SerializeField] private Text _countText;
    
    public EItemType Type
    {
        get => _itemType;
    }

    public void SetCountText()
    {
        int cnt = GameManager.Inst.Data.GetItemCount(_itemType);

        _countText.text = cnt.ToString();
    }
}

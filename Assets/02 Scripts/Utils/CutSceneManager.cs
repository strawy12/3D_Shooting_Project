using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutSceneManager : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private Transform _playerTrs;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private TextPanel _textPanel;

    #region Tutorial_Scene
    [Header("Tutorial_Scene")]
    [SerializeField] private CoolTimePanel _dashPanel;
    [SerializeField] private CoolTimePanel _trapPanel;
    [SerializeField] private CoolTimePanel _hoshiTanPanel;
    [SerializeField] private GameObject _tutorialEnemy;


    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CutScene/TextData")]
public class CutSceneTextSO :ScriptableObject 
{
    [TextArea]
    public List<string> textDataList; 
}

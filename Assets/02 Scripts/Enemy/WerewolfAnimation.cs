using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfAnimation : AgentAnimation
{
    private int _hashSkill = Animator.StringToHash("Skill");

    public void PlaySkillAnim()
    {
        OnPlayActAnimation?.Invoke();

        _animator.SetTrigger(_hashSkill);
    }
}

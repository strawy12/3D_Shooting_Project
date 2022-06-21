using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    private int _hashSpawn = Animator.StringToHash("Spawn");

    public void PlaySpawnAnim()
    {
        _animator.SetTrigger(_hashSpawn);
    }
}

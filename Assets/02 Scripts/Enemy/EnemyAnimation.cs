using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    private int _hashSpawn = Animator.StringToHash("Spawn");

    public void PlaySpawnAnim()
    {
        if (_isDead)
        {
            _isDead = false;
        }
        _animator.SetTrigger(_hashSpawn);
    }
}

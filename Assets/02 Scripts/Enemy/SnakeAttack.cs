using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if (_waitBeforeNextAttack == false)
        {
            IHittable hittable = GetTarget().GetComponent<IHittable>();

            hittable?.GetHit(damage: damage, damagerDealer: gameObject);
            AttackFeedBack?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

}

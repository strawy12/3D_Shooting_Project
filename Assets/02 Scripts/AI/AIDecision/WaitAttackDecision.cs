using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAttackDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return _aiActionData.waitingAttack;
    }

}

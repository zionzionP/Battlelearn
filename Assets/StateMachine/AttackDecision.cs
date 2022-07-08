using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/AttackDecision")]
public class AttackDecision : Decision
{
    public float attackDistance;
    public override bool Decide(StateController controller)
    {
        bool inAttackDistance = GetDistance(controller);
        return inAttackDistance;
    }

    private bool GetDistance(StateController controller)
    {
        if ((controller.agent.position - controller.emeny.position).sqrMagnitude < attackDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


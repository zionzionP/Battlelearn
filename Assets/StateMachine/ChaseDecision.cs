using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ChaseDecision")]
public class ChaseDecision : Decision
{
    public float patrolDistance;
    public override bool Decide(StateController controller)
    {
        bool inPatrolDistance = GetDistance(controller);
        return inPatrolDistance;
    }

    private bool GetDistance(StateController controller)
    {
        if (controller.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            //Debug.Log("IsAttack");
            return false;
            
        }
        else if ((controller.agent.position - controller.emeny.position).sqrMagnitude > patrolDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        
        controller.navMeshAgent.velocity = Vector3.zero;
        controller.navMeshAgent.isStopped = true;
        controller.anim.SetBool("IsWalk", false);
        controller.anim.SetBool("Attack", true);
        if (controller.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            controller.anim.SetBool("Attack", false);
        }
        //Debug.Log("Attack");
    }
}

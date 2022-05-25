using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
             
        controller.anim.SetBool("Attack", false);
        controller.navMeshAgent.isStopped = false;
        controller.navMeshAgent.destination = controller.emeny.position;
        controller.anim.SetBool("IsWalk", true);
        //Debug.Log("Chase");
    }


}
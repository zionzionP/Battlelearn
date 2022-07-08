using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Death")]
public class DeathAction : Action
{  

    public override void Act(StateController controller)
    {
        controller.isAttacking = false;
        Death(controller);
    }

    private void Death(StateController controller)
    {
        controller.navMeshAgent.speed = 0;
        controller.GetComponent<CapsuleCollider>().enabled = false;
        controller.anim.SetTrigger("Die");
        if (!controller.dead)
        {
            controller.emeny.GetComponent<StateController>().score += 1;
            controller.dead = true;            
        }
        

    }
}

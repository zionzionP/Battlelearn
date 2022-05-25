using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Death")]
public class DeathAction : Action
{
    public override void Act(StateController controller)
    {
        Death(controller);
    }

    private void Death(StateController controller)
    {
        //controller.navMeshAgent.speed = 0;
        
        //Debug.Log("DeathAction");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/DeathDecision")]
public class DeathDecision : Decision
{
    
    public override bool Decide(StateController controller)
    {
        bool isDead = GetDeath(controller);
        return isDead;
    }

    private bool GetDeath(StateController controller)
    {
        if (controller.hp <= 0)
        {
            return true;
        }        
        else
        {
            return false;
        }
    }
}

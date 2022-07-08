using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/EnemyDeathDecision")]
public class EnemyDeathDecision : Decision
{
    private Animator anim;
    public override bool Decide(StateController controller)
    {
        bool isDead = GetDeath(controller);
        return isDead;
    }

    private bool GetDeath(StateController controller)
    {
        anim = controller.emeny.GetComponent<Animator>();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

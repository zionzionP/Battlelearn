using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScriptMonster : MonoBehaviour
{
    
    public StateController stateController;
    private StateController stateAgent;
    private BattleLearning battleLearning;
    public bool attackable = false;
        
    void OnTriggerEnter(Collider other)
    {
        if (stateController.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (other.CompareTag("Agent"))
            {
                if (attackable)
                {
                    stateAgent = other.GetComponent<StateController>();
                    if (stateAgent.anim.GetCurrentAnimatorStateInfo(0).IsName("Block"))
                    {
                        stateAgent.anim.SetTrigger("Impact");
                    }
                    else
                    {
                        stateAgent.hp -= 20;
                        stateAgent.UpdateHPValue();
                        battleLearning = other.GetComponent<BattleLearning>();
                        battleLearning.AddReward(-1f);
                    }                    
                }

            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ChangeModel/ChangeToAttack")]
public class ChangeToAttack : Action
{
    public override void Act(StateController controller)
    {
        ChangeTo(controller);

    }

    private void ChangeTo(StateController controller)
    {
        controller.battleLearning.ConfigureAgent(1);
    }
}
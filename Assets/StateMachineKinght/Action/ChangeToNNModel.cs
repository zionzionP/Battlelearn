using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ChangeModel/ChangeToNNModel")]
public class ChangeToNNModel : Action
{
    [SerializeField] private int configNum;
    public override void Act(StateController controller)
    {
        ChangeTo(controller);

    }

    private void ChangeTo(StateController controller)
    {
        controller.battleLearning.ConfigureAgent(configNum);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/PatrolDecision")]
public class RayDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool isHit = GetRayHit(controller);
        return isHit;
    }

    private bool GetRayHit(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(new Vector3(controller.transform.position.x, 1f, controller.transform.position.z), controller.transform.forward * 10, Color.blue, 0.1f);
        if (Physics.Raycast(new Vector3(controller.transform.position.x, 1f, controller.transform.position.z), controller.transform.forward, out hit))
        {
            if (hit.collider.tag == "Enemy" && hit.distance < 8f)
            {
                Debug.Log("Hit");
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

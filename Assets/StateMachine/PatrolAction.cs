using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    [SerializeField] private float patrolRadius;
    [SerializeField] private float patrolTime;
    private Vector3 goal;
    private float time;

    public override void Act(StateController controller)
    {
        time = time + Time.deltaTime;
        if (time > patrolTime)
        {
            Patrol(controller);
        }

        if ((controller.agent.position - goal).sqrMagnitude < 0.1)
        {
            controller.anim.SetBool("IsWalk", false);
        }
    }

    private void Patrol(StateController controller)
    {
        float x = Random.Range(-patrolRadius, patrolRadius);
        float z = Random.Range(-patrolRadius, patrolRadius);
        goal =new Vector3(controller.agent.position.x + x, controller.agent.position.y, controller.agent.position.z + z);
        NavMeshHit hit;
        NavMesh.SamplePosition(goal, out hit, 4f, NavMesh.AllAreas);
        controller.navMeshAgent.SetDestination(hit.position);
        controller.anim.SetBool("IsWalk", true);
        time = 0f;
    }
}

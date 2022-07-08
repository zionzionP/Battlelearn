using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationEvent : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private AttackScriptMonster attack;
    private StateController attackState;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        attack = GetComponentInChildren<AttackScriptMonster>();
        attackState = GetComponent<StateController>();
    }

    void Navstop()
    {
        navMeshAgent.speed = 0;
        //Debug.Log("stopped");
    }

    void Unactive()
    {
        this.gameObject.SetActive(false);
        

    }

    void Attackable()
    {
        attack.attackable = true;
    }

    void UnAttackable()
    {
        attack.attackable = false;
    }

    void IsAttacking()
    {
        attackState.isAttacking = true;
    }

    void IsNotAttacking()
    {
        attackState.isAttacking = false;
    }



}

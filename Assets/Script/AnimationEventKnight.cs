using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventKnight : MonoBehaviour
{

    private BattleLearning battleLearning;
    private Rigidbody rb;
    private AttackScript attack;
    private KnightController controller;
    private StateController attackState;

    // Start is called before the first frame update
    void Start()
    {
        battleLearning = GetComponent<BattleLearning>();
        rb = GetComponent<Rigidbody>();
        attack = GetComponentInChildren<AttackScript>();
        controller = GetComponent<KnightController>();
        attackState = GetComponent<StateController>();
    }

    void StopRotation()
    {
        battleLearning.stopRotation = true;
        controller.stopRotation = true;
    }

    void StartRotation()
    {
        battleLearning.stopRotation = false;
        controller.stopRotation = false;
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
        controller.isAction = true;
    }

    void IsNotAttacking()
    {
        attackState.isAttacking = false;
        controller.isAction = false;
    }
}

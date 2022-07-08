using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{

    public State currentState;
    public State remainState;
    public Transform agent;
    public Transform emeny;

    
    //public NavMeshHit navMeshHit;


    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public BattleLearning battleLearning;

    [SerializeField] private GameObject HPUI;
    [SerializeField] public int maxHp = 100;
    [HideInInspector] public bool dead = false;
    [HideInInspector] public int hp;
    private Slider hpSlider;
    [HideInInspector] public int score = 0;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isBlocking;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        battleLearning = GetComponent<BattleLearning>();
        hpSlider = HPUI.transform.Find("HPBar").GetComponent<Slider>();
        hpSlider.value = 1f;
        isAttacking = false;        
        SetMaxHP();
    }

    public void SetMaxHP()
    {
        hp = maxHp;
        Debug.Log("maxhp");
    }

    public void UpdateHPValue()
    {
        hpSlider.value = (float)hp / (float)maxHp;
    }

    


    private void Update()
    {
        currentState.UpdateState(this);
    }

    

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            //OnExitState();
        }
    }

    /*public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }*/
}
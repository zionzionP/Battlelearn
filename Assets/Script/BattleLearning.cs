using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Barracuda;
using Unity.MLAgentsExamples;
using UnityEngine.UI;
public class BattleLearning : Agent
{
	int m_Configuration;
	public NNModel chaseMode;
	public NNModel attackMode;
	public NNModel patrolMode;

	public float moveX;              
    public float moveZ;
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 input;
    public Animator anim;
	public float forwardSpeed = 7.0f;
	public Transform enemyTf;
	private bool isClose;	
	public float ag;	
	public bool stopRotation;
	private float time;
	public StateController enemyState;
	private bool isAction;
	public Text stateText;

	string m_ChaseModeBehaviorName = "ChaseMode";
	string m_AttackModeBehaviorName = "AttackMode";
	string m_PatrolModeBehaviorName = "PatrolMode";

	/*void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }*/

	public override void Initialize()
	{
		m_Configuration = 1;
		isClose = false;

		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		

		Debug.Log("Initialize");
		
		var modelOverrider = GetComponent<ModelOverrider>();
		if (modelOverrider.HasOverrides)
		{
			chaseMode = modelOverrider.GetModelForBehaviorName(m_ChaseModeBehaviorName);
			m_ChaseModeBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_ChaseModeBehaviorName);

			attackMode = modelOverrider.GetModelForBehaviorName(m_AttackModeBehaviorName);
			m_AttackModeBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_AttackModeBehaviorName);

			patrolMode = modelOverrider.GetModelForBehaviorName(m_PatrolModeBehaviorName);
			m_PatrolModeBehaviorName = ModelOverrider.GetOverrideBehaviorName(m_PatrolModeBehaviorName);
		}


	}
    

    public override void OnEpisodeBegin()
    {
		transform.localPosition = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
		isClose = false;

	}

    public override void CollectObservations(VectorSensor sensor)
    {
		//Vector3 diff = enemyTf.localPosition - transform.localPosition;
		//float positionDiff2 = diff.sqrMagnitude; 
		//float rotationDiff = Vector3.Angle(transform.forward, diff);
		//sensor.AddObservation(transform.localPosition.x);
		//sensor.AddObservation(transform.localPosition.z);
		//sensor.AddObservation(enemyTf.localPosition.x);
		//sensor.AddObservation(enemyTf.localPosition.z);
		//sensor.AddObservation(positionDiff2);
		//sensor.AddObservation(rotationDiff);
		sensor.AddObservation(transform.position.x - enemyTf.position.x);
		sensor.AddObservation(transform.position.z - enemyTf.position.z);
		sensor.AddObservation(enemyState.isAttacking);
	}

    public override void OnActionReceived(ActionBuffers actions)
    {
		MoveAgent(actions.DiscreteActions);
		/*float moveX = actions.ContinuousActions[0];
		float moveZ = actions.ContinuousActions[1];		

		velocity = Vector3.zero;
		input = new Vector3(moveX, 0f, moveZ);

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			velocity = Vector3.zero;
			input = Vector3.zero;
			anim.ResetTrigger("Attack");
		}

		if (input.magnitude > 0f)
		{
			transform.LookAt(rb.position + input);

			velocity = rb.transform.forward * forwardSpeed;

			anim.SetBool("IsWalk", true);
		}
		else
		{
			anim.SetBool("IsWalk", false);
		}

		if (m_Configuration == 1)
        {
			Attack(actions.DiscreteActions);
		}*/



	}

	public void MoveAgent(ActionSegment<int> act)
	{
		AddReward(-0.0005f);
		anim.SetBool("IsWalk", false);
		anim.SetBool("IsBlock", false);
		isAction = false;
		var dirToGo = Vector3.zero;
		var rotateDir = Vector3.zero;
		var dirToGoForwardAction = act[0];
		var rotateDirAction = act[1];
		var dirToGoSideAction = act[2];
		var attackAction = act[3];
		var blockAction = act[4];

		if (dirToGoForwardAction == 1)
		{
			dirToGo = 1f * transform.forward;
			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", 1f);
		}
		else if (dirToGoForwardAction == 2)
		{
			dirToGo = -0.8f * transform.forward;
			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", -1f);
		}

		if (rotateDirAction == 1)
		{
			rotateDir = transform.up * -1f;
			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", -1f);
		}
		else if (rotateDirAction == 2)
		{
			rotateDir = transform.up * 1f;
			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", -1f);
		}

		if (dirToGoSideAction == 1)
		{
			dirToGo = -0.6f * transform.right;
			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", -1f);
		}
		else if (dirToGoSideAction == 2)
		{
			dirToGo = 0.6f * transform.right;
			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", -1f);
		}

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("BackStep"))
		{
			dirToGo = Vector3.zero;
			rotateDir = Vector3.zero;
			anim.ResetTrigger("BackStep");
			isAction = true;
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			dirToGo = Vector3.zero;
			anim.ResetTrigger("Attack");
			anim.ResetTrigger("BackStep");
			isAction = true;			
		}

		if (m_Configuration == 1)
        {
			if (isAction == false)
            {
				if (attackAction == 1)
                {
					anim.SetBool("IsWalk", false);
					anim.SetTrigger("Attack");
				}

				if (blockAction == 1)
                {					
					anim.SetBool("IsBlock", true);
					dirToGo = Vector3.zero;
					rotateDir = Vector3.zero;
				}

			}
        }	

		if (stopRotation)
        {
			rotateDir = Vector3.zero;
		}



		transform.Rotate(rotateDir, Time.fixedDeltaTime * 500f);
		rb.AddForce(dirToGo * forwardSpeed,
			ForceMode.VelocityChange);

		if (!isClose)
		{
			if ((transform.position - enemyTf.position).sqrMagnitude < 4.5f)
			{
				AddReward(1f);
				isClose = true;
			}
		}
	}
	/*private void FixedUpdate()
    {
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
	}*/

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		/*ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
		var discreteActionsOut = actionsOut.DiscreteActions;
		continuousActions[0] = Input.GetAxis("Horizontal");
		continuousActions[1] = Input.GetAxis("Vertical");
		discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;*/

		var discreteActionsOut = actionsOut.DiscreteActions;
		if (Input.GetKey(KeyCode.E))
		{
			discreteActionsOut[1] = 2;
		}
		if (Input.GetKey(KeyCode.W))
		{
			discreteActionsOut[0] = 1;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			discreteActionsOut[1] = 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			discreteActionsOut[0] = 2;
		}
		if (Input.GetKey(KeyCode.A))
		{
			discreteActionsOut[2] = 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			discreteActionsOut[2] = 2;
		}


		discreteActionsOut[3] = Input.GetKey(KeyCode.Mouse0) ? 1 : 0;

		discreteActionsOut[4] = Input.GetKey(KeyCode.Mouse1) ? 1 : 0;

	}

	/*public void Attack(ActionSegment<int> act)
	{
		var attack = act[0];
		if (attack == 1)
		{
			anim.SetTrigger("Attack");			
		}
	}*/

	private void OnCollisionEnter(Collision other)
	{
		/*if (other.gameObject.tag == "Wall")
		{
			AddReward(-0.1f);
		}*/

		if (other.gameObject.tag == "Limit")
		{
			AddReward(-1f);
			EndEpisode();
		}

	}

	public void ConfigureAgent(int config)
	{

		if (config == 0)
		{
			if (m_Configuration != 0)
			{
				SetModel(m_ChaseModeBehaviorName, chaseMode);
				m_Configuration = 0;
				stateText.text = m_ChaseModeBehaviorName;
			}

		}
		else if (config == 1)
		{
			if (m_Configuration != 1)
			{
				SetModel(m_AttackModeBehaviorName, attackMode);
				m_Configuration = 1;
				stateText.text = m_AttackModeBehaviorName;

			}
		}
		else if (config == 2)
        {
			if (m_Configuration != 2)
            {
				SetModel(m_PatrolModeBehaviorName, patrolMode);
				m_Configuration = 2;
				stateText.text = m_PatrolModeBehaviorName;
			}
        }
		
		//EndEpisode();
	}

    /*private void Update()
    {
		time = time + Time.deltaTime;
		
		if (m_Configuration == 0)
		{			
			if (time > 5f)
			{
				float distanceReward = -0.01f * (((transform.position - enemyTf.position).sqrMagnitude) - 32);
				AddReward(distanceReward);
				time = 0;
			}
		}


		if (!isClose)
        {
			if ((transform.position - enemyTf.position).sqrMagnitude < 9)
            {
				SetReward(1f);
				isClose = true;
            }
        }
    }*/

    private void Update()
    {
		

	}



}

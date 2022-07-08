using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Barracuda;
using Unity.MLAgentsExamples;

public class PatrolLearning : Agent
{

	private int m_Configuration = 0;
	public float moveX;              // 入力デバイスの水平軸をhで定義
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
	

	/*void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }*/

	public override void Initialize()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}


	public override void OnEpisodeBegin()
	{
		transform.localPosition = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
		enemyTf.localPosition = new Vector3(Random.Range(-8f, 8f), 0, Random.Range(-8f, 8f));
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
		
	}
	

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		

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

    private void Update()
    {
		RaycastHit hit;
		Debug.DrawRay(new Vector3(transform.position.x, 1f, transform.position.z), transform.forward * 10, Color.blue, 0.1f);
		if (Physics.Raycast(new Vector3(transform.position.x, 1f, transform.position.z), transform.forward, out hit))
		{
			if (hit.collider.tag == "Enemy")
			{
					AddReward(1f);
					EndEpisode();			
			}
		}
	}

}

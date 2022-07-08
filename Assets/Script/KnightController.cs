using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
		
	public float forwardSpeed = 7.0f;	
	

	public float moveX;              // 入力デバイスの水平軸をhで定義
	public float moveZ;
	private Rigidbody rb;
	private Vector3 velocity;
	private Vector3 input;
	public Animator anim;	
	public bool stopRotation;
	public bool isAction;
	public GameObject menuUI;
	private bool isLockOn;
	private StateController controller;
	[SerializeField] private Transform lockOnUI;

	void Start()
	{
		// Animatorコンポーネントを取得する
		anim = GetComponent<Animator>();
		// CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
		rb = GetComponent<Rigidbody>();
		controller = GetComponent<StateController>();
		isAction = false;
		isLockOn = false;
		Time.timeScale = 1;
		lockOnUI.position = new Vector3(0f, -200f, 0f);
	}


	
	void Update()
	{
		
		moveX = Input.GetAxis("Horizontal");                // 入力デバイスの水平軸をhで定義
		moveZ = Input.GetAxis("Vertical");              // 入力デバイスの垂直軸をvで定義	
		var cameraRotetion = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);													
		
		velocity = Vector3.zero;
		input = cameraRotetion * new Vector3(moveX, 0f, moveZ);		
		
		if (input.magnitude > 0.3f)
		{		

			velocity = rb.transform.forward * forwardSpeed ;

			anim.SetBool("IsWalk", true);
			anim.SetFloat("Speed", 1f);
		}
		else
		{
			anim.SetBool("IsWalk", false);
		}

		/*if (anim.GetCurrentAnimatorStateInfo(0).IsName("BackStep"))
		{
			anim.SetBool("IsWalk", false);
			velocity = Vector3.zero;
			input = Vector3.zero;
			isAction = true;
		}*/
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			velocity = Vector3.zero;
			anim.ResetTrigger("Attack");
			if (stopRotation)
			{
				input = Vector3.zero;
			}			
		}

		
		if (isAction == false)
        {
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				anim.SetTrigger("Attack");				
			}

			/*if (Input.GetKeyDown(KeyCode.Space))
			{
				anim.SetBool("IsWalk", false);
				anim.SetTrigger("BackStep");
				rb.AddForce(-40f * transform.forward, ForceMode.VelocityChange);
				stopRotation = false;
			}*/
			if (Input.GetKey(KeyCode.Mouse1))
			{
				anim.SetBool("IsBlock", true);
				velocity = Vector3.zero;
				input = Vector3.zero;
			}
            else
            {
				anim.SetBool("IsBlock", false);
			}
		}
		if (isLockOn == true)
        {
			transform.LookAt(controller.emeny);
			lockOnUI.position = new Vector3 (controller.emeny.position.x, 1f, controller.emeny.position.z);
			lockOnUI.rotation = Camera.main.transform.rotation;
		}

		if (input.magnitude > 0)
		{
			if (!stopRotation)
			{
				transform.LookAt(rb.position + input);
			}

		}

		if (Input.GetKeyDown(KeyCode.P))
        {
			if (menuUI.activeSelf == false)
            {
				menuUI.SetActive(true);
				Time.timeScale = 0;
            }
            else
            {
				menuUI.SetActive(false);
				Time.timeScale = 1;
			}
        }

		if (Input.GetKeyDown(KeyCode.F))
        {
			if(isLockOn == false)
            {
				isLockOn = true;				
            }
            else
            {
				isLockOn = false;
				lockOnUI.position = new Vector3(0f, -200f, 0f);
            }
        }
	}

	void FixedUpdate()
    {
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		//rb.AddForce(input, ForceMode.Force);
	}

	
}


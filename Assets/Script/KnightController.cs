using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
	public float animSpeed = 1.5f;              // アニメーション再生速度設定
	public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
	public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
												// このスイッチが入っていないとカーブは使われない
	public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

	// 以下キャラクターコントローラ用パラメタ
	// 前進速度
	public float forwardSpeed = 7.0f;
	// 後退速度
	public float backwardSpeed = 2.0f;
	// 旋回速度
	public float rotateSpeed = 2.0f;
	// ジャンプ威力
	public float jumpPower = 3.0f;
	// キャラクターコントローラ（カプセルコライダ）の参照

	public float moveX;              // 入力デバイスの水平軸をhで定義
	public float moveZ;
	private Rigidbody rb;
	private Vector3 velocity;
	private Vector3 input;
	public Animator anim;	

	
	void Start()
	{
		// Animatorコンポーネントを取得する
		anim = GetComponent<Animator>();
		// CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
		rb = GetComponent<Rigidbody>();		
	}


	
	void Update()
	{
		moveX = Input.GetAxis("Horizontal");                // 入力デバイスの水平軸をhで定義
		moveZ = Input.GetAxis("Vertical");              // 入力デバイスの垂直軸をvで定義														
		
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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetTrigger("Attack");
		}

		

	}

	void FixedUpdate()
    {
		rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		//rb.AddForce(input, ForceMode.Force);
	}

	
}


using UnityEngine;
using System.Collections;

public class LeanController : MonoBehaviour 
{
	public float moveSpeed = 5f;
	public float moveSmooth = 2f;
	public float leanScale = 60f;
	public float leanSmooth = 2f;
	public float turnSmooth = 2f;
	public bool  faceMoveDirection = true;
	public float gravityAccel = 9.81f;
	
	public Vector3 velocity;
	private Vector3 prevVelocity;
	private Vector3 velocityRef;
	public Vector3 accel;
	public Vector3 prevPosition;
	public Quaternion prevRotation;
	private float prevLean;
    private Vector3 targetVelocity;

    private bool touchingGround = false;

    public bool TouchingGround { get { return touchingGround; } }
	
	void Start() 
	{
		prevPosition = transform.position;
        targetVelocity = Vector3.zero;
	}

    public void Move(float amount)
    {
        targetVelocity.x = amount;
    }

	void Update() 
	{
		prevVelocity = velocity;

		Vector3 targetAccel = Vector3.zero;

		targetVelocity = targetVelocity.normalized*moveSpeed;
		
		velocity = Vector3.SmoothDamp (velocity , targetVelocity , ref velocityRef , Time.deltaTime * moveSmooth);

		targetAccel = (velocity - prevVelocity) / Time.deltaTime;
		
		accel = Vector3.Lerp (accel,targetAccel,Time.deltaTime);
		
		if(faceMoveDirection)
		{
			if (velocity.magnitude > 0.1f)
			{
				AccelLeanTurn();
			}
			else // zero out any weird Quaternion rotations, but maintain your worldSpace Y rotation
			{ 
				transform.rotation = prevRotation;
				Quaternion rotationreset = transform.rotation;
				rotationreset = Quaternion.AngleAxis(0,Vector3.forward);
				rotationreset = Quaternion.AngleAxis(0,Vector3.right);
				rotationreset = Quaternion.AngleAxis(transform.rotation.eulerAngles.y,Vector3.up);
				
				transform.rotation = Quaternion.Lerp (transform.rotation,rotationreset,Time.deltaTime * 2);
			}
		}
		else { AccelLean(); }
		
		ApplyVelocity();
		prevPosition = transform.position;
		prevRotation = transform.rotation;
	}
	
	void AccelLeanTurn()
	{
		Quaternion targetRot = Quaternion.Lerp (transform.rotation,Quaternion.LookRotation(velocity.normalized),Time.deltaTime * 10f/turnSmooth);
		Vector3 targetRotVec = targetRot.eulerAngles;
		Vector3 targetLeanAxis = Vector3.Cross(Vector3.up,accel.normalized);
		//Debug.DrawRay(transform.position+Vector3.up,targetLeanAxis*10f,Color.red,1f);
		Quaternion targetLean  = Quaternion.AngleAxis( Mathf.Lerp(prevLean,Mathf.Atan(accel.magnitude/gravityAccel)*Mathf.Rad2Deg,Time.deltaTime * 100f/leanSmooth) ,targetLeanAxis); //uses gravitational acceleration
		targetRotVec = new Vector3 (0f,targetRotVec.y,0f);
		
		targetRot =	targetLean * Quaternion.AngleAxis(targetRotVec.y,Vector3.up);
		transform.rotation = targetRot;
		prevLean = Mathf.Atan(accel.magnitude/gravityAccel);
	}
	
	void AccelLean()
	{
		Quaternion leanTarget;
		Vector3 leanTemp = Vector3.zero;
		leanTemp.x = accel.z*-leanScale;
		leanTemp.z = accel.x*leanScale;
		leanTarget = Quaternion.Euler(leanTemp);
		transform.rotation = Quaternion.Lerp (transform.rotation,leanTarget,Time.deltaTime * leanSmooth);
	}
	
	void ApplyVelocity()
	{
		transform.position += velocity * Time.deltaTime;
	}
}

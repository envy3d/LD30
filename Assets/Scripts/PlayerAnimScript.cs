using UnityEngine;
using System.Collections;

public class PlayerAnimScript : MonoBehaviour {

	public Animator anim;
	public float speedSmooth = 1f;
	public LeanController controller;
	public float speed = 0f;
	private float previousSpeed = 0f;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		speed = Mathf.Lerp (previousSpeed, Vector3.Magnitude( controller.velocity ), Time.deltaTime * 10/speedSmooth);
		if (speed < 0.01f) {speed = 0f;}
		anim.SetFloat("Speed", speed);
		previousSpeed = speed;
	}
}

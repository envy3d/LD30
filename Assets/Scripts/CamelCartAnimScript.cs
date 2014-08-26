using UnityEngine;
using System.Collections;

public class CamelCartAnimScript : MonoBehaviour {

	public Animator anim;
	public float speedSmooth = 1f;
	public float speed = 0f;
	private Vector3 previousPos;
	private float previousSpeed = 0f;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		previousPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		speed = Mathf.Lerp (previousSpeed, Vector3.Magnitude(transform.position - previousPos) / Time.deltaTime, Time.deltaTime * 10/speedSmooth);
		if (speed < 0.01f) {speed = 0f;}
		anim.SetFloat("Speed", speed);
		previousSpeed = speed;
		previousPos = transform.position;
	}
}

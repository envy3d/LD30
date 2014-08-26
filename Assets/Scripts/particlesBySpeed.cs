using UnityEngine;
using System.Collections;

public class particlesBySpeed : MonoBehaviour 
{
	public ParticleSystem particleSystem;
	public float speed = 0f;
	public float speedSmooth = 1f;
	public float speedToParticleSpeed = 1f;
	public float speedToParticleEmission = 1f;
	public float speedToParticleAlpha = 1f;
	public float speedToParticleSize = 1f;
	public float speedToSizeOffset = 0f;

	private Vector3 previousPosition;
	private float previousSpeed;

	// Use this for initialization
	void Start () 
	{
		previousPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		speed = Mathf.Lerp(previousSpeed, Vector3.Magnitude(transform.position - previousPosition) / Time.deltaTime, Time.deltaTime * 10/speedSmooth);

		particleSystem.emissionRate = speed * speedToParticleEmission;

		particleSystem.startSpeed = speed * speedToParticleSpeed;

		Color particleColor = particleSystem.startColor;
		particleColor.a = speed * speedToParticleAlpha;
		particleSystem.startColor = particleColor;

		particleSystem.startSize = speed * speedToParticleSize + speedToSizeOffset;

		previousPosition = transform.position;
		previousSpeed = speed;
	}
}

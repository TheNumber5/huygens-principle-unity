using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionSurface : MonoBehaviour {
	public float ior;
	void OnCollisionEnter2D(Collision2D other) {
		ior = HuygensPrinciple.instance.ior;
		Particle particle = other.gameObject.GetComponent<Particle>();
		particle.hasHitRefractiveSurface=true;
		float incident = Mathf.Deg2Rad*Vector3.Angle(-particle.velocity, this.transform.up);
		float refracted = Mathf.Asin(Mathf.Sin(incident)/ior);
		float newSpeed = particle.velocity.magnitude/ior;
		if(particle.velocity.x>=0)
		particle.velocity = new Vector3(Mathf.Sin(refracted)*newSpeed, -Mathf.Cos(refracted)*newSpeed);
		else
		particle.velocity = new Vector3(-Mathf.Sin(refracted)*newSpeed, -Mathf.Cos(refracted)*newSpeed);
	}
}
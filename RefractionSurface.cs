using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionSurface : MonoBehaviour {
	public float ior;
	void OnCollisionEnter2D(Collision2D other) {
		ior = HuygensPrinciple.instance.ior;
		Particle particle = other.gameObject.GetComponent<Particle>();
		float incident = Mathf.Deg2Rad*Vector3.Angle(-particle.velocity, this.transform.up);
		float refracted = Mathf.Rad2Deg*Mathf.Asin(Mathf.Sin(incident)/ior);
		float newSpeed = particle.velocity.magnitude/ior;
		if(particle.velocity.x>=0)
		particle.velocity = (Quaternion.AngleAxis(refracted, new Vector3(0f, 0f, 1f))*(-this.transform.up)).normalized*newSpeed;
		else
		particle.velocity = (Quaternion.AngleAxis(-refracted, new Vector3(0f, 0f, 1f))*(-this.transform.up)).normalized*newSpeed;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionSurface: MonoBehaviour {
	void OnCollisionEnter2D(Collision2D other) {
		Particle particle = other.gameObject.GetComponent<Particle>();
		particle.velocity = Vector3.Reflect(particle.velocity, this.transform.up);
	}
	void FixedUpdate() {
		Debug.DrawLine(this.transform.position, this.transform.position+this.transform.up, Color.red);
	}
}

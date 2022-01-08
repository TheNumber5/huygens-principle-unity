using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSurface : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D other) {
		other.gameObject.GetComponent<Particle>().velocity = Vector3.zero;
	}
	void Update() {
		Debug.DrawLine(this.transform.position, this.transform.position+this.transform.up, Color.red);
	}
}

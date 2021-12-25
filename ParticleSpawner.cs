using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {
	public GameObject particle;
	public int spawnSize;
	public float spawnDistance;
	public float rotateSpeed, moveSpeed;
	private List<Transform> particles = new List<Transform>();
	void Start() {
		spawnSize = HuygensPrinciple.instance.size;
		spawnDistance = HuygensPrinciple.instance.distance;
		RespawnParticles(false);
	}
	public void RespawnParticles(bool destroyOldOnes) {
	this.transform.rotation = Quaternion.identity;
	if(destroyOldOnes) {
		foreach(Transform particle in particles) {
			Destroy(particle.gameObject);
		}
	}
	particles = new List<Transform>();
	for(int i=0; i<spawnSize; i++) {
		GameObject clone = Instantiate(particle, this.transform.position+new Vector3(i*spawnDistance, 0f, 0f)+new Vector3(-spawnSize*spawnDistance/2f, 0f, 0f), Quaternion.identity);
		clone.name = "Temporary particle " + (i+1);
		clone.transform.parent = this.transform;
		clone.GetComponent<TrailRenderer>().time = 0f;
		particles.Add(clone.transform);
	}
	}
	public void SpawnParticles() {
		this.transform.DetachChildren();
		for(int i=0; i<particles.Count; i++) {
			particles[i].name = "Particle " + (i+1);
			if(HuygensPrinciple.instance.linesVisible)
			particles[i].GetComponent<TrailRenderer>().time = 15f;
			particles[i].GetComponent<Particle>().velocity = HuygensPrinciple.instance.particleSpeed*this.transform.up;
			HuygensPrinciple.instance.particles.Add(particles[i]);
		}
		RespawnParticles(false);
	}
	void Update() {
		Debug.DrawLine(this.transform.position, this.transform.position+this.transform.up, Color.blue);
		if(Input.GetKey(KeyCode.Keypad7)) {
			this.transform.rotation = this.transform.rotation*Quaternion.Euler(0f, 0f, -rotateSpeed*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.Keypad9)) {
			this.transform.rotation = this.transform.rotation*Quaternion.Euler(0f, 0f, rotateSpeed*Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.Keypad8)) {
			this.transform.position += Vector3.up*moveSpeed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Keypad5)) {
			this.transform.position += -Vector3.up*moveSpeed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Keypad4)) {
			this.transform.position += -Vector3.right*moveSpeed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.Keypad6)) {
			this.transform.position += Vector3.right*moveSpeed*Time.deltaTime;
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			SpawnParticles();
		}
	}
}

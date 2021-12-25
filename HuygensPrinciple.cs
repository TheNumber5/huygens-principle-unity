using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuygensPrinciple : MonoBehaviour {
	public static HuygensPrinciple instance;
	public List<Transform> particles = new List<Transform>();
	private List<Transform> huygensParticles = new List<Transform>();
	public GameObject particle;
	public ParticleSpawner particleSpawner;
	public int size;
	public float distance, particleSpeed, ior;
	public int huygensSize;
	public Slider particleSpeedSlider, sizeSlider, distanceSlider, huygensSlider, iorSlider;
	public Text particleSpeedText, particleSizeText, distanceText, huygensText, iorText;
	public Gradient blueGradient;
	[HideInInspector]
	public bool linesVisible=false;
	void Awake() {
	if(instance==null) {
		instance = this;
	}
	}
	void Update() {
		if(Input.GetKeyDown(KeyCode.LeftControl)) {
			StartHuygens();
		}
		if(Input.GetKeyDown(KeyCode.Q)) {
			DestroyParticles();
		}
	}
	void FixedUpdate() {
		foreach(Transform current in particles) {
			Particle currentParticle = current.GetComponent<Particle>();
			currentParticle.velocity = currentParticle.velocity.normalized*particleSpeed;
			current.position = new Vector3(current.position.x+currentParticle.velocity.x*Time.deltaTime, current.position.y+currentParticle.velocity.y*Time.deltaTime, 0f);
			Debug.DrawLine(current.position, current.position+currentParticle.velocity, Color.green);
		}
		foreach(Transform current in huygensParticles) {
			Particle currentParticle = current.GetComponent<Particle>();
			currentParticle.velocity = currentParticle.velocity.normalized*particleSpeed;
			current.position = new Vector3(current.position.x+currentParticle.velocity.x*Time.deltaTime, current.position.y+currentParticle.velocity.y*Time.deltaTime, 0f);
			Debug.DrawLine(current.position, current.position+currentParticle.velocity, Color.green);
		}
	}
	public void StartHuygens() {
		if(particles.Count <= 20) {
		foreach(Transform current in huygensParticles) {
			Destroy(current.gameObject);
		}
		huygensParticles = new List<Transform>();
		foreach(Transform current in particles) {
		for(int i=0; i<huygensSize; i++) {
		GameObject clone = Instantiate(particle, current.position, Quaternion.identity);
		clone.name = "Huygens particle " + (i+1);
		clone.GetComponent<Particle>().velocity = Quaternion.AngleAxis(i*360/huygensSize, new Vector3(0f, 0f, 1f))*Vector3.up;
		clone.GetComponent<SpriteRenderer>().color = new Color(0f, 0.2f, 0.5f, 1f);
		clone.GetComponent<SpriteRenderer>().sortingOrder = 0;
		clone.GetComponent<TrailRenderer>().colorGradient = blueGradient;
		if(linesVisible)
			clone.GetComponent<TrailRenderer>().time = 15f;
		else
			clone.GetComponent<TrailRenderer>().time = 0f;
		huygensParticles.Add(clone.transform);
		}
		}
		}
	}
	public void DestroyParticles() {
	foreach(Transform current in particles) {
		Destroy(current.gameObject);
	}
	foreach(Transform current in huygensParticles) {
		Destroy(current.gameObject);
	}
	particles = new List<Transform>();
	huygensParticles = new List<Transform>();
	}
	public void ChangeParameter(string parameter) {
		switch(parameter) {
			case "speed":
			particleSpeed = particleSpeedSlider.value;
			particleSpeedText.text = particleSpeedSlider.value.ToString("0.00") + " m/s";
			break;
			case "size":
			size = (int)sizeSlider.value;
			particleSizeText.text = sizeSlider.value.ToString("0");
			particleSpawner.spawnSize = size;
			particleSpawner.RespawnParticles(true);
			break;
			case "distance":
			distance = distanceSlider.value;
			distanceText.text = distanceSlider.value.ToString("0.00") + " m";
			particleSpawner.spawnDistance = distance;
			particleSpawner.RespawnParticles(true);
			break;
			case "huygens size":
			huygensSize = (int)huygensSlider.value;
			huygensText.text = huygensSlider.value.ToString("0");
			break;
			case "ior":
			ior = iorSlider.value;
			iorText.text = iorSlider.value.ToString("0.00");
			break;
		}
	}
	public void ChangeLinesVisible() {
		linesVisible = !linesVisible;
	}
}

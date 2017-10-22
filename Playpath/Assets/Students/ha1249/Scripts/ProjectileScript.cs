using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
	public int pNum = 0;

	public Material p1Mat; 
	public Material p2Mat;

	public Vector3 initialVelocity;

	public MainPlayer player;

	TrailRenderer tr;

	[SerializeField]
	float minVelocity = 10f;
	[SerializeField]
	float damageAmount = 15.3f;

	Vector3 lastFrameVelocity;
	Rigidbody rb;

	void OnEnable(){
		tr = GetComponent<TrailRenderer> ();
		rb = GetComponent<Rigidbody>();
//		rb.velocity = initialVelocity;

//		p1Mat = Resources.Load ("Material/P1.mat", typeof(Material)) as Material;
//		p2Mat = Resources.Load ("Material/P2.mat", typeof(Material)) as Material;
	}

	void Start(){
		rb.velocity = initialVelocity;


	}

	void Update() {
		lastFrameVelocity = rb.velocity;

		if (pNum == 1) {
			tr.material = p1Mat;
		} else if (pNum ==2){
			tr.material = p2Mat;
		}
	}

	void OnCollisionEnter(Collision collision){
		
		if (collision.gameObject.tag == "Player") {
//			if (collision.gameObject.GetComponent<RecorderUnitScript> () != null) {
//				collision.gameObject.GetComponent<RecorderUnitScript> ().health -= damageAmount;
//			}

			Debug.Log ("Player collision");
			DestroyBullet (gameObject);

		}

		if (collision.gameObject.tag == "Wall") {

			DestroyBullet (gameObject);

		}

		if (collision.gameObject.tag == "GOAL_A") {
			ScoreManager.p2Score++;

			DestroyBullet (gameObject);
		}

		if (collision.gameObject.tag == "GOAL_B") {
			ScoreManager.p1Score++;

			DestroyBullet (gameObject);
		}

		Bounce(collision.contacts[0].normal);
	}

	void Bounce(Vector3 collisionNormal){
		var speed = lastFrameVelocity.magnitude;
		var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

//		Debug.Log("Out Direction: " + direction);
		rb.velocity = direction * Mathf.Max(speed, minVelocity);
	}

	public void DestroyBullet(GameObject bullet){
		player.bullets.Remove (bullet);
		Destroy (bullet);
	}
}

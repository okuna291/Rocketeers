using UnityEngine;
using System.Collections;

public class MoveSifter : MonoBehaviour {

	public Transform target;

	public Transform sand;

	public Vector3 prevPos;

	// Use this for initialization
	void Start () {
		prevPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime);
		GetComponent<ParticleSystem>().startSpeed = (transform.position.x - prevPos.x) + (transform.position.z - prevPos.z)* 1000;
		if ((transform.position.x - prevPos.x) + (transform.position.z - prevPos.z) > 0) {
			if (sand.localScale.y > 0) {
				sand.localScale -= new Vector3(0f, ((transform.position.x - prevPos.x) + (transform.position.z - prevPos.z))*0.005f, 0f);
			}
		}
		prevPos = transform.position;
	}
}

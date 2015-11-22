using UnityEngine;
using System.Collections;

public class LockLocalY : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (transform.localPosition.y > 0.05f) {
			transform.localPosition = new Vector3(transform.localPosition.x,0.05f,transform.localPosition.z);
		}
	}
}

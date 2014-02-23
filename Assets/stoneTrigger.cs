using UnityEngine;
using System.Collections;

public class stoneTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Component c = other.GetComponent("elementEnum");

		 //= elementEnum.element.earth;
	}
}

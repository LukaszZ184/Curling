using UnityEngine;
using System.Collections;

public class PlaneInit : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		//create a new material
		Material materialColored = new Material(Shader.Find("Diffuse"));
		materialColored.color = Color.white;
		this.renderer.material = materialColored;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

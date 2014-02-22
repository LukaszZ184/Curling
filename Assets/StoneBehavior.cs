using UnityEngine;
using System.Collections;

public class StoneBehavior : MonoBehaviour {
	
	private Material materialColored;

	private float force;
	private float spin;
	private float inputStart;

	// Use this for initialization
	void Start () 
	{			
		Reset ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Application.platform == RuntimePlatform.WindowsEditor || 
			Application.platform == RuntimePlatform.OSXEditor) 
		{
			CalculateClickPoints();
		}
		else
		{
			CalculateTouchPoints();
		}

		if (force != 0 && spin != 0) 
		{
			// Send the puck
			rigidbody.AddForce(new Vector3(0, 0, force));
		}
	}

	void Reset()
	{
		//create a new material
		materialColored = new Material(Shader.Find("Diffuse"));
		materialColored.color = Color.red;
		this.renderer.material = materialColored;
		
		force = 0;
		spin = 0;
	}



	void CalculateClickPoints()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// calculate force first
			if (force == 0)
			{
				inputStart = Input.mousePosition.y;
			}
			else
			{
				inputStart = Input.mousePosition.x;
			}
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			// calculate force first
			if (force == 0)
			{
				force = Input.mousePosition.y - inputStart;
				Debug.Log("Setting force = " + force);
			}
			else // calculate spin
			{
				spin = Input.mousePosition.x - inputStart;
				Debug.Log("Setting spin = " + spin);
			}
		}
	}

	void CalculateTouchPoints ()
	{
		// Get touch
		if (Input.touchCount > 0)
		{
			Touch touchPoint = Input.GetTouch(0);
			if (touchPoint.phase == TouchPhase.Began)
			{
				// calculate force first
				if (force == 0)
				{
					inputStart = touchPoint.position.y;
				}
				else
				{
					inputStart = touchPoint.position.x;
				}
			}
			
			if (touchPoint.phase == TouchPhase.Ended)
			{
				// calculate force first
				if (force == 0)
				{
					force = touchPoint.position.y - inputStart;
					Debug.Log("Setting force = " + force);
				}
				else // calculate spin
				{
					spin = touchPoint.position.x - inputStart;
					Debug.Log("Setting spin = " + spin);
				}
			}
		}
	}
}

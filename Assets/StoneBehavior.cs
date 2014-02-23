using UnityEngine;
using System.Collections;

public class StoneBehavior : MonoBehaviour {
	
	private Material materialColored;

	public float  FORCE_MULTIPLIER = 0.1f;
	public float  FRICTION = .1f;
	public GameObject prefab;
//	public elementEnum myEnum;

	private float force;
	private float inputStart;

	private float startX;
	private float startY;
	private float startZ;

	private bool  inMotion;
	private bool  finished;


	// Use this for initialization
	void Start () 
	{			
		startX = 0.0004494066f;
		startY = .1f;
		startZ = -8.000008f;
		Reset ();
		Debug.Log ("inMotion: " + inMotion);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Application.platform == RuntimePlatform.WindowsEditor || 
			Application.platform == RuntimePlatform.OSXEditor) 
		{
			if (!inMotion)
			{
				CalculateClickPoints();
			}
		}
		else
		{
			if (!inMotion)
			{
				CalculateTouchPoints();
			}
		}

		if (!inMotion && force != 0) 
		{
			// Send the puck
			rigidbody.AddForce(new Vector3(0, 0, force));
			
			inMotion = true;
		}

		if (finished) 
		{
			//switch(elementEnum){
			Vector3 vec = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
			Instantiate ( prefab, vec, this.transform.rotation);
			this.transform.position = new Vector3(0.0004494066f, .1f, -8.000008f);
			//}
			finished = false;
			Reset ();
		}

		ApplyFriction ();
	}

	void Reset()
	{		
		force = 0;
		inMotion = false;
		finished = false;

		this.transform.position = new Vector3(startX, startY, startZ);
	}

	void ApplyFriction()
	{

		
		if (inMotion && rigidbody.velocity.z > 0) 
		{
			rigidbody.AddForce (new Vector3 (0, 0, FRICTION * -1));
			if (rigidbody.velocity.z < 0.1)
			{
				rigidbody.velocity = new Vector3(0,0,0);
				finished = true;
			}
		}
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
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			// calculate force first
			if (force == 0)
			{
				force = (Input.mousePosition.y - inputStart) * FORCE_MULTIPLIER;
				Debug.Log("Setting force = " + force);
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
			}
			
			if (touchPoint.phase == TouchPhase.Ended)
			{
				// calculate force first
				if (force == 0)
				{
					force = touchPoint.position.y - inputStart;
					Debug.Log("Setting force = " + force);
				}
			}
		}
	}
}

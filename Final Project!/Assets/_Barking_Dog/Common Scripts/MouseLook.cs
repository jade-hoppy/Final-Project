using UnityEngine;


public class MouseLook : MonoBehaviour
{

		public float acceleration = 3.0f;
		public float speedX = 5.0f;
		public float speedZ = 5.0f;
		/// keyboard access
		public KeyCode fwdKey = KeyCode.W;
		public KeyCode leftKey = KeyCode.A;
		public KeyCode backKey = KeyCode.S;
		public KeyCode rightKey = KeyCode.D;
		//cursor
		public CursorLockMode lockCursor;
		//sensitivity
		public Vector2 sensitivity = new Vector2 (10, 10);
		public Vector2 smoothing = new Vector2 (10, 10);


	void Start ()
	{
		
	}
	
	void Update ()
	{
		Cursor.lockState = lockCursor;
		
		
		//mouse input
		var mouseDelta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		
		//scale input for sensitivity
		mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));
		
		
	}

	void FixedUpdate(){

		if (Input.GetKey (rightKey)) 
		{
			speedX += acceleration * Time.deltaTime;
		}
		else if (Input.GetKey (leftKey)) 
		{
			speedX -= acceleration * Time.deltaTime;
		}
		if (Input.GetKey (backKey))
		{
			speedZ -= acceleration * Time.deltaTime;
		} 
		else if (Input.GetKey (fwdKey)) 
		{
			speedZ += acceleration * Time.deltaTime;
		}

		

		transform.position = transform.TransformPoint( new Vector3( speedX,0,speedZ) );
	}

}

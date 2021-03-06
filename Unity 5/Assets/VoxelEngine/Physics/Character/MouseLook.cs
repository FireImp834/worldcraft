using UnityEngine;
using System.Collections;
using System;

enum Axis {
	X = 0,
	Y = 1,
	X_AND_Y = 2
}

public class MouseLook : MonoBehaviour {
	
	[SerializeField] private Axis axis = Axis.X_AND_Y;
	
	private float minimumY = -90f;
	private float maximumY = 90f;
	
	private Vector2 angles = Vector2.zero;

	public static void setSentivity( float s){
		InputManager.inputManager().sensitivity = s;
	}

	public static float getSentivity(){
		return InputManager.inputManager().sensitivity;
	}
	
	void Start() {
		angles = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		if(Cursor.visible) return;
		
		float x = Input.GetAxis("Mouse X");
		float y = -Input.GetAxis("Mouse Y");	
		Vector2 delta = new Vector2(y, x);
		
		angles += delta * InputManager.inputManager().sensitivity;
		angles.x = Mathf.Clamp(angles.x, minimumY, maximumY);
		
		if(axis == Axis.Y) angles.x = transform.localEulerAngles.x;
		if(axis == Axis.X) angles.y = transform.localEulerAngles.y;
		
		Quaternion targetRotation = Quaternion.Euler(angles);
		transform.localRotation = Quaternion.Slerp( transform.localRotation, targetRotation, 25*Time.deltaTime );
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;

	private Vector3 offset;

	// controls for rotation
	public float speedHori = 2.0f;
	private float verticalRotation = 0.0f;

	// controls for field of view
	private float minCameraZoom = 15f;
	private float maxCameraZoom = 90f;
	private float zoomSensitivity = 10f;

	// Start is called before the first frame update
	void Start()
	{
		offset = transform.position - player.transform.position;
	}

	void Update()
	{
		// get mouse data to help with rotation
		verticalRotation += speedHori*Input.GetAxis("Mouse X");
		verticalRotation = Mathf.Clamp(verticalRotation, -50f, 50f);   
		transform.eulerAngles = new Vector3(45.0f, verticalRotation, 0.0f);

		// controls for field of view using the scroll wheel
		float view = Camera.main.fieldOfView;
		view += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
		view = Mathf.Clamp(view, minCameraZoom, maxCameraZoom);
		Camera.main.fieldOfView = view;
	}

	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
	}
}

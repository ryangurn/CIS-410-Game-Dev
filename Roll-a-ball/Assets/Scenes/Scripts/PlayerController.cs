using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public float accel;
	public Text countText;


	private Rigidbody rigid;
	private int count;
	private Vector3 startPosition;

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		count = 0;
		startPosition = new Vector3(rigid.transform.position.x, rigid.transform.position.y, rigid.transform.position.z);
		SetCountText();
	}

	void Update()
	{
		// reset the position if they fall off the map
		if(rigid.transform.position.y <= -15.0f){
			rigid.transform.position = startPosition;
			rigid.velocity = new Vector3(0.0f, 0.0f, 0.0f);
			rigid.angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
			countText.text = "No points for you!";
			count = 0;
		}

		// jump, but not too high. This allows for double jump but not more.
		if (Input.GetKeyDown("space") && rigid.transform.position.y <= 1.7f)
		{
			Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
			rigid.AddForce(jump);
		} 
	}

	void FixedUpdate()
	{
		// keybindings for w, a, s, d or the arrow keys.
		float moveHori = Input.GetAxis("Horizontal");
		float moveVert = Input.GetAxis("Vertical");
		rigid.AddForce(new Vector3(moveHori, 0.0f, moveVert) * accel);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 15)
		{
			countText.text = "You Win!";
		}
	}
}

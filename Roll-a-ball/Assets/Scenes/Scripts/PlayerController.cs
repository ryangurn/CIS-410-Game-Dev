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

	void Start()
	{
		rigid = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
	}

	void Update()
	{
		if (Input.GetKeyDown("space") && rigid.transform.position.y <= 2.0f)
		{
			Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);
			rigid.AddForce(jump);
		}
	}

	void FixedUpdate()
	{
		float moveHori = Input.GetAxis("Horizontal");
		float moveVert = Input.GetAxis("Vertical");

		Vector3 movemt = new Vector3(moveHori, 0.0f, moveVert);

		rigid.AddForce(movemt * accel);
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
		if (count >= 10)
		{
			countText.text = "You Win!";
		}
	}
}

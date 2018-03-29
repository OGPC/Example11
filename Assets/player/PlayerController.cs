using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;
	public float jumpSpeed = 10f;
	public float gravity = 20f;
	[HideInInspector]
	public CharacterController cc;
	private Vector3 moveDirection = Vector3.zero;

	void Start () {
		cc = GetComponent<CharacterController>();
	}
	
	void Update () {
		if (cc.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = moveDirection * moveSpeed;
			transform.LookAt(transform.position + moveDirection, Vector3.up);
			if (Input.GetButtonDown("Jump"))
				moveDirection.y = jumpSpeed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		cc.Move(moveDirection * Time.deltaTime);
	}
}

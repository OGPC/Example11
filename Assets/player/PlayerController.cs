using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10f;
	public float jumpSpeed = 10f;
	public float gravity = 20f;
	public float maxTurnSpeed = 720f;

	[HideInInspector]
	public CharacterController cc;

	private Vector3 moveDelta = Vector3.zero;

	private void Start () {
		cc = GetComponent<CharacterController>();
	}

	private void Update () {
		if (cc.isGrounded) {
			moveDelta = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDelta = moveDelta * moveSpeed;


			if (Input.GetButtonDown("Jump"))
				moveDelta.y = jumpSpeed;
		}

		if (IgnoreVertical(moveDelta).sqrMagnitude != 0f) {
			transform.rotation = Quaternion.RotateTowards(
			transform.rotation,
			Quaternion.LookRotation(IgnoreVertical(moveDelta), Vector3.up),
			maxTurnSpeed * Time.deltaTime
		);
		}

		moveDelta.y -= gravity * Time.deltaTime;
		cc.Move(moveDelta * Time.deltaTime);
	}

	Vector3 IgnoreVertical (Vector3 input) {
		return new Vector3(input.x, 0, input.z);
	}
}

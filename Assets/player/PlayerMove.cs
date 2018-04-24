using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour {

	public float moveSpeed = 20f;
	public float jumpSpeed = 20f;
	public float hopSpeed = 6f;
	public float gravity = 50f;
	public float maxTurnSpeed = 720f;

	[HideInInspector]
	public CharacterController cc;

	private Vector3 moveDeltaXZ = Vector3.zero;
	private Vector3 moveDeltaY = Vector3.zero;

	private void Start () {
		cc = GetComponent<CharacterController>();
	}

	private void Update () {
		moveDeltaXZ.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDeltaXZ = moveDeltaXZ.normalized * Mathf.Clamp01(moveDeltaXZ.magnitude);
		moveDeltaXZ = moveDeltaXZ * moveSpeed;

		if (cc.isGrounded) {
			moveDeltaXZ.y = 0f;

			if (moveDeltaXZ.sqrMagnitude > 0.1f)
				moveDeltaY.y = hopSpeed;

			if (Input.GetButton("Jump"))
				moveDeltaY.y = jumpSpeed;
		}

		if (moveDeltaXZ.sqrMagnitude != 0f) {
			transform.rotation = Quaternion.RotateTowards(
				transform.rotation,
				Quaternion.LookRotation(moveDeltaXZ, Vector3.up),
				maxTurnSpeed * Time.deltaTime
		);
		}

		moveDeltaY.y -= gravity * Time.deltaTime;
		cc.Move((moveDeltaXZ + moveDeltaY) * Time.deltaTime);
	}
}

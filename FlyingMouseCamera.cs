using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FlyingMouseCamera : MonoBehaviour
{
	private float acceleration = 50f;
	private float accSprintMultiplier = 4f;
	private float lookSensitivity = 1f;
	private float dampingCoefficient = 5f;
	private bool focusOnEnable = true;
	private Vector3 velocity;
	static bool Focused
	{
		get => Cursor.lockState == CursorLockMode.Locked;
		set
		{
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = value == false;
		}
	}

	void OnEnable()
	{
		if (focusOnEnable) Focused = true;
	}

	void OnDisable() => Focused = false;

	void Update()
	{
		if (Focused)
        {
			UpdateInput();
		}
		else if (Input.GetMouseButtonDown(0))
        {
			Focused = true;
		}
		
		velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
	}

	void UpdateInput()
	{
		velocity += GetAccelerationVector() * Time.deltaTime;
		Vector2 mouseDelta = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
		Quaternion rotation = transform.rotation;
		Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
		Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
		transform.rotation = horiz * rotation * vert;

		if (Input.GetKeyDown(KeyCode.Escape))
        {
			Focused = false;
		}
			
	}

	Vector3 GetAccelerationVector()
	{
		Vector3 moveInput = default;
		void AddMovement(KeyCode key, Vector3 dir)
		{
			if (Input.GetKey(key))
				moveInput += dir;
		}
		AddMovement(KeyCode.W, Vector3.forward);
		AddMovement(KeyCode.S, Vector3.back);
		AddMovement(KeyCode.D, Vector3.right);
		AddMovement(KeyCode.A, Vector3.left);
		AddMovement(KeyCode.Space, Vector3.up);
		AddMovement(KeyCode.LeftControl, Vector3.down);
		Vector3 direction = transform.TransformVector(moveInput.normalized);

		if (Input.GetKey(KeyCode.LeftShift))
        {
			return direction * (acceleration * accSprintMultiplier);
		}
		return direction * acceleration;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    Vector2 moveAxis;
    Vector2 rotateAxis;
    Rigidbody playerRb;
    float camRotationX;

    public float speed;
    public float rotationSpeed;
    public float rotationPublic;
    public Transform camRotation;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }



    void OnMove(InputValue movementValue)
    {
        moveAxis = movementValue.Get<Vector2>() * speed;
    }



    void OnLook(InputValue rotateValue)
    {
        rotateAxis = rotateValue.Get<Vector2>() * rotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        camRotationX = Mathf.Clamp(camRotationX + rotateAxis.y, -30f, 30f);
        camRotation.eulerAngles = new Vector3(-camRotationX, camRotation.eulerAngles.y, 0f);
    }
    void FixedUpdate()
    {
        playerRb.velocity = transform.TransformDirection(new Vector3(moveAxis.x, playerRb.velocity.y, moveAxis.y));
        playerRb.angularVelocity = new Vector3(0f, rotateAxis.x, 0f);
    }
}

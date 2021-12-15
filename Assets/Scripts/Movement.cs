using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    bool isNoClip = false;
    private float forwardInput;
    private float horizontalInput;

    [Header("Movement Configuration")]
    public float moveSpeed = 4;
    public float gravity = 9.82f;
    public float acceleration = 5f;
    public float airAcceleration = 1500f;
    public float stopSpeed = 1.9f;
    public float airCap = .76f;
    public float friction = 4f;
    public float jumpForce = 10f;

    [Header("Mouse Configs")]
    public float mouseSensitivity = 1f;

    [Header("Other")]
    public Camera cam;
    public Collider playerCollider;

    private Rigidbody playerRB;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            ScreenCapture.CaptureScreenshot("Screenshit");

        /*var mouseX = Input.GetAxisRaw("Mouse X");
        var mouseY = -Input.GetAxisRaw("Mouse Y");
        var rot = Camera.main.transform.eulerAngles;
        var rotationVector = new Vector3(mouseY, mouseX, 0);
        rot += rotationVector * mouseSensitivity;
        Camera.main.transform.rotation = Quaternion.Euler(rot);*/


        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        

        if (Input.GetButton("Jump") && isGrounded)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * horizontalInput);

        /*var sideMove = Input.GetAxisRaw("Horizontal");
        var forwardMove = Input.GetAxisRaw("Vertical");
        var moveVector = new Vector3(sideMove, 0, forwardMove) * moveSpeed;
        moveVector = Camera.main.transform.TransformDirection(moveVector);
        transform.Translate(moveVector * Time.deltaTime);

        attempted no clip code*/

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isNoClip == false)
            {
                DisableCollision();
                isNoClip = true;
                playerCollider.enabled = false;
            }
            else
            {
                EnableCollision();
                isNoClip = false;
                playerCollider.enabled = true;
                
            }
        }
        

    }

    void DisableCollision()
    {
        playerRB.isKinematic = true;
        playerRB.useGravity = false;
    }

    void EnableCollision()
    {
        playerRB.isKinematic = false;
        playerRB.useGravity = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}

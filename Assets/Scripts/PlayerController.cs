using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15;
    public float jumpForce = 750; // Zıplama kuvveti
    public float sensitivity = 10.0f; // Mouse hassasiyeti
    private Vector3 moveDir;
    private float rotationX = 0;
    private bool isJumping = false;

    private Camera playerCamera;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Mouse girişi ile karakterin yatay dönüşünü hesapla
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 75); // Yukarı-aşağı sınırlama

        // Kamerayı yatay yönde döndür
        transform.Rotate(Vector3.up * mouseX);

        // Karakterin yatay dönüşünü uygula
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }

        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Zıplama işlemi
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce);
            isJumping = true; // Zıplama başladı
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 30;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 15;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            moveSpeed = 5;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            moveSpeed = 15;
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            isJumping = false;
        }
    }
}

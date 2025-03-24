using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pcontroller : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Animator animator;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
        UpdateAnimations();
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // -1, 0, 1
        float moveZ = Input.GetAxisRaw("Vertical");   // -1, 0, 1

        // คำนวณทิศทางสัมพันธ์กับกล้อง
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f; // ลบค่าแกน Y เพื่อป้องกันการเอียง
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * moveZ) + (cameraRight * moveX);
        moveDirection.Normalize();

        // หันหน้าไปทางทิศทางการเคลื่อนที่ (เฉพาะเมื่อมีการกดปุ่ม)
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        // เคลื่อนที่
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void UpdateAnimations()
    {
        // ใช้ GetAxisRaw แทน GetAxis เพื่อลดความไว
        float moveX = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.1f ? Input.GetAxisRaw("Horizontal") : 0f;
        float moveZ = Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.1f ? Input.GetAxisRaw("Vertical") : 0f;

        bool isMoving = (Mathf.Abs(moveX) + Mathf.Abs(moveZ)) > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        // ตั้งค่า Animator Parameters
        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", !isGrounded);

        // Debug Log เพื่อตรวจสอบค่า
        Debug.Log($"Moving: {isMoving}, Running: {isRunning}");
    }
}

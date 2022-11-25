using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] public float jumpForce = 6f;
    public LayerMask groundLayer;
    float rayLength = 0.85f;
    bool isFacingRight = true;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private bool isWalking = false;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        isWalking = false;
        float horizontalValue = Input.GetAxis("Horizontal");
        if (horizontalValue != 0)
        {
            if (horizontalValue < 0 && isFacingRight == true)
                Flip();
            if (horizontalValue > 0 && isFacingRight == false)
                Flip();
            isWalking = true;
            float moveSpeed = horizontalValue * horizontalSpeed * Time.deltaTime;
            transform.Translate(moveSpeed, 0, 0, Space.World);
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        animator.SetBool("isGrounded", isGrounded());
        animator.SetBool("Walking", isWalking);
        //Debug.DrawRay(transform.position, rayLength * Vector3.down, Color.white, 1, false);
    }

    bool isGrounded()
    {
        return Physics2D.Raycast(this.transform.position, Vector2.down, rayLength, groundLayer.value);
    }

    void Jump()
    {
        if(isGrounded())
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

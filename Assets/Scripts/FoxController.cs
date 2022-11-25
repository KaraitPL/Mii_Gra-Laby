using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] public float jumpForce = 6f;
    public LayerMask groundLayer;
    float rayLength = 0.85f;

    private Rigidbody2D rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        float moveSpeed = horizontalValue * horizontalSpeed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0, Space.World);

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

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
}

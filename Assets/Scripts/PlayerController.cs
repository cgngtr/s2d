using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] LayerMask oreMask;
    [SerializeField] float mineRadius = 1.2f;
    float moveDirection = 0;

    public TilemapController tilemapController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveDirection = InputManager.Instance.movementInput;
        Move();
        Vector3Int nearestTilePosition = tilemapController.FindNearestTile(transform.position);
    }

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.playerControls.Enable();
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.playerControls.Disable();
        }
    }

    public void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    public void Mine()
    {
        // Block that is going to get mined will light up.
    }

    void CheckGrounded()
    {
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, mineRadius);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, mineRadius, oreMask);
        Gizmos.color = Color.blue;
        foreach (var collider in colliders)
        {
            Gizmos.DrawWireSphere(collider.transform.position, 1);
        }
    }
}

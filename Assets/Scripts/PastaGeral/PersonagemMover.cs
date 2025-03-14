using UnityEngine;

public class PersonagemMover : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxSpeed = 2f, acceleration = 50f, deceleration = 100f;
    private float currentSpeed = 0f;
    private Vector2 OldMovimentInput;
    public Vector2 movimentInput { get; set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movimentInput.magnitude > 0f && currentSpeed >= 0f)
        {
            OldMovimentInput = movimentInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deceleration * maxSpeed * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        rb.linearVelocity = OldMovimentInput * currentSpeed;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movment
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private bool mayStrafe = true;
    public bool isMoving;

    //Sliding
    public float slideDecayRate = 0.95f;
    public float maxSlideSpeed = 20f;
    public float slideSpeedMultiplier = 1.5f;
    public bool isSliding;
    private float currentSlideSpeed;

    //IDK
    public CameraBob cameraBob;

    private Vector3 velocity;
    private bool isGrounded;
    private bool canDoubleJump;

    void Update()
    {
        CheckGrounded();
        Movment();
        Gump();
        Slide();
        ActuallySlide();
        Gravity();
        cameraBob.DoBob(IsPlayerMoving(), isSliding);
    }

    void CheckGrounded()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            velocity.y = -2f;
            canDoubleJump = true;
        }
    }

    void Movment()
    {
        float x = mayStrafe ? Input.GetAxis("Horizontal") : 0;
        float z = mayStrafe ? Input.GetAxis("Vertical") : 0;
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Gump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || canDoubleJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canDoubleJump = isGrounded;
                isGrounded = false;
            }
        }
    }

    void Slide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            isSliding = true;
            mayStrafe = false;
            currentSlideSpeed = maxSlideSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) || !isGrounded)
        {
            StopSliding();
        }
    }

    void StopSliding()
    {
        isSliding = false;
        mayStrafe = true;
    }

    void ActuallySlide()
    {
        if (isSliding) 
        {
            currentSlideSpeed *= slideDecayRate;
            isMoving = false;
            controller.Move(transform.forward * currentSlideSpeed * Time.deltaTime);
        }
    }

    bool IsPlayerMoving()
    {
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        return isMoving;
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
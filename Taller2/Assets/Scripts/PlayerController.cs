using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int MaxJumps;
    public LayerMask Terrain;

    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool LookingRight = true;
    private int JumpCount;
    private Animator Animation;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        Animation = GetComponent<Animator>();
        JumpCount = MaxJumps;
    }
    void Update()
    {
        MovementProccess();
        JumpProccess();
    }

    bool itsinFloor()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.1f, Terrain);
        return raycast.collider != null;
    }
    void JumpProccess()
    {

        if (itsinFloor())
        {
            JumpCount = MaxJumps;
            Animation.SetBool("IsJumping", false);
        }
        else
        {
            Animation.SetBool("IsJumping", true);
        }


        if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0)
        {
            JumpCount--;
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0f);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void MovementProccess()
    {
        float InputMovement = Input.GetAxis("Horizontal");

        if (InputMovement != 0f)
        {
            Animation.SetBool("IsRunning", true);
        }
        else
        {
            Animation.SetBool("IsRunning", false);
        }

        rigidbody.linearVelocity = new Vector2(InputMovement * speed, rigidbody.linearVelocity.y);

        ManageOrientation(InputMovement);
    }
    
    void ManageOrientation(float InputMovement)
    {
        if ((LookingRight == true && InputMovement < 0) || (LookingRight == false && InputMovement > 0))
        {
            LookingRight = !LookingRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}



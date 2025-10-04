using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{


    private bool recibiendodanio;
    public float fuerzaRebote = 3f;
    public float speed;
    public float jumpForce;
    public int MaxJumps;
    public LayerMask Terrain;
    public GameObject fireballPrefab;
    public AudioClip jumpSound;

    private new Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool LookingRight = true;
    private int JumpCount;
    private Animator Animation;
    private GameObject currentFireball = null;


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
        LaunchFireball();
    }


    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if (!recibiendodanio)
        {
            recibiendodanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rigidbody.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            Animation.SetBool("Daño", recibiendodanio);
        }
    }

  
    public void desactivandoDanio()
    {
        recibiendodanio = false;
        Animation.SetBool("Daño", recibiendodanio );
    }

    bool itsinFloor()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.1f, Terrain);
        return raycast.collider != null;
    }
    void JumpProccess()
    {
        float velocityY = rigidbody.linearVelocity.y;

        
        if (Mathf.Abs(velocityY) < 0.01f)
            velocityY = 0f;

        Animation.SetFloat("JumpVelocity", velocityY);

        if (itsinFloor())
        {
            JumpCount = MaxJumps;
            Animation.SetBool("IsGrounded", true);
        }
        else
        {
            Animation.SetBool("IsGrounded", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && JumpCount > 0 && !recibiendodanio)
        {
            JumpCount--;
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0f);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(jumpSound);
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

    void LaunchFireball()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentFireball == null)
        {
            currentFireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            currentFireball.GetComponent<Fireball>().direction = LookingRight ? 1 : -1;
        }
    }

    public void ClearFireball()
    {
        currentFireball = null;
    }

}



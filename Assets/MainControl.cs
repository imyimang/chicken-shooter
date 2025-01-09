using UnityEngine;
using UnityEngine.SceneManagement; // 引入場景管理命名空間

public class MainControl : MonoBehaviour
{
    public Sprite leftSprite;
    public Sprite rightSprite;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 3f;
    public GameObject eggPrefab;
    public float eggSpeed = 10f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (spriteRenderer == null || rb == null)
        {
            Debug.LogError("請確認 SpriteRenderer 和 Rigidbody2D 組件是否已正確添加。");
        }

        rb.gravityScale = gravityScale;

        if (eggPrefab != null)
        {
            eggPrefab.SetActive(false);
        }
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        rb.velocity = move;

        if (moveInput > 0)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.sprite = leftSprite;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (!isGrounded && Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - jumpForce * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShootEgg();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadGame(); // 重新載入場景
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void ShootEgg()
    {
        if (eggPrefab != null)
        {
            GameObject egg = Instantiate(eggPrefab, transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity); // 創建蛋的位置
            egg.SetActive(true);

            Rigidbody2D eggRb = egg.GetComponent<Rigidbody2D>();

            if (spriteRenderer.sprite == rightSprite)
            {
                eggRb.velocity = new Vector2(-eggSpeed, eggSpeed);
            }
            else if (spriteRenderer.sprite == leftSprite)
            {
                eggRb.velocity = new Vector2(eggSpeed, eggSpeed);
            }
        }
    }

    void ReloadGame()
    {
        // 使用 SceneManager 重新載入目前的場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

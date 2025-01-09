using UnityEngine;

public class FarmerControl : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public Sprite defaultSprite;
    public Sprite hitSprite; 
    public float hitDuration = 0.1f; 

    private bool movingRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = defaultSprite;
    }

    void Update()
    {
        MoveNPC();
    }

    void MoveNPC()
    {
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            Flip();
        }

        if (collision.gameObject.CompareTag("Egg"))
        {
            StartCoroutine(ShowHitSprite());
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    System.Collections.IEnumerator ShowHitSprite()
    {
        spriteRenderer.sprite = hitSprite;

        yield return new WaitForSeconds(hitDuration);


        spriteRenderer.sprite = defaultSprite;
    }
}
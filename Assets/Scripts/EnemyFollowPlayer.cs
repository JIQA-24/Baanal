using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float lineOfSight = 10f;
    private Transform player;
    private float startingEnemyHealth = 30f;
    private float currentEnemyHealth;
    [SerializeField] private float damage;
    private CacaoScript cacao;
    private Rigidbody2D rb;
    private SpriteRenderer sRenderer;
    private Color oGColor;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentEnemyHealth = startingEnemyHealth;
        cacao = GameObject.Find("ItemAssets").GetComponent<CacaoScript>();
        rb = GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<SpriteRenderer>();
        oGColor = sRenderer.color;
    }


    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer < lineOfSight){
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void TakeDamage(float _damage, Vector2 direction, float force)
    {
        currentEnemyHealth = Mathf.Clamp(currentEnemyHealth - _damage, 0, startingEnemyHealth);
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
        StartCoroutine(Knockback());
        if (currentEnemyHealth <= 0){
            Destroy(gameObject);
            cacao.SpawnCacao(GetComponent<Transform>().position, 120);
        }
    }

    private IEnumerator Knockback()
    {
        speed = 0;
        sRenderer.color = Color.magenta;
        yield return new WaitForSeconds(0.3f);
        sRenderer.color = oGColor;
        speed = 5f;
    }
}

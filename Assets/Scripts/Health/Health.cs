using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public bool dead;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;

    [SerializeField] public Shooter shoot;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake() {
        currentHealth = startingHealth;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(currentHealth > numOfHearts)
        {
            currentHealth = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].color = new Color32(255,255,255,255);
            }
            else
            {
                hearts[i].color = new Color32(0, 0, 0, 255);
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0){
            StartCoroutine(Invulerability());
            SoundManager.PlaySound(SoundManager.Sound.PlayerHit); //reproduce audio de golpe al jugador
        } else {
            if(!dead){
                GetComponent<PlayerMovement>().enabled = false;
                shoot.canShoot = false;
                dead = true;
                spriteRend.color = new Color(1,0,0,1);
            }
        }
    }

    private IEnumerator Invulerability(){
        Physics2D.IgnoreLayerCollision(6,7,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6,7,false);
    } 

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public bool GetDead()
    {
        return dead;
    }

}

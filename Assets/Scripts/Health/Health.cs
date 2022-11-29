using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Health : MonoBehaviourPunCallbacks
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public bool dead;
    public int numOfHearts;
    private Image[] hearts;
    public Sprite fullHeart;

    [SerializeField] public Shooter shoot;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private ReferenceScript reference;
    private PauseMenu deadUI;
    private SpriteRenderer spriteRend;

    public int id;
    public Player photonPlayer;
    private CharacterController2D controller;

    private void Awake() {
        reference = GameObject.Find("ReferenceObject").GetComponent<ReferenceScript>();
        controller = gameObject.GetComponent<CharacterController2D>();
        deadUI = reference.GetPauseMenu();
        currentHealth = startingHealth;
        hearts = reference.GetHeartList();
        spriteRend = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
        Physics2D.IgnoreLayerCollision(11, 13, true);
        Physics2D.IgnoreLayerCollision(7, 12, true);
        Physics2D.IgnoreLayerCollision(12, 13, true);
        controller = this.GetComponent<CharacterController2D>();
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
            gameObject.GetComponent<TimeStop>().StopTime(0.1f, 10, 0.1f);
        } else {
            if(!dead){
                GetComponent<PlayerMovement>().enabled = false;
                shoot.canShoot = false;
                dead = true;
                spriteRend.color = new Color(1,0,0,1);
                deadUI.DeadMenu();
            }
        }
    }

    private IEnumerator Invulerability(){
        Physics2D.IgnoreLayerCollision(6,7,true);
        Physics2D.IgnoreLayerCollision(6,10,true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6,7,false);
        Physics2D.IgnoreLayerCollision(6,10,false);
    } 

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public bool GetDead()
    {
        return dead;
    }

    [PunRPC]
    public void Prueba2(Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;
        //_GameController.instance.GetComponent<PlayerMovement>();

        if (!photonView.IsMine) // Verificar si el movimiento es del usuario actual
        {
            controller.m_Rigidbody2D.isKinematic = true;
        }
    }

}

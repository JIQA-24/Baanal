using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BossHealthSystem : MonoBehaviourPunCallbacks
{

    private float startingBossHealth = 100;
    private float currentBossHealth;
    public BossHealthBar healthBar;
    public GameObject ImpactEffect;
    public Animator Boss;
    [SerializeField] private PauseMenu pauseMenu;

    public int id;
    public Player photonPlayer;
    private CharacterController2D controller;
    // Start is called before the first frame update
    void Start()
    {
        currentBossHealth = startingBossHealth;
        healthBar.SetHealth(currentBossHealth, startingBossHealth);
    }

    public void BossTakeDamage(float _damage)
    {
        currentBossHealth = Mathf.Clamp(currentBossHealth - _damage, 0, startingBossHealth);
        healthBar.SetHealth(currentBossHealth, startingBossHealth);
        Instantiate(ImpactEffect, transform.position, Quaternion.identity);
        SoundManager.PlaySound(SoundManager.Sound.VucubImpacto);
        if (currentBossHealth <= 0)
        {
            
            Boss.SetBool("muerte",true);
            healthBar.DeactivateHealthBar();
            pauseMenu.EndScreen();
        }
        
    }
    
    public float GetBossHealth()
    {
        return currentBossHealth;   
    }

    [PunRPC]
    void boss_health(int _id)
    {
        photonView.RPC("Update", RpcTarget.All);
    }
}

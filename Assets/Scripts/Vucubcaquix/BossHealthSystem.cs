using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{

    private float startingBossHealth = 700f;
    private float currentBossHealth;
    public BossHealthBar healthBar;
    [SerializeField] private PauseMenu pauseMenu;
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
        if (currentBossHealth <= 0)
        {
            Destroy(gameObject);
            healthBar.DeactivateHealthBar();
            pauseMenu.EndScreen();
        }
    }

    public float GetBossHealth()
    {
        return currentBossHealth;
    }
}

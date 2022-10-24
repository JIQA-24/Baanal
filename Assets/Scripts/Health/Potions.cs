using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Potions : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image potions;
    [SerializeField] public TextMeshProUGUI numberOfPotionsT;
    [SerializeField] private int numberOfPotions;
    [SerializeField] private float potionValue;
    public float cooldown = 5f;
    bool isCooldown = false;


    // Update is called once per frame
    void Update()
    {
        CheckPotions();
    }

    private void CheckPotions(){
        if(PauseMenu.gameIsPaused || playerHealth.dead){
            return;
        }
        if(Input.GetButtonDown("potion") && playerHealth.currentHealth < 4 && numberOfPotions > 0 && !isCooldown){
            isCooldown = true;
            numberOfPotions -= 1;
            potions.fillAmount = 0;
            playerHealth.AddHealth(potionValue);
            SoundManager.PlaySound(SoundManager.Sound.Potion);
        }
        numberOfPotionsT.text = numberOfPotions.ToString();

        if(isCooldown){
            potions.fillAmount += 1 / cooldown * Time.deltaTime;

            if(potions.fillAmount >= 1){
                potions.fillAmount = 1;
                isCooldown = false;
            }
        }
    }
}

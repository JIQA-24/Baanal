using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GunChangeUI : MonoBehaviour
{

    [SerializeField] private Image defaultGun;
    [SerializeField] private Image defaultGunBlack;
    [SerializeField] private Image chaacMask;
    [SerializeField] private Image chaacMaskBlack;
    [SerializeField] private Image jaguarMask;
    [SerializeField] private Image jaguarMaskBlack;

    [SerializeField] private Image bombs;
    [SerializeField] public TextMeshProUGUI numberOfBombsText;
    [SerializeField] public TextMeshProUGUI timeOfCooldownMask;
    public int numberOfBombs;
    public float cooldownBombs = 3f;
    public bool isBombsCooldown = false;

    private IEnumerator coroutine;

    public float maskReturnCooldown = 20f;
    public float maskReturnCooldownVariable = 20f;
    public bool maskReturnOnCooldown = false;
    private float maskCooldown = 20f;
    public bool isChangeCooldown = false;
    public Shooter fireArm;


    public void ChangeUI(int _mask){
        switch(_mask){
            default:
            case 2:
                Restart();
                jaguarMask.fillAmount = 1;
                jaguarMaskBlack.fillAmount = 1;
                break;
            case 1:
                Restart();
                chaacMask.fillAmount = 1;
                chaacMaskBlack.fillAmount = 1;
                break;
            case 0:
                Restart();
                chaacMaskBlack.fillAmount = 1;
                break;
        }
    }

    public void ActivateMask()
    {
        isChangeCooldown = true;
    }

    public void Restart()
    {
        defaultGun.fillAmount = 0;
        defaultGunBlack.fillAmount = 0;
        chaacMask.fillAmount = 0;
        chaacMaskBlack.fillAmount = 0;
        jaguarMask.fillAmount = 0;
        jaguarMaskBlack.fillAmount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Restart();
        chaacMaskBlack.fillAmount = 1;
        isChangeCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        numberOfBombsText.text = numberOfBombs.ToString();
        if(isBombsCooldown)
        {
            bombs.fillAmount += 1 / cooldownBombs * Time.deltaTime;

            if(bombs.fillAmount >= 1)
            {
                bombs.fillAmount = 1;
                isBombsCooldown = false;
            }
        }

        if (maskReturnOnCooldown)
        {
            MaskReturn();
            return;
        }

        if (isChangeCooldown && fireArm.fireArm == 2)
        {

            jaguarMask.fillAmount -= 1 / maskCooldown * Time.deltaTime;

            if (jaguarMask.fillAmount <= 0)
            {
                fireArm.fireArm = 0;
                isChangeCooldown = false;
                maskReturnOnCooldown = true;
                fireArm.changeOfInventory();
            }

        }

        if (isChangeCooldown && fireArm.fireArm == 1){

            chaacMask.fillAmount -= 1 / maskCooldown * Time.deltaTime;

            if(chaacMask.fillAmount <= 0){
                fireArm.fireArm = 0;
                isChangeCooldown = false;
                maskReturnOnCooldown = true;
                fireArm.changeOfInventory();
            }
        
        }

    }

    public void ChangeBomb()
    {
        bombs.fillAmount = 0;
        numberOfBombs -= 1;
        isBombsCooldown = true;
    }

    private void MaskReturn()
    {
        maskReturnCooldown -= Time.deltaTime;
        timeOfCooldownMask.text = Mathf.RoundToInt(maskReturnCooldown).ToString() + "s";
        if (maskReturnCooldown <= 0)
        {
            maskReturnCooldown = 20f;
            maskReturnCooldownVariable = 20f;
            maskReturnOnCooldown = false;
        }
        if (fireArm.GetEquippedItems()[0].weaponChangeNum == 2)
        {
            jaguarMaskBlack.fillAmount = 1;
            jaguarMask.fillAmount += 1 / maskReturnCooldownVariable * Time.deltaTime;

            if (jaguarMask.fillAmount >= 1)
            {
                jaguarMask.fillAmount = 1;
                isChangeCooldown = false;
            }

        }
        if (fireArm.GetEquippedItems()[0].weaponChangeNum == 1)
        {
            chaacMaskBlack.fillAmount = 1;
            chaacMask.fillAmount += 1 / maskReturnCooldownVariable * Time.deltaTime;

            if (chaacMask.fillAmount >= 1)
            {
                chaacMask.fillAmount = 1;
                isChangeCooldown = false;
            }

        }
    }

    public void ButtonPressed()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = ShowText();
        StartCoroutine(coroutine);
    }

    private IEnumerator ShowText()
    {
        timeOfCooldownMask.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        timeOfCooldownMask.gameObject.SetActive(false);

    }
}

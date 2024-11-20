using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public Movement MV;

    public Image abilityImage1;
    public float cooldown1;
    bool isCooldown1 = false;
    public KeyCode Ability1;

    public Image abilityImage2;
    public float cooldown2;
    bool isCooldown2 = false;
    public KeyCode Ability2;

    public Mana M;
    public Health HP;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
    }

    void Update()
    {
        Skill1();
        ULT();
    }

    void Skill1(){
        if (Input.GetKey(Ability1) && isCooldown1 == false){
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
            Debug.Log("Q key was pressed!");
            MV.ActivateSkillOne();
            M.Cost1();
        }

        if(isCooldown1){
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0){
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }


    void ULT(){
        if (Input.GetKey(Ability2) && isCooldown2 == false){
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            Debug.Log("R key was pressed!");
            MV.ActivateSkillTwo();
            M.Cost2();
        }

        if(isCooldown2){
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if(abilityImage2.fillAmount <= 0){
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
}

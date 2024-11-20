using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public Slider playerSlider3D;
    private Slider playerSlider2D;

    public int maxMana = 300;
    public float manaRegenRate = 2f;
    public float currentMana;

    void Start()
    {
        playerSlider2D = GetComponent<Slider>();

        currentMana = maxMana;
        playerSlider2D.maxValue = maxMana;
        playerSlider3D.maxValue = maxMana;

        playerSlider2D.value = currentMana;
        playerSlider3D.value = currentMana;
    }

    void Update()
    {
        RegenerateMana();

        playerSlider2D.value = currentMana;
        playerSlider3D.value = playerSlider2D.value;
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            currentMana = Mathf.Min(currentMana, maxMana);
        }
    }

    public void Cost1(){
        currentMana = currentMana - 15;
    }

    public void Cost2(){
        currentMana = currentMana -105;
    }
}

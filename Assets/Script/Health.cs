using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider playerSlider3D;
    Slider playerSlider2D;

    public Stat HP;

    void Start(){
        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = HP.maxHealth;
        playerSlider3D.maxValue = HP.maxHealth;
    }

    void Update(){
        playerSlider2D.value = HP.currentHealth;
        playerSlider3D.value = playerSlider2D.value;
    }

    public void Drain1(){
        HP.currentHealth = HP.currentHealth - 50;
    }
}

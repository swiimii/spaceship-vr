using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int maxHealth = 5, health;
    public GameObject healthDisplay, failMenu;
    public GameObject[] controllers;
    float highestHealthScale;

    public void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("IsPlaying", 1);
        health = maxHealth;
        highestHealthScale = healthDisplay.transform.localScale.y;
    }

    public void OnDamage(int damage)
    {
        health -= damage;
        healthDisplay.transform.localScale= new Vector3(healthDisplay.transform.localScale.x, highestHealthScale * health / maxHealth, healthDisplay.transform.localScale.z);
        if (health <= 0)
        {
            foreach (var controller in controllers)
            {
                GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>().enabled = true;
                GetComponent<LineRenderer>().enabled = true;
                GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>().enabled = true;
            }
            failMenu.SetActive(true);
            PlayerPrefs.SetInt("IsPlaying", 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int maxHealth = 5, health;
    public GameObject healthDisplay, failMenu, scoreDisplay, cockpit;
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
        healthDisplay.transform.localScale = new Vector3(healthDisplay.transform.localScale.x, Mathf.Max(0, highestHealthScale * health / maxHealth), healthDisplay.transform.localScale.z);
        if (health >= 0)
        {
            cockpit.GetComponent<AudioSource>().Play();
        }
        if (health <= 0)
        {
            foreach (var controller in controllers)
            {
                controller.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRayInteractor>().enabled = true;
                controller.GetComponent<LineRenderer>().enabled = true;
                controller.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual>().enabled = true;
            }
            failMenu.SetActive(true);
            scoreDisplay.GetComponent<TextMesh>().text = "Score: " + PlayerPrefs.GetInt("Score") + " \nTry Again?";
            PlayerPrefs.SetInt("IsPlaying", 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject healthBarUIPrefab;
    public Transform healthBarParent;
    public Transform healthBarPoint;

    private int health = 0;
    private Slider healthSlider;
    private Renderer rend;

    void Start()
    {
        health = maxHealth;
        GameObject clone = Instantiate(healthBarUIPrefab, healthBarParent);
        healthSlider = clone.GetComponent<Slider>();

        rend = GetComponent<Renderer>();
    }

    void OnDestroy()
    {
        Destroy(healthSlider.gameObject);
    }

    void LateUpdate()
    {

        if (rend.isVisible)
        {
            healthSlider.gameObject.SetActive(true);
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPoint.position);
            healthSlider.transform.position = screenPosition;
        }
        else
        {
            healthSlider.enabled = false;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // Update value of slider
        healthSlider.value = (float)health / (float)maxHealth; // Converts 0-100 to 0-1 (current/max)
                                                 // If health is zero
        if (health < 0)
        {
            // Destroy GameObject
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public Image healthBar;
	public float maxHealth= 100f;
	public float currentHealth = 0f;
	public Text healthUIText;
	public Sprite[] liveHearts;
	public Image heartUI;
	public int lives = 1;

	void Start () {
		currentHealth = 100f;
		healthUIText.text = currentHealth.ToString ();
	}

	void Update () {
		heartUI.sprite = liveHearts [lives];
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		} 
		else if (currentHealth <= 0) {
			currentHealth = 0;
		}
	}

	public void TakeDamage (float damage) {
		currentHealth -= damage;
		float calcHealth = currentHealth / maxHealth;
		SetHealth (calcHealth);
	}

	public void GiveHealth (float health) {
		currentHealth += health;
		float calcHealth = currentHealth / maxHealth;
		SetHealth (calcHealth);
	}

	void SetHealth(float myhealth) {
		healthBar.fillAmount = myhealth;
		healthUIText.text = currentHealth.ToString ();
	}

}
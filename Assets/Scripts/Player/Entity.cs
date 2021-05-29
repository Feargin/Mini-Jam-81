﻿using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
	[Header("------------------ Настройки кайдзю -----------------")]
	public int _health;
	[SerializeField] private int _currentHealth;
	[Space]
	[Header("--------------------- Системные --------------------")]
	[HideInInspector] public Movement movement;
	[SerializeField] private Image _healtBar;
	
	private void Awake()
	{
		movement = GetComponent<Movement>();
	}

	private void Start()
	{
		_currentHealth = _health;
		_healtBar.fillAmount = 1;
	}

	public void DealDamage(int damage)
	{
		_health -= damage;
		_healtBar.fillAmount = _health / _currentHealth;
		if (_health <= 0) Kill();
		
	}

	private void Kill()
	{
		Destroy(gameObject);
	}
}

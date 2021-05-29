using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
	[Header("------------------ Настройки кайдзю -----------------")]
	public int _health;
	public int MaxActionPoints = 4;
	public int _currentActionPoints;
	[SerializeField] private int _currentHealth;
	[Space]
	[Header("--------------------- Системные --------------------")]
	[HideInInspector] public Movement movement;
	[SerializeField] private Image _healtBar;
	
	private void OnEnable() => ChangeTurn.TheNextTurn += ResetActionPoints;
	private void OnDisable() => ChangeTurn.TheNextTurn -= ResetActionPoints;

	private void ResetActionPoints(bool f)
	{
		_currentActionPoints = MaxActionPoints;
	}
	private void Awake()
	{
		_currentActionPoints = MaxActionPoints;
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

using UnityEngine;
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
<<<<<<< Updated upstream
		_healtBar.fillAmount = _health / _currentHealth;
=======
		if(_healtBar != null)
			_healtBar.fillAmount = (float)_health / (float)_currentHealth;
>>>>>>> Stashed changes
		if (_health <= 0) Kill();
		
	}

	private void Kill()
	{
<<<<<<< Updated upstream
=======
		if(this is Enemy) Spawn.Instance.Enemyes.Remove(transform);
		else
		{
			Spawn.Instance.Players.Remove(transform);
			SpawnEgg.Instance.Spawner(transform.position);
		}

		var vfx = Instantiate(_vfx, transform.position, Quaternion.identity);
		Destroy(vfx, 1.5f);
>>>>>>> Stashed changes
		Destroy(gameObject);
	}
}

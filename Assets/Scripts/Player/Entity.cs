using UnityEngine;
using UnityEngine.tvOS;
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
	[SerializeField] private GameObject _vfx;
	
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
		if(_healtBar != null)
			_healtBar.fillAmount = (float)_health / (float)_currentHealth;
		if (_health <= 0) Kill();
		
	}

	private void Kill()
	{
		if(this is Enemy) Spawn.Instance.Enemyes.Remove(transform);
		else
		{
			Spawn.Instance.Players.Remove(transform);
			SpawnEgg.Instance.Spawner(transform.position);
		}

		var vfx = Instantiate(_vfx, transform.position, Quaternion.identity);
		Destroy(vfx, 1.5f);
		Destroy(gameObject);
	}
}

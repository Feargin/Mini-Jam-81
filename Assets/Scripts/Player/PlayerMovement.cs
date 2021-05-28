using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("------------- Dependencies --------------")]
	[SerializeField] private LayerMask _walkableMask;
	
	private PlayerEntity _selectedPlayer;
	
	private Camera _main;
	
	private void OnEnable() => PlayerSelector.OnPlayerSelect += OnPlayerSelect;
	private void OnDisable() => PlayerSelector.OnPlayerSelect -= OnPlayerSelect;
	
	private void Start()
	{
		_main = Camera.main;
	}
	
	private void OnPlayerSelect(PlayerEntity player)
	{
		_selectedPlayer = player;
	}
	
	private void Update()
	{
		if(Input.GetMouseButtonDown(0) && _selectedPlayer != null)
		{
			SelectTile();
		}
	}
    
	private void SelectTile()
	{
		if(Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _walkableMask))
		{
			_selectedPlayer.movement.MoveTo(hit.transform.position);
		}
	}
}

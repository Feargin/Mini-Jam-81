using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
	[Header("------------- Info ----------------------")]
	[ReadOnly] public PlayerEntity SelectedPlayer;
	
	[Header("------------- Dependencies --------------")]
	[SerializeField] private LayerMask _playerMask;
	
	#region Events
	public static event System.Action<PlayerEntity> OnPlayerSelect;
	#endregion
	
	private Camera _main;
	
	private void Start()
	{
		_main = Camera.main;
	}
	
    private void Update()
    {
	    if(Input.GetMouseButtonDown(0))
	    {
	    	SelectPlayerEntity();
	    }
    }
    
	private void SelectPlayerEntity()
	{
		if(Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _playerMask))
		{
			SelectedPlayer = hit.transform.gameObject.GetComponent<PlayerEntity>();
			OnPlayerSelect?.Invoke(SelectedPlayer);
		}
	}
}

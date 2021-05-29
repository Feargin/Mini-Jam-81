using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
	[Header("------------- Info ----------------------")]
	[ReadOnly] public Entity SelectedPlayer;
	
	[Header("------------- Dependencies --------------")]
	[SerializeField] private LayerMask _playerMask;
	
	#region Events
	public static event System.Action<Entity> OnPlayerSelect;
	public static event System.Action<Entity> OnPlayerDeselect;
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
		if(Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _playerMask, QueryTriggerInteraction.Ignore))
		{
			SelectedPlayer = hit.transform.gameObject.GetComponent<PlayerEntity>();
			if(SelectedPlayer != null)
			{
				//MoveHelper.Instance.SelectEntity(SelectedPlayer);
				OnPlayerSelect?.Invoke(SelectedPlayer);
			}
		}
	}
	
	public void Deselect()
	{
		if(SelectedPlayer != null)
		{
			SelectedPlayer = null;
			OnPlayerDeselect?.Invoke(SelectedPlayer);
		}
	}
}

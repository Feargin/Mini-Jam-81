using UnityEngine;

public class PlayerSelector : Singleton<PlayerSelector>
{
	[Header("------------- Info ----------------------")]
	[ReadOnly] public Entity SelectedPlayer;
	
	[Header("------------- Dependencies --------------")]
	[SerializeField] private LayerMask _playerMask;
	[SerializeField] private LayerMask _walkableMask;
	public Entity Target;
	
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
	    RaycastHit hit;
	    if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, _walkableMask, QueryTriggerInteraction.Ignore))
	    {
		    if(hit.transform.GetComponent<Tile>() && hit.transform.GetComponent<Tile>().EntityIn)
		    {
			    Map.Instance.ReloadSelectTiles();
			    hit.transform.GetComponent<Tile>().Selected = true;
			    Target = hit.transform.GetComponent<Tile>().EntityIn;
		    }
	    }
    }
    
	private void SelectPlayerEntity()
	{
		if(Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _playerMask, QueryTriggerInteraction.Ignore))
		{
			SelectedPlayer = hit.transform.gameObject.GetComponent<PlayerEntity>();
			if(SelectedPlayer != null)
			{
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

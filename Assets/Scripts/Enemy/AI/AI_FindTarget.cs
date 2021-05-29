using UnityEngine;

public class AI_FindTarget : AI
{
	public Entity Target;
	private Spawn _spawn;
    
	private void Start()
	{
		_spawn = Spawn.Instance;
	}
    
	public override void BeginState()
	{
		SelectPlayerTarget();
		ExitState();
	}
	
	private void SelectPlayerTarget()
	{
		Target = _spawn.Players[Random.Range(0, _spawn.Players.Count)].GetComponent<Entity>();
	}
}

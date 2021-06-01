using UnityEngine;

public class AI_FindTarget : AI
{
	public Entity Target;
	public Entity.Type PriorityTarget;
	
	private Spawn _spawn;
	protected Spawn Spawn
	{
		get 
		{
			if(_spawn == null)
				_spawn = Spawn.Instance;
			return _spawn;
		}
	}
    
	public override void BeginState()
	{
		SelectPlayerTarget();
		ExitState();
	}
	
	private void SelectPlayerTarget()
	{
		if(Spawn.Players.Count == 0)
			return;
		Target = null;
		foreach(var p in Spawn.Players)
		{
			if(p != null && Target != null)
			{
				if(Vector3.Distance(p.transform.position, transform.position)
					< Vector3.Distance(Target.transform.position, transform.position)
					&& p.type != PriorityTarget
					|| p.type == PriorityTarget)
				{
					Target = p.GetComponent<Entity>();
				}
			}
			else
			{
				Target = p.GetComponent<Entity>();
			}
		}
	}
}

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
		if(Target != null)
			ExitState();
	}
	
	private void SelectPlayerTarget()
	{
		foreach(var p in _spawn.Players)
		{
			if(p != null && Target != null)
			{
				if((p.transform.position - transform.position).magnitude 
					< (Target.transform.position - transform.position).magnitude)
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

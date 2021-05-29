using UnityEngine;

public class AI_Moving : AI
{
	public AI_FindTarget AI_Target;
	public Movement Movement;
	public Attak Attack;
	
	public override void BeginState()
	{
		CheckAttackArea();
		ExitState();
	}
	
	private void CheckAttackArea()
	{
		if(Attack.CanAttack(AI_Target.Target) == false)
		{
			Movement.MoveTo(this.Attack.FindClosestNode(AI_Target.Target).Position);
		}
		else
		{
			this.Attack.Attack(AI_Target.Target);
		}
	}
}
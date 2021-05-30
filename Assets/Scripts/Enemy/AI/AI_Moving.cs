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
	
	Vector3 targetPos;
	private void CheckAttackArea()
	{
		if(Attack.CanAttack(AI_Target.Target) == false)
		{
			targetPos = new Vector3();
			print(Attack +"_"+this.Attack.FindClosestNode(AI_Target.Target));
			Node closest = this.Attack.FindClosestNode(AI_Target.Target);
			if(closest == null)
				return;
			Vector2 v2 = closest.Position;
			targetPos.x = v2.x;
			targetPos.y = transform.position.y;
			targetPos.z = v2.y;
			Movement.MoveTo(targetPos, true);
		}
		else
		{
			this.Attack.Attack(AI_Target.Target);
		}
	}
	
	protected void OnDrawGizmos()
	{
		if(targetPos == null)
			return;
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(targetPos, 0.5f);
	}
}
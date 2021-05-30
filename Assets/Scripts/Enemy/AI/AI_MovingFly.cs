using UnityEngine;

public class AI_MovingFly : AI
{
	public AI_FindTarget AI_Target;
	public Movement Movement;
	
	public override void BeginState()
	{
		if (AI_Target.Target != null)
		{
			AttackArea();
		}
		else
		{
			ExitState();
		}
	}
	
	Vector3 targetPos;
	private void AttackArea()
	{
		print(0);
		Movement.MoveTo(AI_Target.Target.transform.position, true, OnCompleteMovement);
	}
	
	private void OnCompleteMovement()
	{
		print(1);
		//Movement.MoveRandomDir(OnCompleteRunAway);
	}
	
	private void OnCompleteRunAway()
	{
		print(2);
		ExitState();
	}
}

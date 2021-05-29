using System.Collections.Generic;
using UnityEngine;

public class Attak : MonoBehaviour
{
    public int _minDamage;
    public int _maxDamage;
	public List<Vector3Int> RangeAttak;
	[SerializeField] private GameObject _vfx;
	
	public void Attack(Entity entity)
	{
		entity.transform.position += Vector3.up * 0.5f;
	}
    
	public bool CanAttack(Entity entity)
	{
		//List<Node> moveArea =  movement.pathfinding.FindPossibleMovement(transform.position, entity._currentActionPoints, out List<Node> _obstacles);
		GridGraph grid = entity.movement.pathfinding.map.nodemap;
		
		if(CheckInFireRadius(grid) == false)
		{
			//print(FindClosestNode(entity));
			return false;
		}
		else return true;
	}
    
	public bool CheckInFireRadius(GridGraph grid)
	{
		foreach(Vector3Int attackPos in RangeAttak)
		{
			Node node = grid.WorldToNode(attackPos);
			if(node == grid.WorldToNode(transform.position))
			{
				return true;
			}
		}
		return false;
	}
	
	public Node FindClosestNode(Entity entity)
	{
		GridGraph grid = entity.movement.pathfinding.map.nodemap;
		Node main = grid.WorldToNode(transform.position);
		int closestDistance = AStar.GetWalkDistance(grid, main, grid.WorldToNode(entity.transform.position));
		Node closest = null;
		foreach(Vector3Int attackPos in RangeAttak)
		{
			Node node = grid.WorldToNode(attackPos);
			
			int distance = AStar.GetWalkDistance(grid, main, node);
			if(distance < closestDistance)
			{
				closestDistance = distance;
				closest = node;
			}
		}
		return closest;
	}
}

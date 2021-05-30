using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attak : MonoBehaviour
{
<<<<<<< Updated upstream
    public int _minDamage;
    public int _maxDamage;
    public List<Vector3> RangeAttak;
    [SerializeField] private GameObject _vfx;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
=======
	public int _damage = 1;
	public List<Vector3Int> RangeAttak;
	[SerializeField] private Projectile _projectile;
	//[SerializeField] private GameObject _vfx;
	
	public void Attack(Entity entity)
	{
		if(_projectile != null)
		{
			Projectile proj = Instantiate(_projectile, transform.position, Quaternion.identity);
			proj.Init(entity.transform.position);
			entity.DealDamage(_damage);
		}
		//entity.transform.position += Vector3.up * 0.5f;
	}
    
	public bool CanAttack(Entity entity)
	{
		//List<Node> moveArea =  movement.pathfinding.FindPossibleMovement(transform.position, entity._currentActionPoints, out List<Node> _obstacles);
		GridGraph grid = entity.movement.pathfinding.map.nodemap;
		print($"{entity} {entity.movement} {entity.movement.pathfinding} {entity.movement.pathfinding.map} {grid}");
		return CheckInFireRadius(entity, grid);
	}
    
	public bool CheckInFireRadius(Entity entity, GridGraph grid)
	{
		foreach(Vector3Int attackPos in RangeAttak)
		{
			Node node = grid.WorldToNode(transform.position + attackPos);
			if(node == grid.WorldToNode(entity.transform.position))
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
		Node entityNode = grid.WorldToNode(entity.transform.position);
		int closestDistance = AStar.GetWalkDistance(grid, main, entityNode, true);
		Node closest = null;
		foreach(Vector3Int attackPos in RangeAttak)
		{
			Node node = grid.WorldToNode(entity.transform.position + attackPos);
			if(node == null)
				continue;
			int distance = AStar.GetWalkDistance(grid, main, node, true);
			if(distance < closestDistance)
			{
				closestDistance = distance;
				closest = node;
			}
		}
		if(closest == null)
		{
			Vector2Int v2i = new Vector2Int();
			foreach(var n in grid.Neighbours(entityNode))
			{
				v2i.x = (int)entityNode.Position.x;
				v2i.y = (int)entityNode.Position.y;
				if(grid.Passable(v2i))
				{
					closest = n;
					break;
				}
			}
		}
		return closest;
	}
>>>>>>> Stashed changes
}

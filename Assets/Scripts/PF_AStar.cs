using System.Collections.Generic;
using UnityEngine;

public class PF_AStar : Singleton<PF_AStar>
{
	public Map map;
	
	public List<Node> FindPath(Vector3 _from, Vector3 _to)
	{
		Vector2Int from = map.nodemap.WorldToIndex(_from);
		Vector2Int to = map.nodemap.WorldToIndex(_to);
		Node nodeFrom = map.nodemap.Grid[from.x, from.y];
		Node nodeTo = map.nodemap.Grid[to.x, to.y];
		List<Node> path = AStar.Search(map.nodemap, nodeFrom, nodeTo);
		return path;
    }
}

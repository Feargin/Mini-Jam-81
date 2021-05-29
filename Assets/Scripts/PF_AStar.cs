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
    
	#region DEBUGG
	//List<Node> lastPath = new List<Node>();
	//[SerializeField] private LayerMask _walkableMask;
	//private void Update()
	//{
	//	if(Input.GetMouseButtonDown(1))
	//	{
	//		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, float.PositiveInfinity, _walkableMask))
	//		{
	//			lastPath.Clear();
	//			Vector2Int v2i = map.nodemap.WorldToIndex(hit.transform.position);
	//			Node n0 = map.nodemap.Grid[v2i.x, v2i.y];
	//			foreach(Node n in map.nodemap.Neighbours(n0))
	//			{
	//				lastPath.Add(n);
	//			}
	//		}
	//	}
	//}
	
	//protected void OnDrawGizmos()
	//{
	//	if(lastPath.Count <= 0)
	//		return;
	//	Gizmos.color = Color.yellow;
	//	foreach (Node n in lastPath)
	//	{
	//		Vector3 v3 = new Vector3(n.Position.x, 1f, n.Position.y);
	//		Gizmos.DrawWireSphere(v3, 0.3f);
	//	}
	//}
	#endregion
}

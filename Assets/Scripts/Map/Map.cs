using UnityEngine;

public class Map : MonoBehaviour
{
	public float GridSize = 1f;
	public GridGraph nodemap;
	public Tile[,] tiles;
    
	public void Init(int xSize, int ySize, Tile[,] tiles)
	{
		nodemap = new GridGraph(xSize, ySize, GridSize, this);
		this.tiles = tiles;
	}
	
	public bool Passable(Vector2Int pos)
	{
		//tiles[pos.x, pos.y].transform.position += Vector3.up * 0.3f;
		return tiles[pos.x, pos.y].Passable;
	}
}

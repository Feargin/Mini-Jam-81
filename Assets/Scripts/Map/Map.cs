using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map>
{
	public float GridSize = 1f;
	public GridGraph nodemap;
	public Tile[,] tiles;
    
	public void Init(int xSize, int ySize, Tile[,] tiles)
	{
		nodemap = new GridGraph(xSize, ySize, GridSize, this);
		this.tiles = tiles;
	}
	
	public bool Passable(Vector2Int pos, bool withEntity = false)
	{
		return tiles[pos.x, pos.y].IsPassable(withEntity);
	}
}

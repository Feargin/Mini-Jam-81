using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MapGeneratorTwoType : MonoBehaviour
{
    [Header("----------------------- Настройки карты -----------------------")]
    [Range(0, 0.3f)][SerializeField] private float border;
    [Range(6, 50)][SerializeField] private int xSize; //ширина карты
    [Range(6, 50)][SerializeField] private int ySize;
    [Space]
    [Range(0, 100)][SerializeField] private int Purity_CellTypeOne;
    [Range(0, 100)][SerializeField] private int Purity_CellTypeTwo;
    [Range(0, 100)][SerializeField] private int Purity_CellTypeThree;
    [Space]
    [SerializeField] private bool randomSeed; //включение рандомного семени
    [SerializeField] private string seed;
    [Space]
    [Header("--------------------------- Системное ---------------------------")]
    [SerializeField] private Transform CellTypeOne;
    [SerializeField] private Transform CellTypeTwo;
    [SerializeField] private Transform CellTypeThree;
    private int[,] coordinates;
    
    private void Start()
    {
        coordinates = new int[xSize, ySize];
        Generation();
    }

    private void Generation()
    {
        if (randomSeed)
        {
            seed = "" + Random.Range(-9999f, 9999f);
        }

        System.Random randHash = new System.Random(seed.GetHashCode());
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                var rand = randHash.Next(0, 100);
                if (Purity_CellTypeOne > rand) coordinates[x, y] = 0;
                else if (Purity_CellTypeTwo > rand) coordinates[x, y] = 1;
                else if (Purity_CellTypeThree > rand) coordinates[x, y] = 2;
                else
                {
	                coordinates[x, y] = 0;
                }
            }
        }

        Spawn();
    }

    private void Spawn()
    {
	    if (coordinates != null)
	    {
		    string nameGroupOne = "Ocean";
		    string nameGroupTwo = "Forest";
		    string nameGroupThree = "Desert";
		    string nameGroupFour = "Tundra";
		    string nameGroupFive = "SnowWasted";
		    string nameGroupSix = "Rock";
		    if (transform.Find(nameGroupOne))
		    {
			    DestroyImmediate(transform.Find(nameGroupOne).gameObject);
		    }

		    if (transform.Find(nameGroupTwo))
		    {
			    DestroyImmediate(transform.Find(nameGroupTwo).gameObject);
		    }

		    if (transform.Find(nameGroupThree))
		    {
			    DestroyImmediate(transform.Find(nameGroupThree).gameObject);
		    }

		    if (transform.Find(nameGroupFour))
		    {
			    DestroyImmediate(transform.Find(nameGroupFour).gameObject);
		    }

		    if (transform.Find(nameGroupFive))
		    {
			    DestroyImmediate(transform.Find(nameGroupFive).gameObject);
		    }

		    if (transform.Find(nameGroupSix))
		    {
			    DestroyImmediate(transform.Find(nameGroupSix).gameObject);
		    }


		    Transform mapGroupOne = new GameObject(nameGroupOne).transform;
		    mapGroupOne.parent = transform;
		    Transform mapGroupTwo = new GameObject(nameGroupTwo).transform;
		    mapGroupTwo.parent = transform;
		    Transform mapGroupThree = new GameObject(nameGroupThree).transform;
		    mapGroupThree.parent = transform;
		    Transform mapGroupFour = new GameObject(nameGroupFour).transform;
		    mapGroupFour.parent = transform;
		    Transform mapGroupFive = new GameObject(nameGroupFive).transform;
		    mapGroupFive.parent = transform;
		    Transform mapGroupSix = new GameObject(nameGroupSix).transform;
		    mapGroupSix.parent = transform;
		    for (int x = 0; x < xSize; x++)
		    {
			    for (int y = 0; y < ySize; y++)
			    {
				    if (coordinates[x, y] == 0)
				    {
					    Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
					    Transform newCell = Instantiate(CellTypeOne, cellPosition, Quaternion.identity) as Transform;
					    newCell.localScale = Vector3.one * (1 - border);
					    newCell.parent = mapGroupOne;
				    }

				    if (coordinates[x, y] == 1)
				    {
					    Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
					    Transform newECell = Instantiate(CellTypeTwo, cellPosition + Vector3.up * 0.2f, Quaternion.identity) as Transform;
					    newECell.localScale = Vector3.one * (1 - border);
					    newECell.parent = mapGroupTwo;
				    }
				    if (coordinates[x, y] == 2)
				    {
					    Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
					    Transform newECell = Instantiate(CellTypeThree, cellPosition + Vector3.up * 0.5f, Quaternion.identity) as Transform;
					    newECell.localScale = Vector3.one * (1 - border);
					    newECell.parent = mapGroupTwo;
				    }
			    }
		    }
	    }
    }
}

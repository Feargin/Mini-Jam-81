using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{

	public int xSize; //ширина карты
	public int ySize; //высота карты
	//public int index; //индекс рандомизатора биомов
	public Transform cellPref; //перфаб океана
	//public Transform cellRPref; //перфаб скал
	//public Transform cellSWPref; //перфаб снежных пустошей
	//public Transform cellTPref; //перфаб тундры
	public Transform cellDPref; //перфаб пустыни
	public Transform cellEPref; //перфаб леса
	[Range(0, 6)]
	public int Smooth;
	//public Text Water;
	// Элемены UI --------------------
	//public Text SizeB;
	//public InputField seedText;
	//public InputField Xsize;
	//public InputField Ysize;
	//public Camera cameraMain;
	//public Slider camSlider;
	//public Slider bordSlider;
	//public Toggle random;
	//public Slider amountWater;
	//----------------------------------
	[Range(0, 0.3f)]
	public float border; //выраженность границ клеток
	[Range(40, 60)]
	public float densityFactor; //плотность земли
	public bool randomSeed; //включение рандомного семени
	public string seed; //семя
	int[,] coordinates; //массив координат карты
	int[,] ea; //массив данных биомов

	void Start()
	{
		TotalGenerateMap();
	}
	public void Update() //передаем значения UI в переменные генератора
	{
		/*densityFactor = amountWater.value;
		border = bordSlider.value;
		Water.text = "" + Math.Round(amountWater.value, 1, MidpointRounding.AwayFromZero);
		SizeB.text = "" + Math.Round(bordSlider.value, 2, MidpointRounding.AwayFromZero);
		xSize = int.Parse(Xsize.text);
		ySize = int.Parse(Ysize.text);
		randomSeed = random.isOn;
		seed = seedText.text;*/
		//cameraMain.orthographicSize = camSlider.value;
	}

	public void Refresh() //кнопка генерации
	{
		TotalGenerateMap();
	}

	void TotalGenerateMap() //стартовый метод генерации
	{
		coordinates = new int[xSize, ySize]; //инициализация координатной сетки
		ea = new int[xSize, ySize];
		RandomMap();
		//RandomMap2();

		for (int i = 0; i < Smooth; i++) //цикл запуска сглаживания
		{
			Islands();
		}
		/*for (int i = 0; i < 2; i++) //цикл запуска сглаживания
		{
			Islands2();
		}*/

		cellGenerator();
	}
	void RandomMap() //рандомизация
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
				coordinates[x, y] = randHash.Next(0, 100) < densityFactor ? 1 : 0;
				/*if (x == 0 || y == 0 || y == ySize - 1 || x == xSize - 1)
				{
					coordinates[x, y] = 1;
				}
				else
				{
					coordinates[x, y] = randHash.Next(0, 100) < densityFactor ? 1 : 0;
				}*/
			}
		}

	}
	void RandomMap2() //рандомизация
	{
		if (randomSeed)
		{
			seed = Time.time.ToString();
		}
		System.Random randHash = new System.Random(seed.GetHashCode());
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				if (coordinates[x, y] == 0)
				{
					if (randHash.Next(0, 100) <= 30)
					{
						ea[x, y] = 0;
					}
					if (randHash.Next(0, 100) < 40 && randHash.Next(0, 100) > 30)
					{
						ea[x, y] = 1;
					}
					/*if (randHash.Next(0, 100) < 60 && randHash.Next(0, 100) > 40)
					{
						ea[x, y] = 2;
					}
					if (randHash.Next(0, 100) < 80 && randHash.Next(0, 100) > 60)
					{
						ea[x, y] = 3;
					}
					if (randHash.Next(0, 100) < 100 && randHash.Next(0, 100) > 80)
					{
						ea[x, y] = 4;
					}*/

				}
			}
		}

	}
	void Islands() //сглаживание в острова
	{
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				int neighbourBords = CheckingNeighbours(x, y);

				if (neighbourBords > 4)
				{
					coordinates[x, y] = 1;
				}
				else if (neighbourBords < 4)
				{
					coordinates[x, y] = 0;
				}
			}
		}

	}
	int CheckingNeighbours(int xRow, int yRow) //проверяем соседей
	{
		int rowCoint = 0;
		for (int xNeighbour = xRow - 1; xNeighbour <= xRow + 1; xNeighbour++)
		{
			for (int yNeighbour = yRow - 1; yNeighbour <= yRow + 1; yNeighbour++)
			{
				if (xNeighbour >= 0 && xNeighbour <= xSize && yNeighbour >= 0 && yNeighbour <= ySize)
				{
					
					if (xNeighbour != xRow || yNeighbour != yRow)
					{
						rowCoint += coordinates[xNeighbour, yNeighbour];
					}
				}
				else
				{
					rowCoint++;
				}
			}
		}
		return rowCoint;
	}
	/*void Islands2() //сглаживание в острова
	{
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				int neighbourBords = CheckingNeighbours2(x, y);

				if (neighbourBords > 1)
				{
					ea[x, y] = 1;
				}
				else if (neighbourBords < 1)
				{
					ea[x, y] = 0;
				}
			}
		}

	}
	int CheckingNeighbours2(int xRow, int yRow) //проверяем соседей
	{
		int rowCoint = 0;
		for (int xNeighbour = xRow - 1; xNeighbour <= xRow + 1; xNeighbour++)
		{
			for (int yNeighbour = yRow - 1; yNeighbour <= yRow + 1; yNeighbour++)
			{
				if (xNeighbour >= 0 && xNeighbour < xSize && yNeighbour >= 0 && yNeighbour < ySize)
				{
					if (xNeighbour != xRow || yNeighbour != yRow)
					{
						rowCoint += coordinates[xNeighbour, yNeighbour];
					}
				}
				else
				{
					rowCoint++;
				}
			}
		}
		return rowCoint;
	}*/
	void cellGenerator()
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
					if (coordinates[x, y] == 1)
					{
						Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
						Transform newCell = Instantiate(cellPref, cellPosition, Quaternion.identity) as Transform;
						newCell.localScale = Vector3.one * (1 - border);
						newCell.parent = mapGroupOne;
					}
					if (coordinates[x, y] == 0)
					{
						Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
						Transform newECell = Instantiate(cellEPref, cellPosition + Vector3.up * 0.5f, Quaternion.identity) as Transform;
						newECell.localScale = Vector3.one * (1 - border);
						newECell.parent = mapGroupTwo;
						/*if (ea[x, y] == 0)
						{
							
						}
						/*if (ea[x, y] == 1)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellDPref, cellPosition + Vector3.up * 0.2f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border);
							newECell.parent = mapGroupThree;
						}
						/*if (ea[x, y] == 2)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellTPref, cellPosition + Vector3.up * 0.06f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border) + Vector3.down / 1.3f;
							newECell.parent = mapGroupFour;
						}
						if (ea[x, y] == 3)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellEPref, cellPosition + Vector3.up * 0.06f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border) + Vector3.down / 1.3f;
							newECell.parent = mapGroupTwo;
						}
						if (ea[x, y] == 4)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellDPref, cellPosition + Vector3.up * 0.06f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border) + Vector3.down / 1.3f;
							newECell.parent = mapGroupThree;
						}*/
						/*if (index == 2)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellTPref, cellPosition + Vector3.up * 0.06f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border) + Vector3.down / 1.3f;
							newECell.parent = mapGroupFour;
						}
						if (index == 3)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellSWPref, cellPosition + Vector3.up * 0.06f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border) + Vector3.down / 1.3f;
							newECell.parent = mapGroupFive;
						}
						if (index == 4)
						{
							Vector3 cellPosition = new Vector3(-xSize / 2 + 0.5f + x, 0, -ySize / 2 + 0.5f + y);
							Transform newECell = Instantiate(cellRPref, cellPosition + Vector3.up * 0.06f, Quaternion.identity) as Transform;
							newECell.localScale = Vector3.one * (1 - border) + Vector3.down / 1.3f;
							newECell.parent = mapGroupSix;
						}*/

					}
				}
			}
		}
	}
}


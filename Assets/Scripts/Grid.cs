using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grid : MonoBehaviour
{
    [SerializeField] private Menu menu;

    [SerializeField] private float spead;

    [SerializeField] private GameObject[] grid;

    [SerializeField] private GameObject[] gridCells;

    [SerializeField] private LevelRule m_LevelRule;
    public LevelRule levelRule => m_LevelRule;

    [SerializeField] private Island islandPrefab;

    [SerializeField] private Ship shipPrefab;

    [SerializeField] private Void voidPrefab;

    [SerializeField] private Whirlpool whirlpoolPrefab;

    [SerializeField] private PlayerShip playerShipPrefab;

    [SerializeField] private PirateShip pirateShipPrefab;

    [SerializeField] private Treasure treasurePrefab;

    [SerializeField] private Kraken krakenPrefab;

    [SerializeField] private Warship warshipPrefab;

    [SerializeField] private Cell cellPrefab;

    private float shiftX;
    private float shiftY;

    private int move;

    private bool isMove;


    [SerializeField] private TextMeshProUGUI treasureUI;

    private int m_TerasureMax;
    public int treasureMax => m_TerasureMax;

    private int m_Treasure;
    public int treasure => m_Treasure;

    [SerializeField] private TextMeshProUGUI stormBeginUI;

    private int round;
    private int stormStartRound;

    private int playerShipCount;

    private bool continuation;

    private void Start()
    {
        StartNewLevel();
    }

    private void DestroyAndCheck(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerShip>() != null)
        {
            playerShipCount--;
        }
        Destroy(gameObject);
    }

    public void UpdateInformation()
    {
        if (m_Treasure == m_TerasureMax)
        {
            menu.OpenNextLevelMenu();
        }
        else if (playerShipCount <= 0)
        {
            menu.OpenDefeatMenu();
        }
        else if (round > stormStartRound && continuation == false)
        {
            continuation = true;
            menu.OpenDefeatResumeMenu();
        }
        treasureUI.text = m_Treasure + "/" + m_TerasureMax;
        stormBeginUI.text = round + "/" + stormStartRound;
    }

    public void SetLevelRule(LevelRule levelRule)
    {
        this.m_LevelRule = levelRule;
        StartNewLevel();
    }

    public void StartNewLevel()
    {
        continuation = false;

        for (int i = 0; i < grid.Length; i++)
        {
            Destroy(grid[i].gameObject);
        }
        for (int i = 0; i < gridCells.Length; i++)
        {
            Destroy(gridCells[i].gameObject);
        }


        m_TerasureMax = 0;
        m_Treasure = 0;

        round = 1;
        stormStartRound = m_LevelRule.stormStartRound;

        playerShipCount = 0;

        grid = new GameObject[m_LevelRule.gridID.Length];
        gridCells = new GameObject[m_LevelRule.gridID.Length];
        for (int j = 0; j < m_LevelRule.sizeX; j++)
        {
            for (int i = 0; i < m_LevelRule.sizeY; i++)
            {
                Cell newCell = Instantiate(cellPrefab);
                newCell.transform.SetParent(this.transform);
                gridCells[i + j * m_LevelRule.sizeY] = newCell.gameObject;

                if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 0)
                {
                    Void newVoid = Instantiate(voidPrefab);
                    newVoid.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newVoid.gameObject;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 1)
                {
                    Island newIsland = Instantiate(islandPrefab);
                    newIsland.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newIsland.gameObject;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 2)
                {
                    Ship newShip = Instantiate(shipPrefab);
                    newShip.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newShip.gameObject;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 3)
                {
                    Whirlpool newWhirlpool = Instantiate(whirlpoolPrefab);
                    newWhirlpool.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newWhirlpool.gameObject;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 4)
                {
                    PlayerShip newPlayerShip = Instantiate(playerShipPrefab);
                    newPlayerShip.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newPlayerShip.gameObject;
                    playerShipCount++;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 5)
                {
                    PirateShip newPirateShip = Instantiate(pirateShipPrefab);
                    newPirateShip.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newPirateShip.gameObject;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 6)
                {
                    Treasure newTreasure = Instantiate(treasurePrefab);
                    newTreasure.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newTreasure.gameObject;
                    m_TerasureMax++;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 7)
                {
                    Kraken newKraken = Instantiate(krakenPrefab);
                    newKraken.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newKraken.gameObject;
                }
                else if (m_LevelRule.gridID[i + j * m_LevelRule.sizeY] == 8)
                {
                    Warship newWarship = Instantiate(warshipPrefab);
                    newWarship.transform.SetParent(this.transform);
                    grid[i + j * m_LevelRule.sizeY] = newWarship.gameObject;
                }
            }
        }
        if (m_TerasureMax == 0)
        {
            m_TerasureMax = int.MaxValue;
        }
    }

    private void CheckMove(int from, int @in)
    {
        if (grid[@in].gameObject.GetComponent<Void>() != null)
        {
            if (grid[from].gameObject.GetComponent<Move>() != null)
            {
                grid[from].gameObject.GetComponent<Move>().isMove = true;
            }

        }
        if (grid[@in].gameObject.GetComponent<Move>() != null)
        {
            if (grid[@in].gameObject.GetComponent<Move>().isMove == true)
            {
                if (grid[from].gameObject.GetComponent<Move>() != null)
                {
                    grid[from].gameObject.GetComponent<Move>().isMove = true;
                }
            }
            if (grid[@in].gameObject.GetComponent<Ship>() != null || grid[@in].gameObject.GetComponent<PlayerShip>() != null)
            {
                if (grid[from].gameObject.GetComponent<Move>() != null && grid[from].gameObject.GetComponent<PirateShip>() != null)
                {
                    grid[from].gameObject.GetComponent<Move>().isMove = true;
                }
            }
            if (grid[@in].gameObject.GetComponent<Ship>() != null || grid[@in].gameObject.GetComponent<PirateShip>() != null || grid[@in].gameObject.GetComponent<PlayerShip>() != null || grid[@in].gameObject.GetComponent<Warship>() != null)
            {
                if (grid[from].gameObject.GetComponent<Move>() != null && grid[from].gameObject.GetComponent<Kraken>() != null)
                {
                    grid[from].gameObject.GetComponent<Move>().isMove = true;
                }
            }
            if (grid[@in].gameObject.GetComponent<PirateShip>() != null)
            {
                if (grid[from].gameObject.GetComponent<Move>() != null && grid[from].gameObject.GetComponent<Warship>() != null)
                {
                    grid[from].gameObject.GetComponent<Move>().isMove = true;
                }
            }
        }
        if (grid[@in].gameObject.GetComponent<Treasure>() != null)
        {
            if (grid[from].gameObject.GetComponent<Move>() != null && (grid[from].gameObject.GetComponent<PlayerShip>() != null || grid[from].gameObject.GetComponent<PirateShip>() != null || grid[from].gameObject.GetComponent<Ship>() != null || grid[from].gameObject.GetComponent<Warship>() != null))
            {
                grid[from].gameObject.GetComponent<Move>().isMove = true;
            }
        }
    }

    private void SwapMove(int from, int @in)
    {
        if (grid[from].gameObject.GetComponent<Move>() == true)
        {
            if (grid[from].gameObject.GetComponent<Move>().isMove == true)
            {
                if (grid[@in].gameObject.GetComponent<Whirlpool>() == true)
                {
                    DestroyAndCheck(grid[from].gameObject);
                    Void newVoid = Instantiate(voidPrefab);
                    newVoid.transform.SetParent(this.transform);
                    grid[from] = newVoid.gameObject;
                }
                else if (grid[from].gameObject.GetComponent<PirateShip>() != null && (grid[@in].gameObject.GetComponent<Ship>() != null || grid[@in].gameObject.GetComponent<PlayerShip>() != null))
                {
                    DestroyAndCheck(grid[@in].gameObject);
                    Void newVoid = Instantiate(voidPrefab);
                    newVoid.transform.SetParent(this.transform);
                    grid[@in] = newVoid.gameObject;

                    GameObject temp = grid[from];
                    grid[from] = grid[@in];
                    grid[@in] = temp;
                    isMove = true;
                }
                else if ((grid[from].gameObject.GetComponent<PirateShip>() != null || grid[from].gameObject.GetComponent<PlayerShip>() != null || grid[from].gameObject.GetComponent<Ship>() != null || grid[from].gameObject.GetComponent<Warship>() != null) && grid[@in].gameObject.GetComponent<Treasure>() != null)
                {
                    DestroyAndCheck(grid[@in].gameObject);
                    Void newVoid = Instantiate(voidPrefab);
                    newVoid.transform.SetParent(this.transform);
                    grid[@in] = newVoid.gameObject;

                    if (grid[from].gameObject.GetComponent<PlayerShip>() != null)
                    {
                        m_Treasure++;
                    }

                    GameObject temp = grid[from];
                    grid[from] = grid[@in];
                    grid[@in] = temp;
                    isMove = true;
                }
                else if (grid[from].gameObject.GetComponent<Kraken>() != null && (grid[@in].gameObject.GetComponent<Ship>() != null || grid[@in].gameObject.GetComponent<PlayerShip>() != null || grid[@in].gameObject.GetComponent<PirateShip>() != null || grid[@in].gameObject.GetComponent<Warship>() != null))
                {
                    DestroyAndCheck(grid[@in].gameObject);
                    Void newVoid = Instantiate(voidPrefab);
                    newVoid.transform.SetParent(this.transform);
                    grid[@in] = newVoid.gameObject;

                    GameObject temp = grid[from];
                    grid[from] = grid[@in];
                    grid[@in] = temp;
                    isMove = true;
                }
                else if (grid[from].gameObject.GetComponent<Warship>() != null && (grid[@in].gameObject.GetComponent<PirateShip>() != null))
                {
                    DestroyAndCheck(grid[@in].gameObject);
                    Void newVoid = Instantiate(voidPrefab);
                    newVoid.transform.SetParent(this.transform);
                    grid[@in] = newVoid.gameObject;

                    GameObject temp = grid[from];
                    grid[from] = grid[@in];
                    grid[@in] = temp;
                    isMove = true;
                }
                else
                {
                    GameObject temp = grid[from];
                    grid[from] = grid[@in];
                    grid[@in] = temp;
                    isMove = true;
                }
            }
        }
    }

    private void SetMoveFalse()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].gameObject.GetComponent<Move>() == true)
            {
                grid[i].gameObject.GetComponent<Move>().isMove = false;
            }
        }
    }

    private void Update()
    {
        if (menu.GetGameInformationStatus() == true)
        {
            UpdateInformation();
        }
        if (menu.GetGameInformationStatus() == true)
        {
            if (move == 0)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    move = 1;
                    isMove = true;
                }

                else if (Input.GetKeyDown(KeyCode.A))
                {
                    move = 2;
                    isMove = true;
                }

                else if (Input.GetKeyDown(KeyCode.W))
                {
                    move = 3;
                    isMove = true;
                }

                else if (Input.GetKeyDown(KeyCode.S))
                {
                    move = 4;
                    isMove = true;
                }

                for (int i = 0; i < grid.Length; i++)
                {
                    if (grid[i].GetComponent<Move>() != null)
                    {
                        if (move == 1)
                        {
                            grid[i].transform.rotation = Quaternion.Euler(0, 90, 0);
                        }
                        if (move == 2)
                        {
                            grid[i].transform.rotation = Quaternion.Euler(0, 270, 0);
                        }
                        if (move == 3)
                        {
                            grid[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                        if (move == 4)
                        {
                            grid[i].transform.rotation = Quaternion.Euler(0, 180, 0);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menu.GetGameInformationStatus() == true)
                {
                    menu.OpenGameMenu();
                }
            }
        }

        if (isMove == true && move == 1)
        {
            for (int i = m_LevelRule.sizeX - 2; i >= 0; i--)
            {
                for (int j = 0; j < m_LevelRule.sizeY; j++)
                {
                    CheckMove(i + j * m_LevelRule.sizeY, i + 1 + j * m_LevelRule.sizeY);
                }
            }
        }
        else if (isMove == true && move == 2)
        {
            for (int i = 1; i < m_LevelRule.sizeX; i++)
            {
                for (int j = 0; j < m_LevelRule.sizeY; j++)
                {
                    CheckMove(i + j * m_LevelRule.sizeY, i - 1 + j * m_LevelRule.sizeY);
                }
            }
        }
        else if (isMove == true && move == 3)
        {
            for (int i = 0; i < m_LevelRule.sizeX; i++)
            {
                for (int j = 1; j < m_LevelRule.sizeY; j++)
                {
                    CheckMove(i + j * m_LevelRule.sizeY, i + (j - 1) * m_LevelRule.sizeY);
                }
            }
        }
        else if (isMove == true && move == 4)
        {
            for (int i = 0; i < m_LevelRule.sizeX; i++)
            {
                for (int j = m_LevelRule.sizeY - 2; j >= 0; j--)
                {
                    CheckMove(i + j * m_LevelRule.sizeY, i + (j + 1) * m_LevelRule.sizeY);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        isMove = true;
        if (move == 1)
        {

            if (shiftX < 2)
            {
                shiftX += Time.fixedDeltaTime * spead;
            }
            if (shiftX >= 2)
            {
                isMove = false;
                shiftX = 0;
                //move = 0;

                for (int i = m_LevelRule.sizeX - 2; i >= 0; i--)
                {
                    for (int j = 0; j < m_LevelRule.sizeY; j++)
                    {
                        SwapMove(i + j * m_LevelRule.sizeY, i + 1 + j * m_LevelRule.sizeY);
                    }
                }
                SetMoveFalse();
            }
        }

        if (move == 2)
        {

            if (shiftX > -2)
            {
                shiftX -= Time.fixedDeltaTime * spead;
            }
            if (shiftX <= -2)
            {
                isMove = false;
                shiftX = 0;
                //move = 0;

                for (int i = 1; i < m_LevelRule.sizeX; i++)
                {
                    for (int j = 0; j < m_LevelRule.sizeY; j++)
                    {
                        SwapMove(i + j * m_LevelRule.sizeY, i - 1 + j * m_LevelRule.sizeY);
                    }
                }
                SetMoveFalse();
            }
        }
        if (move == 3)
        {
            if (shiftY < 2)
            {
                shiftY += Time.fixedDeltaTime * spead;
            }
            if (shiftY >= 2)
            {
                isMove = false;
                shiftY = 0;
                //move = 0;

                for (int i = 0; i < m_LevelRule.sizeX; i++)
                {
                    for (int j = 1; j < m_LevelRule.sizeY; j++)
                    {
                        SwapMove(i + j * m_LevelRule.sizeY, i + (j - 1) * m_LevelRule.sizeY);
                    }
                }
                SetMoveFalse();
            }
        }

        if (move == 4)
        {

            if (shiftY > -2)
            {
                shiftY -= Time.fixedDeltaTime * spead;
            }
            if (shiftY <= -2)
            {
                isMove = false;
                shiftY = 0;
                //move = 0;

                for (int i = 0; i < m_LevelRule.sizeX; i++)
                {
                    for (int j = m_LevelRule.sizeY - 2; j >= 0; j--)
                    {
                        SwapMove(i + j * m_LevelRule.sizeY, i + (j + 1) * m_LevelRule.sizeY);
                    }
                }
                SetMoveFalse();
            }
        }

        if (isMove == false)
        {
            move = 0;
            round++;
        }

        for (int i = 0; i < m_LevelRule.sizeX; i++)
        {
            for (int j = 0; j < m_LevelRule.sizeY; j++)
            {
                if (grid[i + j * m_LevelRule.sizeY] != null)
                {
                    if (grid[i + j * m_LevelRule.sizeY].gameObject.GetComponent<Move>() != null)
                    {
                        if (grid[i + j * m_LevelRule.sizeY].gameObject.GetComponent<Move>().isMove == true)
                        {
                            grid[i + j * m_LevelRule.sizeY].transform.position = new Vector3(i * 2 + shiftX, 0, -j * 2 + shiftY);
                        }
                        else
                        {
                            grid[i + j * m_LevelRule.sizeY].transform.position = new Vector3(i * 2, 0, -j * 2);
                        }
                    }
                    else
                    {
                        grid[i + j * m_LevelRule.sizeY].transform.position = new Vector3(i * 2, 0, -j * 2);
                    }
                }
            }
        }

        for (int i = 0; i < m_LevelRule.sizeX; i++)
        {
            for (int j = 0; j < m_LevelRule.sizeY; j++)
            {
                gridCells[i + j * m_LevelRule.sizeY].transform.position = new Vector3(i * 2, 0.01f, -j * 2);
            }
        }

    }
}

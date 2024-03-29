using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    private Board board;
    private Cell[,] state;
    private bool gameover;
    public int cameraPadding = 100;
    private int playerCount = -1;
    private int currentTeam = -1;
    // private Tilemap tilemap;

    public int playerScore = 0;
    public TextMeshProUGUI scoreUI;

    private void OnValidate()
    {
        MainMenu.mineCount = Mathf.Clamp(MainMenu.mineCount, 0, MainMenu.width * MainMenu.height);
    }

    private void Awake()
    {

        //isClientTurn = true;

        Application.targetFrameRate = 60;

        board = GetComponentInChildren<Board>();

        RegisterEvents();

    }

    public void Start()
    {
        Debug.Log("Start() method called");
        NewGame();
    }

    private void NewGame()
    {
        state = new Cell[MainMenu.width, MainMenu.height];
        gameover = false;

        Debug.Log("NewGame() method called");

        GenerateCells();
        GenerateMines();
        GenerateNumbers();

        Camera.main.transform.position = new Vector3(MainMenu.width / 2f, MainMenu.height / 2f, -10f);
        Camera.main.orthographicSize = Mathf.Max(MainMenu.width, MainMenu.height) * 30f + cameraPadding;

        board.tilemap.transform.position = new Vector3(0 - (MainMenu.width / 2f) * 50, 0 - (MainMenu.height / 2f) * 50);
        board.Draw(state);
    }

    private void GenerateCells()
    {
        for (int x = 0; x < MainMenu.width; x++)
        {
            for (int y = 0; y < MainMenu.height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x, y] = cell;
            }
        }
    }

    private void GenerateMines()
    {
        for (int i = 0; i < MainMenu.mineCount; i++)
        {
            int x = Random.Range(0, MainMenu.width);
            int y = Random.Range(0, MainMenu.height);

            while (state[x, y].type == Cell.Type.Mine)
            {
                x++;

                if (x >= MainMenu.width)
                {
                    x = 0;
                    y++;

                    if (y >= MainMenu.height) {
                        y = 0;
                    }
                }
            }

            state[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumbers()
    {
        for (int x = 0; x < MainMenu.width; x++)
        {
            for (int y = 0; y < MainMenu.height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine) {
                    continue;
                }

                cell.number = CountMines(x, y);

                if (cell.number > 0) {
                    cell.type = Cell.Type.Number;
                }

                state[x, y] = cell;
            }
        }
    }

    private int CountMines(int cellX, int cellY)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0) {
                    continue;
                }

                int x = cellX + adjacentX;
                int y = cellY + adjacentY;

                if (GetCell(x, y).type == Cell.Type.Mine) {
                    count++;
                }
            }
        }

        return count;
    }

    private void Update()
    {

        scoreUI.text = playerScore.ToString();
        

        if (Input.GetKeyDown(KeyCode.R)) {
            NewGame();
        }
        else if (!gameover)
        {
            if (Input.GetMouseButtonDown(1)) {
                Flag();
            } else if (Input.GetMouseButtonDown(0)) {
                Reveal();
            }
        }
    }

    private void Flag()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        // Cannot flag if already revealed
        if (cell.type == Cell.Type.Invalid || cell.revealed) {
            return;
        }

        cell.flagged = !cell.flagged;
        AudioManager.instance.Play("FlagNoise");
        state[cellPosition.x, cellPosition.y] = cell;
        board.Draw(state);
    }

    private void Reveal()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        // Cannot reveal if already revealed or while flagged
        if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged) {
            return;
        }

        switch (cell.type)
        {
            case Cell.Type.Mine:
                Explode(cell);
                playerScore -= 3;
                //GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>().SetText("Score: " + playerScore.ToString());
                AudioManager.instance.Play("GameOver");
                
                break;

            case Cell.Type.Empty:
                Flood(cell);
                playerScore += 5;
                //GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>().SetText("Score: " + playerScore.ToString());
                AudioManager.instance.Play("FloodNoise");
                Debug.Log("drugs!");
                CheckWinCondition();
                break;

            default:
                cell.revealed = true;
                state[cellPosition.x, cellPosition.y] = cell;
                playerScore++;
                //GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>().SetText("Score: " + playerScore.ToString());
                AudioManager.instance.Play("PointNoise");
                CheckWinCondition();
                break;
        }

        board.Draw(state);
    }

    private void Flood(Cell cell)
    {
        // Recursive exit conditions
        if (cell.revealed) return;
        if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid) return;

        // Reveal the cell
        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        // Keep flooding if the cell is empty, otherwise stop at numbers
        if (cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
        }
    }

    private void Explode(Cell cell)
    {
        Debug.Log("Game Over!");
        gameover = true;

        // Set the mine as exploded
        cell.exploded = true;
        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        // Reveal all other mines
        for (int x = 0; x < MainMenu.width; x++)
        {
            for (int y = 0; y < MainMenu.height; y++)
            {
                cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x, y] = cell;
                }
            }
        }
    }

    private void CheckWinCondition()
    {
        for (int x = 0; x < MainMenu.width; x++)
        {
            for (int y = 0; y < MainMenu.height; y++)
            {
                Cell cell = state[x, y];

                // All non-mine cells must be revealed to have won
                if (cell.type != Cell.Type.Mine && !cell.revealed) {
                    return; // no win
                }
            }
        }

        Debug.Log("Winner!");
        gameover = true;

        // Flag all the mines
        for (int x = 0; x < MainMenu.width; x++)
        {
            for (int y = 0; y < MainMenu.height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.flagged = true;
                    state[x, y] = cell;
                }
            }
        }
    }

    private Cell GetCell(int x, int y)
    {
        if (IsValid(x, y)) {
            return state[x, y];
        } else {
            return new Cell();
        }
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < MainMenu.width && y >= 0 && y < MainMenu.height;
    }



    private void RegisterEvents()
    {
        NetUtility.S_WELCOME += OnWelcomeServer;

        NetUtility.C_WELCOME-= OnWelcomeClient;

        NetUtility.C_START_GAME += OnStartGameClient;

    }

    private void UnRegisterEvents()
    {

    }

    private void OnWelcomeServer(NetMessage msg, NetworkConnection cnn) {
    
        NetWelcome nw = msg as NetWelcome;

        nw.AssignedTeam = ++playerCount;

        Server.Instance.SendToClient(cnn, nw);

        if(playerCount== 1) {

            Server.Instance.Broadcast(new NetStartGame());

        }
    }

    private void OnWelcomeClient(NetMessage msg)
    {

        NetWelcome nw = msg as NetWelcome;

        currentTeam = nw.AssignedTeam;

        Debug.Log($"Team is {nw.AssignedTeam}");

    }

    private void OnStartGameClient(NetMessage Obj) {


    
    }




}

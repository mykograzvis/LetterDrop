using UnityEngine;

//objektas kaladeles
public class Piece : MonoBehaviour
{
    public Board board { get; private set; }
    public TileData data { get; private set; }
    public Vector3Int position { get; private set; }
    public Vector3Int[] cells { get; private set; }

    public float stepDelay;
    public float lockDelay;

    private float stepTime;
    private float lockTime;
    private float speedTime;

    public void Initialize(Board board, Vector3Int position, TileData data)
    {
        this.board = board;
        this.data = data;
        this.position = position;
        this.stepTime = Time.time + stepDelay;
        this.lockTime = 0f;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    public void SpeedUp()
    {
        this.stepDelay = this.stepDelay*0.97f;
        this.lockDelay = this.lockDelay*0.97f;
        Debug.Log("speedUp: " + stepDelay);
    }

    public void SpeedUpLVL()
    {
        this.stepDelay = this.stepDelay*0.9f;
        this.lockDelay = this.lockDelay*0.9f;
        Debug.Log("speedUp: " + stepDelay);
    }

    //paupdatinam kordinates positiono
    private void Update()
    {
        this.board.Clear(this);
        if(this.speedTime == 0)
        {
            this.speedTime = Time.time + 30f;
        }
        else if(this.speedTime <= Time.time)
        {
            this.speedTime = Time.time + 30f;
            this.stepDelay = this.stepDelay*0.9f;
            this.lockDelay = this.lockDelay*0.9f;
            Debug.Log("speedUp: " + stepDelay);
        }

        this.lockTime += Time.deltaTime;
        //judejimas i sonus
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right);
        }

        //judejimas zemyn
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down);
        }

        //iskart slamina zemyn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if (Time.time >= this.stepTime)
        {   
            Step();
        }

        this.board.Set(this);
    }

    private void HardDrop()
    {
        while (Move(Vector2Int.down))
        {
            continue;
        }

        Lock();
    }

    private void Step()
    {
        this.stepTime = Time.time + this.stepDelay;

        Move(Vector2Int.down);

        if (this.lockTime >= this.lockDelay)
        {
            Lock();
        }
    }

    private void Lock()
    {
        this.board.Set(this);
        this.board.SpawnPiece();
    }

    //funkcija pajudint kur update naudoja ^
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        //patikrina ar boarde gali ten judeti
        bool validPosition = this.board.IsValidPosition(this, newPosition);

        //
        if (validPosition)
        {
            this.position = newPosition;
            this.lockTime = 0f;
        }

        return validPosition;
    }
}

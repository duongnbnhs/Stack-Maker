using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Direct
    {
        Forward, Backward, Left, Right, None
    }
    Vector3 mouseDown, mouseUp, targetPoint;
    bool isMoving;
    bool isControl;
    public float speed;
    public LayerMask brickLayer;
    [SerializeField] Transform playerBrickPrefab;
    [SerializeField] Transform brickHold;
    [SerializeField] Transform player;
    List<Transform> playerbrickList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        //OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.IsState(GameState.Play))
        {
            if (!isMoving)
            {
                if (Input.GetMouseButtonDown(0) && !isControl)
                //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isControl = true;
                    mouseDown = Input.mousePosition;
                }
                if (Input.GetMouseButtonUp(0) && isControl)
                //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    isControl = false;
                    mouseUp = Input.mousePosition;
                    Direct dir = CheckDirect(mouseDown, mouseUp);
                    if (dir != Direct.None)
                    {
                        targetPoint = GetLastPoint(dir);
                        isMoving = true;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
                {
                    isMoving = false;
                }
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime * speed);
            }
        }
        

    }
    public void OnInit()
    {
        isMoving = false;
        isControl = false;
        ClearBrick();
        player.localPosition = Vector3.zero;
    }
    Vector3 GetLastPoint(Direct direct)
    {
        Vector3 lastPoint;
        Vector3 directVector = transform.position;
        if (direct == Direct.Forward) directVector = Vector3.forward;
        if (direct == Direct.Backward) directVector = Vector3.back;
        if (direct == Direct.Right) directVector = Vector3.right;
        if (direct == Direct.Left) directVector = Vector3.left;
        if (direct == Direct.None) directVector = Vector3.zero;
        RaycastHit hit;
        int i = 1;
        while (true)
        {
            if (Physics.Raycast(transform.position + directVector * i + Vector3.up * 2, Vector3.down, out hit, 10f, brickLayer))
            {
                i++;
            }
            else
            {
                lastPoint = transform.position + directVector * (i - 1);
                break;
            }
        }
        return lastPoint;
    }
    Direct CheckDirect(Vector3 down, Vector3 up)
    {
        Direct direct = Direct.None;
        float deltaX = up.x - down.x;
        float deltaY = up.y - down.y;
        if (Vector3.Distance(up, down) > 100)
        {
            if (Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                if (deltaY > 0)
                {
                    direct = Direct.Forward;
                }
                else
                {
                    direct = Direct.Backward;
                }
            }
            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if (deltaX > 0)
                {
                    direct = Direct.Right;
                }
                else
                {
                    direct = Direct.Left;
                }
            }
        }
        return direct;
    }
    public void AddBrick()
    {
        int index = playerbrickList.Count;
        Transform playerBrick = Instantiate(playerBrickPrefab, brickHold);
        playerBrick.localPosition = Vector3.down + index * 0.25f * Vector3.up;
        playerbrickList.Add(playerBrick);
        player.localPosition = player.localPosition + Vector3.up * 0.25f;
    }
    public void RemoveBrick()
    {
        int index = playerbrickList.Count - 1;
        if (index >= 0)
        {
            Transform playerBrick = playerbrickList[index];
            playerbrickList.RemoveAt(index);
            Destroy(playerBrick.gameObject);
            player.localPosition = player.localPosition - Vector3.up * 0.25f;
        }
    }
    public void ClearBrick()
    {
        foreach(Transform playerBrick in playerbrickList)
        {
            Destroy(playerBrick.gameObject);
        }
        playerbrickList.Clear();
    }
}

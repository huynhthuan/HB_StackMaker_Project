using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    public MapController mapController;

    [SerializeField]
    public Camera mainCamera;

    [SerializeField]
    public Camera subCamera;

    protected int playerLevel;

    private Vector3 touchStartPoint,
        touchEndPoint;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        // Reset camera
        ResetCamera();

        // Reset touch point
        resetTouchPoint();

        // Get current player level
        GetDataPLayer();

        //Loading map by player level
        mapController.InitMap(playerLevel);
        // Set player to start point of map
        player.SetPosition(mapController.GetPointMap("startPoint"));
    }

    private void OnDespawn() { }

    private bool IsTouchValid()
    {
        return Vector3.Distance(touchStartPoint, touchEndPoint) >= 10f
            && touchEndPoint != Vector3.zero
            && touchEndPoint != Vector3.zero;
    }

    public Vector3 GetVectorNormalized(Vector3 startPoint, Vector3 endPoint)
    {
        return (startPoint - endPoint).normalized;
    }

    private Direction GetDirectToMove()
    {
        Direction dir;
        // Normalized vector dir
        Vector3 dirVectorNormalized = GetVectorNormalized(touchEndPoint, touchStartPoint);

        if (Mathf.Abs(dirVectorNormalized.x) > Mathf.Abs(dirVectorNormalized.y))
        {
            dir = dirVectorNormalized.x > 0 ? Direction.Right : Direction.Left;
        }
        else
        {
            dir = dirVectorNormalized.y > 0 ? Direction.Forward : Direction.Back;
        }

        return dir;
    }

    public void GetDataPLayer()
    {
        // Get current player level from playerPrefs
        PlayerPrefs.SetInt("playerLevel", 2);
        playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
    }

    private void Update()
    {
        // If user touch down
        if (Input.GetMouseButtonDown(0))
        {
            // If player not moving
            if (!player.isMoving)
            {
                // Get point touch down
                touchStartPoint = Input.mousePosition;
            }
            // Debug.Log("touchStartPoint" + touchStartPoint);
        }

        // If user touch up
        if (Input.GetMouseButtonUp(0))
        {
            // If player not moving
            if (!player.isMoving)
            {
                // Get point touch up
                touchEndPoint = Input.mousePosition;
            }
            // Debug.Log("touchEndPoint" + touchEndPoint);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Debug.Log("Player is can move " + player.isMoving);

        // If touch invalid
        if (!IsTouchValid())
        {
            return;
        }

        // Move player by direction user swipe
        player.MoveByDirect(GetDirectToMove());
    }

    public void resetTouchPoint()
    {
        touchStartPoint = Vector3.zero;
        touchEndPoint = Vector3.zero;
    }

    public void SwitchSubCamera()
    {
        mainCamera.gameObject.SetActive(false);
        subCamera.gameObject.SetActive(true);
    }

    public void ResetCamera()
    {
        mainCamera.gameObject.SetActive(true);
        subCamera.gameObject.SetActive(false);
    }
}

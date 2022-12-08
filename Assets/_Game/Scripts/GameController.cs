using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    internal Player player;

    [SerializeField]
    public MapController mapController;

    [SerializeField]
    public Camera mainCamera;

    [SerializeField]
    public UiController uiController;

    [SerializeField]
    public Camera subCamera;

    [SerializeField]
    public int maxLevel;

    public int playerLevel;

    private Vector3 touchStartPoint,
        touchEndPoint;

    internal bool isLevelPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt("playerLevel", 1);
        OnInit();
    }

    private void OnInit()
    {
        uiController.HideAllUI();

        uiController.ShowUI(UILayer.LOADING);

        // Reset camera
        ResetCamera();

        // Reset touch point
        resetTouchPoint();

        // Get current player level
        GetDataPLayer();

        //Loading map by player level
        mapController.InitMap(playerLevel);
        // Set player to start point of map
        player.OnInit(mapController.GetPointMap("startPoint"));
        isLevelPlaying = true;

        uiController.HideUI(UILayer.LOADING);
    }

    private void OnDespawn() { }

    private bool IsTouchValid()
    {
        return Vector3.Distance(touchStartPoint, touchEndPoint) >= 10f
            && touchEndPoint != Vector3.zero
            && touchEndPoint != Vector3.zero
            && !player.collisionSensorFoot.hasStandEndLevel
            && isLevelPlaying;
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
        playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
        Debug.Log("Current Level: " + playerLevel);
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

        if (player.isHasStandOpenBoxPosition)
        {
            return;
        }

        Debug.Log("isHasStandOpenBoxPosition " + player.isHasStandOpenBoxPosition);

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

    public void OnEndLevel()
    {
        uiController.ShowUI(UILayer.END_LEVEL);
    }

    public void OnNextLevel()
    {
        playerLevel++;

        if (playerLevel > maxLevel)
        {
            uiController.ShowUI(UILayer.NO_NEW_LEVEL);
            return;
        }

        Debug.Log("playerLevel " + playerLevel);
        PlayerPrefs.SetInt("playerLevel", playerLevel);
        OnInit();
    }

    public void OnRestartLevel()
    {
        PlayerPrefs.SetInt("playerLevel", playerLevel);
        OnInit();
    }

    public void OnReplay()
    {
        PlayerPrefs.SetInt("playerLevel", 1);
        OnInit();
    }
}

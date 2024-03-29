using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections;
using UnityEngine.Events;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
 
    private const float LEFT_BOUNDARY = -1.6f;
    private const float RIGHT_BOUNDARY = 1.6f;
    private const float Z_POSITION = 0;

    public Transform fruitStartPosition;
    public Transform leftBoundary;
    public Transform rightBoundary;
    private Camera mainCamera;
    public bool isFruitExist = false;
    private Coroutine releaseCoroutine;
    public UnityEvent releaseFruitEvent;
    private float delay = 0.5f;

    private void Awake()
    {
        mainCamera = Camera.main;
        isFruitExist = false;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (!EndLine.isPaused)
            {
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    HandleTouchRelease();
                }

                UpdatePosition(touch);
            }
        }
    }

    private void HandleTouchRelease()
    {
        releaseFruitEvent.Invoke();
        releaseFruitEvent.RemoveAllListeners();

        if (releaseCoroutine != null)
        {
            StopCoroutine(releaseCoroutine);
        }

        releaseCoroutine = StartCoroutine(ReleaseCoroutine());
    }

    private void UpdatePosition(Touch touch)
    {
        Vector3 screenPos = GetScreenPosition(touch);
        Vector3 worldPos = GetWorldPosition(screenPos);
        worldPos = AdjustWorldPosition(worldPos);
        gameObject.transform.position = worldPos;
    }

    private Vector3 GetScreenPosition(Touch touch)
    {
        return new Vector3(touch.screenPosition.x, touch.screenPosition.y, mainCamera.nearClipPlane);
    }

    private Vector3 GetWorldPosition(Vector3 screenPos)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPos);
        worldPos.z = Z_POSITION;
        worldPos.y = fruitStartPosition.position.y;
        return worldPos;
    }

    private Vector3 AdjustWorldPosition(Vector3 worldPos)
    {
        if (worldPos.x <= leftBoundary.position.x)
        {
            worldPos.x = LEFT_BOUNDARY;
        }
        if (worldPos.x >= rightBoundary.position.x)
        {
            worldPos.x = RIGHT_BOUNDARY;
        }
        return worldPos;
    }

    private IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(delay);

        isFruitExist = false;
        releaseCoroutine = null;
    }
}
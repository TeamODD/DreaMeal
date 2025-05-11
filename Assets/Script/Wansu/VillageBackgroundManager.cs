using System.Collections;
using UnityEngine;

public class VillageBackgroundManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject village;
    public GameObject background;
    public GameObject map;
    public GameObject back;
    private bool isZoomedIn = false;
    private Vector3 originalPosition;
    private float originalSize;
    public float zoomSize = 2f;
    public float zoomDuration = 0.5f;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    public SpriteRenderer backgroundRenderer;
    void Start()
    {
        backgroundRenderer = GetComponent<SpriteRenderer>();
        Bounds bounds = backgroundRenderer.bounds;
        mapMinX = bounds.min.x;
        mapMaxX = bounds.max.x;
        mapMinY = bounds.min.y;
        mapMaxY = bounds.max.y;
        originalPosition = mainCamera.transform.position;
        originalSize = mainCamera.orthographicSize;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isZoomedIn)
        {
            village.SetActive(false);
            map.SetActive(true);
        }
    }
    private Vector3 ClampCameraPosition(Vector3 targetPos, float camSize)
    {
        float camHalfHeight = camSize;
        float camHalfWidth = camHalfHeight * mainCamera.aspect;

        float clampedX = Mathf.Clamp(targetPos.x, mapMinX + camHalfWidth, mapMaxX - camHalfWidth);
        float clampedY = Mathf.Clamp(targetPos.y, mapMinY + camHalfHeight, mapMaxY - camHalfHeight);

        return new Vector3(clampedX, clampedY, targetPos.z);
    }
    public void ZoomToTarget(Transform target, System.Action onComplete = null)
    {
        back.SetActive(false);
        originalPosition = mainCamera.transform.position;
        originalSize = mainCamera.orthographicSize;
        isZoomedIn = true;

        Vector3 clampedPos = ClampCameraPosition(target.position, zoomSize);
        StartCoroutine(ZoomCamera(clampedPos, zoomSize, onComplete));
    }
    private IEnumerator ZoomCamera(Vector3 targetPosition, float targetSize, System.Action onComplete = null)
    {
        Vector3 startPos = mainCamera.transform.position;
        float startSize = mainCamera.orthographicSize;
        float t = 0f;
        targetPosition.z = startPos.z;
        while (t < 1f)
        {
            t += Time.deltaTime / zoomDuration;
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            yield return null;
        }
        onComplete?.Invoke();
    }
    public void ZoomOutCamera()
    {
        StartCoroutine(ZoomCamera(originalPosition, originalSize, () => 
        { 
            isZoomedIn = false;
            back.SetActive(true);
        }));
    }
    public void ResetCamera()
    {
        back.SetActive(true);
        mainCamera.transform.position = originalPosition;
        mainCamera.orthographicSize = originalSize;
        isZoomedIn = false;
    }
}
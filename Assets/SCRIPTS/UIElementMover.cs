using UnityEngine;

public class UIElementMover : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField, Min(0)] float delay = .5f;
    [SerializeField] Vector2 moveSpeed;
    float timer;
    bool isMoving = false;
    Vector2 ogPos;
    
    void Awake()
    {
        ogPos = rectTransform.anchoredPosition;
    }
    void Update()
    {
        if (!isMoving) return;
        timer += Time.deltaTime;
        if (timer > delay)
            rectTransform.anchoredPosition += moveSpeed * Time.deltaTime;
    }
    void OnDisable()
    {
        isMoving = false;
        rectTransform.anchoredPosition = ogPos;
    }
    public void StartMoving() => isMoving = true;
}
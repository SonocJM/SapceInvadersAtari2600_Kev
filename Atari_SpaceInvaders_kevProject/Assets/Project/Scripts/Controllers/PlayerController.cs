using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveInput;
    private Vector3 currentPosition;
    [SerializeField] private float speed = 5f;

    [Header("Limit")]
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    void Update()
    {
        Movement(); 
    }

    public void Movement()
    {
        moveInput = Input.GetAxis("Horizontal");

        currentPosition = transform.position;
        currentPosition.x += moveInput * speed * Time.deltaTime;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        transform.position = currentPosition;
    }
}


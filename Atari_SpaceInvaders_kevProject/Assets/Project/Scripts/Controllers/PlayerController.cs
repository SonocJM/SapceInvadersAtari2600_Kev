using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float minX = -5f;
    [SerializeField] private float maxX = 5f;

    [Header("Shoot")]
    [SerializeField] private ObjectPoolManager bulletPool;
    [SerializeField] private Vector3 bulletOffset = new Vector3(0, 0.5f, 0);

    private float moveInput;
    private Vector3 currentPosition;

    void Update()
    {
        Movement();
        Shooting();
    }

    private void Movement()
    {
        moveInput = Input.GetAxis("Horizontal");

        currentPosition = transform.position;
        currentPosition.x += moveInput * speed * Time.deltaTime;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);

        transform.position = currentPosition;
    }

    private void Shooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletPool != null)
            {
                GameObject bullet = bulletPool.AskForObject(transform.position + bulletOffset);
                if (bullet != null)
                    bullet.SetActive(true);
            }
        }
    }
}



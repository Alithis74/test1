using UnityEngine;

public class LootAnimation : MonoBehaviour
{
    [Header("Настройки парения")]
    public float amplitude = 0.2f; 
    public float frequency = 2f;   

    [Header("Настройки вращения")]
    public bool lookAtPlayer = true;
    public float rotationSpeed = 5f;

    private Vector3 startLocalPosition;
    private Transform playerTransform;

    void Start()
    {
        // Используем LocalPosition, чтобы не зависеть от мировых координат
        startLocalPosition = transform.localPosition;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    void Update()
    {
        // Парение относительно начальной точки
        float newY = startLocalPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);

        // Поворот к игроку
        if (lookAtPlayer && playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0; 
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
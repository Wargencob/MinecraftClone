using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Настройки движения")]
    public float speed = 5f;           
    public float jumpHeight = 2f;      
    public float gravity = -9.81f;     
    public float coyoteTime = 0.2f;

    [Header("Настройки камеры")]
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private float rotationX = 0f;
    private float lastGroundedTime;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        // Обновляем состояние касания земли
        if (controller.isGrounded)
        {
            lastGroundedTime = Time.time;
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        // Получаем ввод с клавиатуры для передвижения
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Прыжок с учетом буффера времени
        if (Input.GetButton("Jump") && (controller.isGrounded || (Time.time - lastGroundedTime <= coyoteTime)))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Применяем гравитацию
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Обработка вращения камеры
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Вращаем игрока вокруг вертикальной оси
        transform.Rotate(Vector3.up * mouseX);

        // Наклоняем камеру вверх/вниз
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}

using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    [SerializeField] InputAction escape;

    [SerializeField] GameObject[] lasers;

    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float xRange = 20f;
    [SerializeField] float yRange = 15f;

    float xThrow;
    float yThrow;
    float gameStartTime;

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
        escape.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
        escape.Disable();
    }
    void Start()
    {
        gameStartTime = Time.time;
    }

    void Update()
    {
        ProcessFiring();
        ProcessTranslation();
        ProcessRotation();
        ProcessEscaping();
    }
    void ProcessEscaping()
    {
        if (Time.time > 44 + gameStartTime) Application.Quit();
        if (escape.ReadValue<float>() > 0.5) Application.Quit();
    }
    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5) ActivateFiring(true);
        else ActivateFiring(false);
    }

    void ActivateFiring(bool IsFiringActivated)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = IsFiringActivated;
        }
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPosition = transform.localPosition.x + xOffset;
        float clampedXPosition = Mathf.Clamp(rawXPosition, -xRange, xRange);

        yThrow = movement.ReadValue<Vector2>().y;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPosition = transform.localPosition.y + yOffset;
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yRange, yRange);

        float zLocal = transform.localPosition.z;

        transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, zLocal);
    }
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}

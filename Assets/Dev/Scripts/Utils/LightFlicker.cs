using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Header("Intensity Settings")]
    [SerializeField] private float m_IntensityVariation = 0.4f;
    [SerializeField] private float m_FlickerSpeed = 5f;

    [Header("Movement Settings")]
    [SerializeField] private float m_MovementAmount = 0.02f;
    [SerializeField] private float m_MovementSpeed = 3f;

    private Light m_PointLight;
    private Vector3 m_InitialPosition;
    private float m_InitialIntensity;

    // ========== Unity Functions ===========

    void Awake()
    {
        m_PointLight = GetComponent<Light>();
        m_InitialPosition = transform.localPosition;
        m_InitialIntensity = m_PointLight.intensity;
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(0f, Time.time * m_FlickerSpeed);

        m_PointLight.intensity = m_InitialIntensity + (noise - 0.5f) * 2f * m_IntensityVariation;

        float movementX = (Mathf.PerlinNoise(1f, Time.time * m_MovementSpeed) - 0.5f) * m_MovementAmount;
        float movementY = (Mathf.PerlinNoise(2f, Time.time * m_MovementSpeed) - 0.5f) * m_MovementAmount;

        transform.localPosition = m_InitialPosition + new Vector3(movementX, movementY, 0f);
    }
}

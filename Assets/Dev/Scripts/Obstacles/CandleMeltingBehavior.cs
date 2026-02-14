using UnityEngine;

public class CandleMeltingBehavior : MonoBehaviour
{
    [SerializeField] private Renderer _CandleRenderer;
    [SerializeField] private GameObject _CandleObject;

    private float _MaxCandleHealth = 100f;
    private float _CurrentCandleHealth = 100f;
    public float _CandleHealthLossPerSecond = 10f;

    private float _CandleHeight = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Change CurrentHealth
            float healthBefore = _CurrentCandleHealth;
            _CurrentCandleHealth = Mathf.Clamp(_CurrentCandleHealth - _CandleHealthLossPerSecond * Time.deltaTime, 0f, _MaxCandleHealth);

            float cutHeight = 1 - (_CurrentCandleHealth / _MaxCandleHealth);
            _CandleRenderer.material.SetFloat("_CutHeight", cutHeight);

            float deltaY = (healthBefore - _CurrentCandleHealth) / _MaxCandleHealth * _CandleHeight;
            transform.position -= new Vector3(0, deltaY, 0);


            if(_CurrentCandleHealth == 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        MeshFilter mf = _CandleObject.GetComponent<MeshFilter>();
        Mesh mesh = mf.sharedMesh;
        
        _CandleHeight = mesh.bounds.size.y * _CandleObject.transform.lossyScale.y;
    }

    void Update()
    {
        
    }
}

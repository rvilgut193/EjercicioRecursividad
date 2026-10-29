using UnityEngine;
public class JuegoRecursividad : MonoBehaviour
{

    [SerializeField] private Color[] colores;
    private InputSystem_Actions input;
    void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i <7; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                GameObject nuevoCubo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                nuevoCubo.transform.position = new Vector3(i * 1.1f, j * 1.1f, 0);
                nuevoCubo.GetComponent<Renderer>().material.color = colores[Random.Range(0, 5)];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

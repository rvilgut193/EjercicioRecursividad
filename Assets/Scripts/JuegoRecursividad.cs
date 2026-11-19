using UnityEngine;
using UnityEngine.InputSystem;

public class JuegoRecursividad : MonoBehaviour
{
    [SerializeField] private Color[] coloresCubos;
    [SerializeField, Range(7, 21)] private int lado = 7;
    [SerializeField] private float separacion = 0.05f;
    [SerializeField] private float margenCamara = 1.15f;
    private Camera cam;
    private float ultimoAspect;
    private Cubo[,] cubos;

    void Start()
    {
        if (lado % 2 == 0) lado++;
        cam = Camera.main;
        CrearCuadricula();
        AjustarCamara();
    }

    void Update()
    {
        // Si el jugador cambia el tamaño de la ventana, recolocamos la cámara
        if (cam.aspect != ultimoAspect) AjustarCamara();
        DetectarClic();
    }



    private void CrearCuadricula()
    {
        cubos = new Cubo[lado, lado];

        for (int x = 0; x < lado; x++)
        {
            for (int y = 0; y < lado; y++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.name = $"cubo_{x}_{y}";
                obj.transform.SetParent(transform);
                obj.transform.position = new Vector3(x * (1 + separacion), y * (1 + separacion), 0);

                Cubo cubo = obj.AddComponent<Cubo>();
                cubo.SetPosicion(x, y);

                int indice = Random.Range(0, coloresCubos.Length);
                cubo.SetColor(indice, coloresCubos[indice]);

                cubos[x, y] = cubo;
            }
        }
    }
    private bool EstaDentro(int x, int y)
    {
        return x >= 0 && x < lado && y >= 0 && y < lado;
    }

    private void AjustarCamara()
    {
        float paso = 1 + separacion;
        float tamanoTotal = (lado - 1) * paso + 1;   // lo que mide la cuadrícula de borde a borde
        float centro = (lado - 1) * paso / 2f;       // el punto medio de la cuadrícula

        cam.orthographic = true;
        cam.transform.position = new Vector3(centro, centro, -10f);
        cam.transform.rotation = Quaternion.identity;

        float tamanoPorAlto = tamanoTotal / 2f;
        float tamanoPorAncho = tamanoTotal / (2f * cam.aspect);
        cam.orthographicSize = Mathf.Max(tamanoPorAlto, tamanoPorAncho) * margenCamara;

        ultimoAspect = cam.aspect;
    }

    private void DetectarClic()
    {
        if (Mouse.current == null || !Mouse.current.leftButton.wasPressedThisFrame) return;

        Ray rayo = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(rayo, out RaycastHit hit))
        {
            Cubo cubo = hit.collider.GetComponent<Cubo>();
            if (cubo != null)
            {
                int destruidos = DestruirGrupo(cubo.X, cubo.Y, cubo.IndiceColor);
                Debug.Log($"Cubos destruidos: {destruidos}");
            }
        }
    }

    private int DestruirGrupo(int x, int y, int indiceColor)
    {
        // --- Casos base: aquí la recursión se detiene ---
        if (!EstaDentro(x, y)) return 0;
        Cubo cubo = cubos[x, y];
        if (cubo == null) return 0;
        if (cubo.IndiceColor != indiceColor) return 0;

        // --- Este cubo pertenece al grupo ---
        cubos[x, y] = null;            // 1º marcarlo (evita la recursión infinita)
        Destroy(cubo.gameObject);      // 2º destruirlo (se elimina al final del frame)

        // --- Caso recursivo: preguntar a los 4 vecinos ---
        int total = 1;                 // yo mismo
        total += DestruirGrupo(x + 1, y, indiceColor);
        total += DestruirGrupo(x - 1, y, indiceColor);
        total += DestruirGrupo(x, y + 1, indiceColor);
        total += DestruirGrupo(x, y - 1, indiceColor);
        return total;
    }
}
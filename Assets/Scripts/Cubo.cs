using UnityEngine;

public class Cubo : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public int IndiceColor { get; private set; }

    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void SetPosicion(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetColor(int indice, Color color)
    {
        IndiceColor = indice;
        rend.material.color = color;
    }
}
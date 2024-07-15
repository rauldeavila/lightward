using UnityEngine;

public class InfiniteHorizontalLoop : MonoBehaviour
{
    public float velocidade = 5f; // Defina a velocidade desejada, pode ser uma variável pública.

    void Update()
    {
        float offset = Time.time * velocidade;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
    }
}
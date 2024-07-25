using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ThunderstormListener : MonoBehaviour
{
    private Material defaultMat; // Agora é uma variável privada
    public Material ThunderMat;

    private Renderer rendererComponent; // Será SpriteRenderer ou TilemapRenderer
    private Coroutine materialChangeCoroutine;

    public bool Wiz = false;

    void Awake()
    {
        // Tenta obter um SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            rendererComponent = spriteRenderer;
        }
        else
        {
            // Se não houver SpriteRenderer, tenta obter um TilemapRenderer
            TilemapRenderer tilemapRenderer = GetComponent<TilemapRenderer>();
            if (tilemapRenderer != null)
            {
                rendererComponent = tilemapRenderer;
            }
            else
            {
                Debug.LogError("SpriteRenderer or TilemapRenderer not found on the object. ThunderstormListener won't work without them.");
            }
        }

        if (rendererComponent != null)
        {
            defaultMat = rendererComponent.material; // Obtendo o material padrão do Renderer
        }
    }

    void Start()
    {
        // Inscrever-se para o evento do ThunderstormManager
        if(FindObjectOfType<ThunderstormManager>() != null)
            FindObjectOfType<ThunderstormManager>().OnLightningStrike += HandleLightningStrike;
    }

    void HandleLightningStrike(float thunderDuration)
    {
        if(Wiz) { defaultMat = rendererComponent.material; }
        // Trocar o material do Renderer para ThunderMat
        if (rendererComponent != null)
        {
            rendererComponent.material = ThunderMat;

            // Iniciar a contagem regressiva para voltar ao DefaultMat após o thunderDuration
            if (materialChangeCoroutine != null)
            {
                StopCoroutine(materialChangeCoroutine);
            }
            materialChangeCoroutine = StartCoroutine(ResetMaterialAfterDuration(thunderDuration));
        }
    }

    IEnumerator ResetMaterialAfterDuration(float duration)
    {
        // Aguardar o tempo especificado e, em seguida, restaurar o material padrão
        yield return new WaitForSeconds(duration);

        // Voltar ao material padrão
        if (rendererComponent != null)
        {
            rendererComponent.material = defaultMat;
        }
    }

    // void OnDisable()
    // {
    //     // Desinscrever-se quando o objeto é desativado (importante para evitar vazamentos de memória)
    //     if (FindObjectOfType<ThunderstormManager>() != null)
    //     {
    //         FindObjectOfType<ThunderstormManager>().OnLightningStrike -= HandleLightningStrike;
    //     }
    // }
}

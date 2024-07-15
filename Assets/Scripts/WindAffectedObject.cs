using UnityEngine;

public class WindAffectedObject : MonoBehaviour
{
    public bool Wiz = false;
    private Rigidbody2D rb;
    private ParticleSystem ps;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    private float windStrength = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            velocityModule = ps.velocityOverLifetime;
            velocityModule.enabled = true;
        }
    }

    void Start()
    {
        if (WindController.Instance != null)
        {
            WindController.Instance.OnWindStrengthChanged += UpdateWindEffect;
            UpdateWindEffect(WindController.Instance.WindStrength);
        }
    }

    void OnDestroy()
    {
        if (WindController.Instance != null)
        {
            WindController.Instance.OnWindStrengthChanged -= UpdateWindEffect;
        }
    }

    void Update()
    {
        if (WindController.Instance != null && WindController.Instance.IsWindLerping)
        {
            windStrength = WindController.Instance.WindStrength;
        }
        if (ps != null)
        {
            var x = new ParticleSystem.MinMaxCurve(windStrength);
            velocityModule.x = x;
        }

        if (rb != null)
        {
            if(!Wiz)
            {
                rb.AddForce(new Vector2(windStrength * 2, 0f));
            }
            else
            {
                if(PlayerState.Instance.UnaffectedByWind)
                {
                    // print("Not adding force right now");
                }
                else
                {
                    rb.AddForce(new Vector2(windStrength * 2, 0f));
                }
            }
        }
    }

    private void UpdateWindEffect(float _windStrength)
    {
        windStrength = _windStrength;
        if (ps != null)
        {
            var x = new ParticleSystem.MinMaxCurve(windStrength);
            velocityModule.x = x;
        }
    }
}

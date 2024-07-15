using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour {

    private List<ParticleSystem> particleSystems;

    public bool BearBattle = false;

    public float raycastLength = 10f;
    public float yVariationR1 = 0f;
    public float yVariationR2 = 0f;
    public float xVariationR1 = 0.3f;
    public float xVariationR2 = 0.3f;
    RaycastHit2D raycast1;
    RaycastHit2D raycast2;
    int groundLayerMask;
    Vector2 direction;

    private bool _dropped = false;
    private bool _collided = false;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private WizOrGroundCollision _wizOrGroundCollision;
    private Projectile _projectileScript;

    void Start() {
        groundLayerMask = LayerMask.GetMask("WizHitBox");
        _rb = GetComponentInChildren<Rigidbody2D>();
        _wizOrGroundCollision = GetComponentInChildren<WizOrGroundCollision>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _projectileScript = GetComponentInChildren<Projectile>();
        particleSystems = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());
        if(BearBattle){
            _dropped = true;
            Drop();
        }
    }

    void Update() {
        if(_projectileScript.LastHitByPlayer){
            _rb.constraints = RigidbodyConstraints2D.None;
            this.enabled = false;
        }
        if(!_dropped){
            // direction = PlayerState.Instance.FacingRight ? Vector2.right : Vector2.left;
            direction = Vector2.down;
            raycast1 = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), direction, raycastLength, groundLayerMask);
            raycast2 = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), direction, raycastLength, groundLayerMask);
            if(!BearBattle){
                if(raycast1 || raycast2){
                    _dropped = true;
                    Drop();
                }
            }
        } else {
            if(!_collided){
                if(_wizOrGroundCollision.CollidedWithGround){
                    _wizOrGroundCollision.CollidedWithGround = false;
                    _wizOrGroundCollision.CollidedWithWiz = false;
                    _collided = true;
                    HandleGround();
                } else if(_wizOrGroundCollision.CollidedWithWiz){
                    _wizOrGroundCollision.CollidedWithGround = false;
                    _wizOrGroundCollision.CollidedWithWiz = false;
                    _collided = true;
                    HandleWiz();
                }   
            }
        }
    }

    void HandleGround(){
        foreach (ParticleSystem ps in particleSystems) {
            ps.Play();
        }
        GetComponentInChildren<Play3DSound>().Play();
        _rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        _rb.velocity = Vector2.zero;
        _rb.Sleep();
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        StartCoroutine(ChangeColorOverTime(new Color(0.333f, 0.333f, 0.333f, 1f), 1f));
        // print("collided with ground");
    }

    void HandleWiz(){

        GetComponentInChildren<Play3DSound>().Play();
        _rb.constraints -= RigidbodyConstraints2D.FreezePositionX;
        float horizontalForce = UnityEngine.Random.Range(-1f, 1f) * 5f; // Random force to the left or right
        float verticalForce = 10f; // Fixed force upwards

        _rb.velocity = Vector2.zero;
        _rb.AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);

        GetComponentInChildren<BoxCollider2D>().enabled = false;
        StartCoroutine(ChangeColorOverTime(new Color(0.333f, 0.333f, 0.333f, 1f), 1f));
        // print("collided with Wiz");
        Invoke("DisableSelf", 1f);
    }

    void DisableSelf(){
        this.gameObject.SetActive(false);
    }

    IEnumerator ChangeColorOverTime(Color newColor, float duration) {
        Color oldColor = _spriteRenderer.color;
        for (float t = 0; t < duration; t += Time.deltaTime) {
            _spriteRenderer.color = Color.Lerp(oldColor, newColor, t / duration);
            yield return null;
        }
        _spriteRenderer.color = newColor;
    }

    void Drop(){
        _rb.constraints -= RigidbodyConstraints2D.FreezePositionY;
        _rb.AddForce(Vector2.down, ForceMode2D.Impulse);
        // print("Drop!");
    }

private void OnDrawGizmos() 
{
    direction = Vector2.down;
    RaycastHit2D raycast1Gizmo = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), direction, raycastLength, groundLayerMask);
    RaycastHit2D raycast2Gizmo = Physics2D.Raycast(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), direction, raycastLength, groundLayerMask);
    
    Gizmos.color = Color.white;
    Gizmos.DrawRay(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), direction * raycastLength);
    Gizmos.DrawRay(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), direction * raycastLength);

    if (raycast1Gizmo.collider != null) 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + xVariationR1, transform.position.y + yVariationR1, 0f), raycast1Gizmo.point);
    } 
    if (raycast2Gizmo.collider != null) 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + xVariationR2, transform.position.y + yVariationR2, 0f), raycast2Gizmo.point);
    } 
}













}

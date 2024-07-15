using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkellyGroundToDisable : MonoBehaviour
{

    public GameObject CatacombsBricksExplosion;
    public Vector3 ParticlesPosition;

    public void DestroyGround2()
    {
        Instantiate(CatacombsBricksExplosion, ParticlesPosition, Quaternion.identity);
        FMODUnity.RuntimeManager.PlayOneShot("event:/game/02_graveyard/skely/catacombs_ground_explosion", transform.position);
        Destroy(gameObject);
    }

}

/**********************************************************************
 * Autor: Leandro Dornela Ribeiro
 * Contato: leandrodornela@ice.ufjf.br
 * Data de criação: 12/2018
 * Modificação:
 * ********************************************************************/

 
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class Granade : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public ParticleSystem particle;
    public GameObject explosionCollider;
    public float explosionTime = 5;
    public float disableColliderTime = 1;
    public GameObject graphics;
    public float damage = 9999;

    private float timer = 0;
    private bool inExplosion = false;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    public void Init(Vector3 dir)
    {
        rb.velocity = dir * speed;
        rb.angularVelocity = new Vector3(1,5,7);
    }


    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if(!inExplosion)
        {
            if (timer >= explosionTime)
            {
                inExplosion = true;
                Explode();
            }
        }
        else
        {
            if(timer >= particle.main.duration + explosionTime)
            {
                Destroy(gameObject);
            }

            if(timer >= particle.main.duration + explosionTime - disableColliderTime)
            {
                explosionCollider.SetActive(false);
            }
        }

        timer += Time.deltaTime;
    }


    /// <summary>
    /// 
    /// </summary>
    void Explode()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        particle.Play();
        explosionCollider.SetActive(true);
        graphics.SetActive(false);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("hit player");
            col.gameObject.GetComponentInParent<PlayerStateManager>().ActiveState().TakeDamage(damage);
        }
    }
}

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
public class Shoot : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject particles;


	// Use this for initialization
	void Start ()
    {
		
	}


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public float GetDamage()
    {
        return damage;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="direction"></param>
    public void Init(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponentInParent<PlayerStateManager>().ActiveState().TakeDamage(damage);
            GameObject clone = Instantiate(particles, transform.position, Quaternion.LookRotation(-transform.forward));
            Destroy(gameObject);
        }
        else if(col.gameObject.tag == "Player_saber")
        {
            Vector3 dir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)).normalized * rb.velocity.magnitude;
            if (dir == Vector3.zero) dir = -rb.velocity;
            rb.velocity = dir;
            rb.rotation = Quaternion.LookRotation(rb.velocity.normalized);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }


    /// <summary>
    /// 
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

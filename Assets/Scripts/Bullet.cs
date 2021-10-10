using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        StartCoroutine(DestroyAfterSeconds());
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var dmg = collision.gameObject.GetComponentInParent<IDamageable>();
        print(collision.gameObject);
        if(dmg != null)
        {
            dmg.OnDamage(1);
            print("Damage dealt");
        }
        Destroy(gameObject);
    }
}

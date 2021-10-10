using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState
{
    ATTACK = 0,
    MOVE = 1,
    RECHARGE = 2,
    REVUP = 3
}

public class Enemy : MonoBehaviour, IDamageable
{
    public int health = 5;
    public float maxSpeed = 15f, currentSpeed;
    public AIState state;
    public GameObject bullet;
    public GameObject bulletSpawnPosition;
    public int direction = 1;
    public AudioClip deathSound;
    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        direction = (int)(Random.value * 2) == 1 ? 1 : -1;
        currentSpeed = maxSpeed;
        StartCoroutine(EnemyActions());
    }

    void Move(float elapsed)
    {
        var targetDirection = Vector3.Cross(transform.position - PlayerController.player.transform.position, Vector3.up) * direction;
        var currentDirection = Vector3.Lerp(transform.forward, targetDirection, elapsed / 4);
        GetComponent<Rigidbody>().velocity = currentDirection.normalized * currentSpeed;
        transform.rotation = Quaternion.LookRotation(currentDirection);
    }

    bool PrepareAttack(float elapsed)
    {
        var prepareTime = 2;
        var targetDirection = PlayerController.player.transform.position - transform.position;
        var currentDirection = Vector3.Lerp(transform.forward, targetDirection, elapsed / prepareTime);
        GetComponent<Rigidbody>().velocity = currentDirection.normalized * currentSpeed;
        transform.rotation = Quaternion.LookRotation(currentDirection);
        if (targetDirection.magnitude < .5)
        {
            // too close to fire safely
            return true;
        }
        if (elapsed >= prepareTime)
        {
            ShootWeapon();
            return true;
        }
        return false;
    }

    bool Recharge(float elapsed)
    {
        var rechargeTime = 5f;
        var rb = GetComponent<Rigidbody>();
        currentSpeed = Mathf.Lerp(maxSpeed, 0, elapsed / rechargeTime);
        rb.velocity = rb.velocity.normalized * currentSpeed;
        return elapsed >= rechargeTime;
    }

    bool RevUp(float elapsed)
    {
        var revUpTime = 2f;
        var rb = GetComponent<Rigidbody>();
        currentSpeed = Mathf.Lerp(0, maxSpeed, elapsed / revUpTime);
        rb.velocity = rb.velocity.normalized * currentSpeed;
        return elapsed >= revUpTime;
    }

    IEnumerator DestroyEnemy()
    {
        GetComponent<TrailRenderer>().emitting = true;
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 1);
        yield return new WaitForSeconds(1f);
        print("Destoyed!");
        Destroy(this.gameObject);
    }

    public IEnumerator EnemyActions()
    {
        yield return null;
        var timeElapsed = 0f;
        var moveTime = 7f;

        while(true)
        {
            // Start as moving.
            
            switch (state)
            {
                case AIState.MOVE:
                    timeElapsed += Time.deltaTime;
                    Move(timeElapsed);
                    if (timeElapsed > moveTime)
                    {
                        direction *= -1;
                        timeElapsed = 0;
                        state = AIState.ATTACK;
                    }
                    break;
                case AIState.ATTACK:
                    bool shotFiredOrTooClose = PrepareAttack(timeElapsed);
                    timeElapsed += Time.deltaTime;
                    if (shotFiredOrTooClose)
                    {
                        timeElapsed = 0;
                        state = AIState.RECHARGE;
                    }
                    break;
                case AIState.RECHARGE:
                    bool isRecharged = Recharge(timeElapsed);
                    timeElapsed += Time.deltaTime;
                    if (isRecharged)
                    {
                        timeElapsed = 0;
                        state = AIState.REVUP;
                    }
                    break;
                case AIState.REVUP:
                    bool isRevvedUp = RevUp(timeElapsed);
                    timeElapsed += Time.deltaTime;
                    if (isRevvedUp)
                    {
                        GetComponent<AudioSource>().Play();
                        timeElapsed = 0;
                        state = AIState.MOVE;
                    }
                    break;
            }
            yield return null;
        }
    }

    public void OnDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            StopCoroutine(EnemyActions());
            StartCoroutine(DestroyEnemy());
        }
    }

    public void ShootWeapon()
    {
        print("Shot Fired!");
        var bulletInstance = Instantiate(bullet);
        bulletInstance.transform.position = bulletSpawnPosition.transform.position;
        bulletInstance.transform.rotation = bulletSpawnPosition.transform.rotation;
    }
}

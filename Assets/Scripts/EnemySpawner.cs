using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject portalObject;
    public Transform directionTransform;
    private Vector3 direction;

    void Start()
    {
        direction = directionTransform.position - transform.position;
        StartCoroutine(SpawnEnemy());
    }

    public IEnumerator SpawnEnemy()
    {
        float scale = 0f;
        float maxScale = .5f;
        float timeElapsed = 0f;
        float rotationSpeed = 270f;

        portalObject.transform.localScale = new Vector3(scale, scale, portalObject.transform.localScale.z);
        while (scale < maxScale)
        {
            scale += Time.deltaTime;
            portalObject.transform.localScale = new Vector3(scale, scale, portalObject.transform.localScale.z);
            portalObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        var enemyInstance = Instantiate(enemy);
        enemyInstance.transform.position = transform.position;
        enemyInstance.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        // PLAY SPAWN SOUND EFFECT
        while (scale > 0)
        {
            scale -= Time.deltaTime;
            portalObject.transform.localScale = new Vector3(scale, scale, portalObject.transform.localScale.z);
            portalObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}

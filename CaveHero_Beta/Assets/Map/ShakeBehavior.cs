using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShakeBehavior : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform transform;
    // Desired duration of the shake effect
    private float shakeDuration = 0f;
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.5f;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
    // The initial position of the GameObject
    Vector3 initialPosition;

    public Transform player;

    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }

        if(SceneManager.GetActiveScene().name == "ThroneRoom")
        {
            TriggerShake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        initialPosition = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        if (shakeDuration > 0)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = initialPosition;
        }
    }
    public void TriggerShake()
    {
        this.GetComponent<FollowPlayer>().StopFollow();
        shakeDuration = 2.0f;
    }
}

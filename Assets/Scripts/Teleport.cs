using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameDev.tv Challenge Club. Got questions or want to share your nifty solution?
// Head over to - http://community.gamedev.tv

public class Teleport : MonoBehaviour
{
    public Light areaLight;
    public Light mainWorldLight;

    private bool _isDestination;
    private Teleport _currentTarget;
    private ScoreText _scoreText;

    public void MarkAsDestination()
    {
        _isDestination = true;
    }

    public void TurnOnAreaLight()
    {
        areaLight.enabled = true;
    }

    private void Awake()
    {
        _scoreText = FindObjectOfType<ScoreText>();
    }

    private void Start()
    {
        // CHALLENGE TIP: Make sure all relevant lights are turned off until you need them on
        // because, you know, that would look cool.
        areaLight.enabled = false;
        mainWorldLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (_isDestination)
        {
            // Challenge 3: DeactivateObject();
            DeactivateObject();
            _scoreText.ArrivedAtTeleportPad();
            return;
        }

        // Challenge 5: StartCoroutine ("BlinkWorldLight");
        StartCoroutine(BlinkWorldLight());

        DefineTeleportTarget();

        // Challenge 4: IlluminateArea();
        IlluminateArea();

        // Challenge 2: TeleportPlayer();
        // Challenge 6: TeleportPlayerRandom();
        TeleportPlayer(other);
    }

    private void DefineTeleportTarget()
    {
        var targets = Helpers.GetTeleportPads(this);
        _currentTarget = targets.Length == 0
            ? null
            : targets[Random.Range(0, targets.Length)];
    }

    private void TeleportPlayer(Component other)
    {
        if (_currentTarget == null) DefineTeleportTarget();

        var gameOver = _currentTarget == null;
        if (gameOver) return;

        _currentTarget.MarkAsDestination();
        _isDestination = false;
        var newPosition = _currentTarget.transform.position;
        newPosition.z += 0.5f;

        other.gameObject.transform.position = newPosition;
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
    }

    private void IlluminateArea()
    {
        areaLight.enabled = false;
        if (_currentTarget == null) return;
        _currentTarget.TurnOnAreaLight();
    }

    private IEnumerator BlinkWorldLight()
    {
        ToggleMainWorldLight();
        foreach (var f in new List<float> { .3f, .2f, .15f })
        {
            yield return new WaitForSeconds(f);
            ToggleMainWorldLight();
        }
    }

    private void ToggleMainWorldLight()
    {
        mainWorldLight.enabled = !mainWorldLight.enabled;
    }
}
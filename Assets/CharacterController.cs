using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float xThreshold = 10f; // X position at which to trigger scene load

    public static GameObject characterPrefab;

    void Update()
    {
        HandleMovement();
        CheckSceneTransition();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal"); // Supports WASD & Arrow Keys
        float moveY = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, moveY, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
    }

    private void CheckSceneTransition()
    {
        if (transform.position.x > xThreshold)
        {
            SceneManager.LoadScene(MainSystem.GetScene());
            Destroy(this.gameObject);
        }
    }

    public static GameObject Spawn(Vector3 spawnPosition)
    {
        if (characterPrefab == null)
        {
            characterPrefab = Resources.Load<GameObject>("Player");
        }

        if (characterPrefab == null)
        {
            Debug.LogError("PlayerCharacter prefab not found in Resources folder.");
            return null;
        }

        return Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
    }
}
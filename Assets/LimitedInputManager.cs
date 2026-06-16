using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LimitedInputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Button submitButton;

    private void Start()
    {
        inputField.characterLimit = 12;

        inputField.onValueChanged.AddListener(OnInputChanged);

        submitButton.interactable = false;
    }

    private void OnInputChanged(string input)
    {
        submitButton.interactable = (input.Length >= 3);
        MainSystem.name = input;
    }

    public void Play()
    {
        SceneManager.LoadScene("Bedroom");
    }
}
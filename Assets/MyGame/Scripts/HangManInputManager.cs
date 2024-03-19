using UnityEngine;
using UnityEngine.UI;

public class HangManInputManager : MonoBehaviour
{
    public HangmanGameManager gameManager;
    public InputField inputField;

    void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnInputEnd(); });
    }

    void OnInputEnd()
    {
        string guess = inputField.text.ToLower();

        if (string.IsNullOrEmpty(guess) || guess.Length != 1 || !char.IsLetter(guess[0]))
        {
            Debug.Log("Please enter a valid single letter.");
            return;
        }

        char letter = guess[0];

        if (!gameManager.GuessLetter(letter))
        {
            Debug.Log("You already guessed that letter.");
        }

        inputField.text = ""; // Clear input field after guess
    }
}

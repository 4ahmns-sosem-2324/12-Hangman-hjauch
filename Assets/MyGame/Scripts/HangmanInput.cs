using UnityEngine;
using UnityEngine.UI;

public class HangmanInput : MonoBehaviour
{
    public InputField inputField;

    public HangMan hangmanGame; // Reference to the HangmanGame script

    void Start()
    {
        inputField.onEndEdit.AddListener(delegate { OnInputEnd(); }); // Listen to end edit event on the input field
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

        hangmanGame.Guess(); // Call the Guess method in the HangmanGame script
        inputField.text = ""; // Clear input field after guess
    }
}

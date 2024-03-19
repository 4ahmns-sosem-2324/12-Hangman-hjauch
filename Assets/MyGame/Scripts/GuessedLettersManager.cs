using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GuessedLettersManager : MonoBehaviour
{
    public Text guessedLettersText; // Reference to the UI Text for displaying guessed letters

    private HashSet<char> guessedLetters = new HashSet<char>();

    void UpdateDisplay()
    {
        guessedLettersText.text = "Guessed Letters: " + string.Join(", ", guessedLetters);
    }

    public void AddGuessedLetter(char letter)
    {
        guessedLetters.Add(letter);
        UpdateDisplay();
    }

    public void ClearGuessedLetters()
    {
        guessedLetters.Clear();
        UpdateDisplay();
    }
}

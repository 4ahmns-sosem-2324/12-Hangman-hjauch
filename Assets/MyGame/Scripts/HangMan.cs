using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HangMan : MonoBehaviour
{
    public Text wordDisplayText;
    public InputField inputField;
    public Text attemptsText;

    private string[] words = { "apple", "banana", "orange", "grape", "strawberry", "kiwi", "peach" };
    private string selectedWord;
    private List<char> guessedLetters = new List<char>();
    private int attempts = 6;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        selectedWord = words[Random.Range(0, words.Length)];
        guessedLetters.Clear();
        attempts = 6;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        wordDisplayText.text = GetDisplayWord();
        attemptsText.text = "Attempts left: " + attempts;
    }

    string GetDisplayWord()
    {
        string displayWord = "";
        foreach (char letter in selectedWord)
        {
            if (guessedLetters.Contains(letter))
                displayWord += letter;
            else
                displayWord += "_";
        }
        return displayWord;
    }

    public void Guess()
    {
        string guess = inputField.text.ToLower();
        inputField.text = ""; // Clear input field after guess

        if (guess.Length != 1 || !char.IsLetter(guess[0]))
        {
            Debug.Log("Please enter a single letter.");
            return;
        }

        char letter = guess[0];

        if (guessedLetters.Contains(letter))
        {
            Debug.Log("You already guessed that letter.");
            return;
        }

        guessedLetters.Add(letter);

        if (!selectedWord.Contains(letter))
        {
            attempts--;
            if (attempts <= 0)
            {
                Debug.Log("Sorry, you lost. The word was: " + selectedWord);
                StartGame();
            }
        }

        if (GetDisplayWord().Equals(selectedWord))
        {
            Debug.Log("Congratulations! You guessed the word: " + selectedWord);
            StartGame();
        }

        UpdateDisplay();
    }
}

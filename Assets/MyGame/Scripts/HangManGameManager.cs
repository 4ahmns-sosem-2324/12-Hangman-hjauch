using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class HangmanGameManager : MonoBehaviour
{
    public Text wordDisplayText;
    public Text attemptsText;

    private string[] words = { "apple", "banana", "orange", "grape", "strawberry", "kiwi", "peach" };
    private string selectedWord;
    private List<char> guessedLetters = new List<char>();
    private int attempts = 6;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
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

    public bool GuessLetter(char letter)
    {
       
        if (guessedLetters.Contains(letter))
            return false; // Letter already guessed

        guessedLetters.Add(letter);

        if (!selectedWord.Contains(letter))
        {
            attempts--;
            if (attempts <= 0)
            {
                Debug.Log("Sorry, you lost. The word was: " + selectedWord);
                return false; // Game over
            }
        }

        if (GetDisplayWord().Equals(selectedWord))
        {
            Debug.Log("Congratulations! You guessed the word: " + selectedWord);
            UpdateDisplay();
            return true; // Player won
        }

        UpdateDisplay();
        return true; // Guess successful


    }
}

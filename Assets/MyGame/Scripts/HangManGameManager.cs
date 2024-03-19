using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;

public class HangmanGame : MonoBehaviour
{
    public Text wordDisplayText;
    public Text guessedLettersText;
    public Text attemptsText;

    string[] words = {
   "apfel", "banane", "orange", "traube", "erdbeere", "kiwi", "pfirsich", "wassermelone", "ananas", "heidelbeere",
    "computer", "tastatur", "maus", "monitor", "drucker", "kopfhörer", "lautsprecher", "mikrofon", "laptop", "tablet",
    "baum", "blume", "gras", "sonnenblume", "gänseblümchen", "rose", "tulpe", "löwenzahn", "kaktus", "farn",
    "auto", "fahrrad", "motorrad", "bus", "lastwagen", "zug", "flugzeug", "hubschrauber", "boot", "schiff",
    "hund", "katze", "vogel", "fisch", "hamster", "kaninchen", "schildkröte", "schlange", "eidechse", "frosch",
    "haus", "wohnung", "villa", "hütte", "schloss", "zelt", "iglu", "bungalow", "blockhütte", "landhaus",
    "berg", "hügel", "tal", "schlucht", "plateau", "klippe", "vulkan", "gletscher", "höhle", "ozean",
    "schule", "universität", "college", "bibliothek", "klassenzimmer", "laboratorium", "turnhalle", "aula", "mensa", "spielplatz",
    "fußball", "basketball", "fußball", "baseball", "volleyball", "tennis", "golf", "rugby", "cricket", "hockey",
    "pizza", "hamburger", "sandwich", "nudeln", "sushi", "steak", "salat", "suppe", "burrito", "taco"

    };

    
    private string selectedWord;
    private HashSet<char> wrongLetters = new HashSet<char>();
    private HashSet<char> correctLetters = new HashSet<char>();
    private int attempts = 5;
    public GameObject [] hangman;
    public Text winText;
    public Text loseText;
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        selectedWord = words[Random.Range(0, words.Length)];
        wrongLetters.Clear();
        correctLetters.Clear();
        attempts = 5;
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        string displayWord = "";
        foreach (char letter in selectedWord)
        {
            if (correctLetters.Contains(letter))
                displayWord += letter + " ";
            else
                displayWord += "_ ";
        }
        wordDisplayText.text = displayWord;

        guessedLettersText.text = "Wrong Letters: ";
        foreach (char letter in wrongLetters)
        {
            guessedLettersText.text += letter + ", ";
        }

        attemptsText.text = "Attempts left: " + attempts;
    }

    void Update()
    {
        if (Input.anyKeyDown && attempts > 0 && !CheckWin())
        {
            char keyPressed = GetKeyPressed();
            if (keyPressed != '\0')
            {
                if (selectedWord.Contains(keyPressed))
                {
                    correctLetters.Add(keyPressed);
                }
                else if (!wrongLetters.Contains(keyPressed))
                {
                    wrongLetters.Add(keyPressed);
                    hangman[5 - attempts].SetActive(true);
                    attempts--;
                }

                UpdateDisplay();

                if (attempts == 0)
                {
                    losePanel.SetActive(true);
                    loseText.text = "You Lost. The word was: " + selectedWord;
                    Debug.Log("Sorry, you lost. The word was: " + selectedWord);
                }
                else if (CheckWin())
                {
                    winPanel.SetActive(true);
                    winText.text = "You Won! Congratulations";
                    //SceneManager.LoadScene("WinScene");
                    Debug.Log("Congratulations! You guessed the word: " + selectedWord);
                }
            }
        }
    }

    char GetKeyPressed()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c))
            {
                return char.ToLower(c);
            }
        }
        return '\0';
    }

    bool CheckWin()
    {
        foreach (char letter in selectedWord)
        {
            if (!correctLetters.Contains(letter))
            {
                return false;
            }
        }
        return true;
    }
}

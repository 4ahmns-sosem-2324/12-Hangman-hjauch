using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class HangmanGame : MonoBehaviour
{
    public Text wordDisplayText;
    public Text guessedLettersText;
    public Text attemptsText;

    public string[] words = {
    "Apfel", "Banane", "Orange", "Traube", "Erdbeere", "Kiwi", "Pfirsich", "Wassermelone", "Ananas", "Heidelbeere",
    "Computer", "Tastatur", "Maus", "Monitor", "Drucker", "Kopfhörer", "Lautsprecher", "Mikrofon", "Laptop", "Tablet",
    "Baum", "Blume", "Gras", "Sonnenblume", "Gänseblümchen", "Rose", "Tulpe", "Löwenzahn", "Kaktus", "Farn",
    "Auto", "Fahrrad", "Motorrad", "Bus", "Lastwagen", "Zug", "Flugzeug", "Hubschrauber", "Boot", "Schiff",
    "Hund", "Katze", "Vogel", "Fisch", "Hamster", "Kaninchen", "Schildkröte", "Schlange", "Eidechse", "Frosch",
    "Haus", "Wohnung", "Villa", "Hütte", "Schloss", "Zelt", "Iglu", "Bungalow", "Blockhütte", "Landhaus",
    "Berg", "Hügel", "Tal", "Schlucht", "Plateau", "Klippe", "Vulkan", "Gletscher", "Höhle", "Ozean",
    "Schule", "Universität", "College", "Bibliothek", "Klassenzimmer", "Laboratorium", "Turnhalle", "Aula", "Mensa", "Spielplatz",
    "Fußball", "Basketball", "Fußball", "Baseball", "Volleyball", "Tennis", "Golf", "Rugby", "Cricket", "Hockey",
    "Pizza", "Hamburger", "Sandwich", "Nudeln", "Sushi", "Steak", "Salat", "Suppe", "Burrito", "Taco"
    };

    
    private string selectedWord;
    private HashSet<char> wrongLetters = new HashSet<char>();
    private HashSet<char> correctLetters = new HashSet<char>();
    private int attempts = 5;
    public GameObject [] hangman;

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
                    Debug.Log("Sorry, you lost. The word was: " + selectedWord);
                }
                else if (CheckWin())
                {
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

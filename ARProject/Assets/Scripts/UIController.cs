using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public Button startButton;
    public GameObject GuessButtons;
    public Button[] optionButtons;
    public AudioClip[] audioClips;
    public AudioClip oppsAudioClip;
    public AudioClip sucessAudioClip;
    public AudioSource audioSource;
    public Button nextButton;
    private int guessNumber=0;
    public GameObject SuccessWindow;

    private Dictionary<int, bool> numberDictionary = new Dictionary<int, bool>();

        
private void Start()
    {
        promptText.text = "Welcome! Press Start to play.";
        startButton.onClick.AddListener(StartGame);
        for (int i = 1; i <= 10; i++)
        {
            numberDictionary.Add(i, false);
        }


    }

    private void StartGame()
    {
        startButton.gameObject.SetActive(false);
       
        promptText.text = "Find a number by tap on the screen.";
    }

    public void RestartGame()
    {
        for (int i = 1; i <= 10; i++)
        {
            numberDictionary[i]=false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    public void FindNextnumber()
    {
        StartGame();
        nextButton.gameObject.SetActive(false); 
        GuessButtons.gameObject.SetActive(false);
        FindObjectOfType<RaycastAndDisplayText>().ResetGuessNumber();
    }
    public void CorrctOption() {
        numberDictionary[guessNumber] = true;
       
        if (guessAllNumbers())
        {
            promptText.text = "Congratulations! You found all correct number!";
            SuccessWindow.SetActive(true);
        }
        else
        {
            promptText.text = "Congratulations! You found correct number!";
            nextButton.gameObject.SetActive(true);
        }
        audioSource.clip = sucessAudioClip;
        audioSource.Play();
       
    }

    private bool guessAllNumbers()
    {
        bool allValuesTrue = true;
        foreach (var value in numberDictionary.Values)
        {
            if (!value)
            {
                allValuesTrue = false;
                break; // Exit the loop if a false value is found
            }
        }

        return allValuesTrue;
    }
    public void WrongOption() {
        promptText.text = "Oops! Try again.";
        nextButton.gameObject.SetActive(true);
        audioSource.clip = oppsAudioClip;
        audioSource.Play();
    }
    void SetUpOptionButtons(int opt1, int opt2)
    {
        GuessButtons.GetComponent<HorizontalLayoutGroup>().reverseArrangement = Random.Range(0, 2) != 0; 
        optionButtons[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(opt1.ToString());
        optionButtons[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(opt2.ToString());

    }
    public void DisplayOption(int currentNumber)
    {
        guessNumber = currentNumber;
        promptText.text = "Find and tap the displayed number.";
        GuessButtons.SetActive(true);
        int opt2 = GetDifferentNumber(currentNumber);
        audioSource.clip = audioClips[currentNumber-1];
        audioSource.Play();
        SetUpOptionButtons(currentNumber,opt2);
    }
    public int GetDifferentNumber(int number)
    {
        int differentNumber;
        do
        {
            differentNumber = Random.Range(1, 11);
        } while (differentNumber == number);

        return differentNumber;
    }
}

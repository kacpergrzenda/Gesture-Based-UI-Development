using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;
using System.IO;
using System.Text;
public class SelectLevel : MonoBehaviour
{
    /* Variables */
    public GameObject mainMenuGM;
    GrammarRecognizer gr;
    const string SIMPLE_G = "GameGrammar.xml";
    private string wordValue;
    void Start()
    {
        gr = new GrammarRecognizer( Path.Combine(Application.streamingAssetsPath, SIMPLE_G), ConfidenceLevel.Low);

        Debug.Log("Grammar is loaded " + gr.GrammarFilePath);

        gr.OnPhraseRecognized += GR_OnPhrasesRecognised;

        gr.Start();

        if (gr.IsRunning) Debug.Log("GR is running.");
    }

    private void GR_OnPhrasesRecognised(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Recognised Something...");
        // Read the semantic meanings from the args returned
        // Put them in a string to print a message in the console
        StringBuilder message = new StringBuilder();

        SemanticMeaning[] meanings = args.semanticMeanings;

        // Returned a set of name/value pairs - keys/values
        // Move pawn from C2 to C4
        // Piece/pawn, start/c2, finish/c4
        foreach (SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            wordValue = meaning.values[0].Trim();

            
            message.Append("Key: " + keyString +
                            ", Value: " + wordValue +
                            System.Environment.NewLine);
        }

        Debug.Log(message);
        CheckWordAction(wordValue);  
    }

    private void CheckWordAction(string wordAction)
    {
        switch (wordAction)
        {
            case "levelone" :
                //StartGame();
                break;
            case "leveltwo" :
                //QuitGame();
                break;
            case "levelthree" :
            //QuitGame();
                break;
            case "back" :
                Back();
                break;
            default:
                break;
        }
    }

    public void Back() {
       stopGR();
       mainMenuGM.SetActive(true);
       gameObject.SetActive(false);
    }

    public void LevelOne()
    {
        stopGR();
        SceneManager.LoadScene(1);
    }

    public void LevelTwo()
    {
        stopGR();
        SceneManager.LoadScene(2);
    }

    public void LevelThree()
    {
        stopGR();
        SceneManager.LoadScene(3);
    }

    private void stopGR() {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhrasesRecognised;

            Debug.Log("GR has stopped.");
            gr.Stop();
        }
    }
}

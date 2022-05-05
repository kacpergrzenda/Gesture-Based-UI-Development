using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.IO;
using System.Text;
public class VoiceController : MonoBehaviour
{
    /* Variables */
    public GameObject deathMenu;
    public GameObject pasueMenu;
    public GameObject winMenu;
    GrammarRecognizer gr;
    const string SIMPLE_G = "GameGrammar.xml";
    private string wordValue;
    // Start is called before the first frame update
    void Start()
    {
        StartGR();
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
            case "winnextlevel" :
                gameObject.GetComponent<VoiceWinMenu>().LoadNextLevel();
                break;
            case "winquitgame" :
                winMenu.GetComponent<WinMenu>().QuitGame();
                break;
            case "pausegame" :
                pasueMenu.GetComponent<PauseMenu>().Pause();
                break;
            case "resumegame" :
                pasueMenu.GetComponent<PauseMenu>().Resume();
                break;
            case "retry" :
                deathMenu.GetComponent<DeathMenu>().Retry();
                break;
            default:
                break;
        }
    }

    /* Start The Grammar Recogniser */
    public void StartGR() {
        gr = new GrammarRecognizer( Path.Combine(Application.streamingAssetsPath, SIMPLE_G), ConfidenceLevel.Low);

        Debug.Log("Grammar is loaded " + gr.GrammarFilePath);

        gr.OnPhraseRecognized += GR_OnPhrasesRecognised;

        gr.Start();

        if (gr.IsRunning) Debug.Log("GR is running.");
    }

    /* Stop The Grammar Recogniser */
    private void stopGR() {
        if (gr != null && gr.IsRunning)
        {
            gr.OnPhraseRecognized -= GR_OnPhrasesRecognised;

            Debug.Log("GR has stopped.");
            gr.Stop();
        }
    }
}

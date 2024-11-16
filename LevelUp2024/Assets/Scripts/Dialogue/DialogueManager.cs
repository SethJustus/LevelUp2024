using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI TextObj;
    [SerializeField] private InputActionReference InteractAction;

    private Story CurrentSpeach;
    private bool firstframe;
    private bool textPlaying;
    private static DialogueManager instance;
    private void Awake(){
        if(instance != null){
            Debug.LogWarning("Än instance of singleton object DialogueManager already exists");
        }
        instance = this;
    }

    public static DialogueManager GetInstance(){
        return instance;
    }

    private void Start(){
        textPlaying = false;
        panel.SetActive(false);
    }

    private void Update(){
        if(!textPlaying || firstframe){
            firstframe = false;
            return;
        }
        if (InteractAction.action.triggered){
            
            ContStory();
        }

    }

    public void EnterDialogue(TextAsset InkJSONFile){
        CurrentSpeach = new Story(InkJSONFile.text);
        textPlaying = true;
        panel.SetActive(true);

        ContStory();
        firstframe = true;
    }

    public void ExitDialogue(){
        textPlaying = false;
        panel.SetActive(false);
        TextObj.text = "";
    }

    private void ContStory(){
        if(CurrentSpeach.canContinue){
            TextObj.text = CurrentSpeach.Continue();
            print(TextObj.text);
        } else{
            print("hoy");
            ExitDialogue();
        }
    }


}

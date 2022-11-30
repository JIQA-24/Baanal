using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class ScriptReader : MonoBehaviour
{
    [SerializeField]
    private TextAsset _InkJsonFile;
    private Story _StoryScript;

    public TMP_Text dialogueBox;
    public TMP_Text nameTag;

    public Image characterIcon;
    public RawImage background;
    public Animator animator;


    [SerializeField]
    private GridLayoutGroup choiceHolder;

    [SerializeField]
    private Button choiceBasePrefab;

    void Start()
    {
        LoadStory();
        DisplayNextLine();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            DisplayNextLine();
        }
    }

    void LoadStory() {
        _StoryScript = new Story(_InkJsonFile.text);
        _StoryScript.BindExternalFunction("Name", (string charName) => ChangeName(charName));
        _StoryScript.BindExternalFunction("Icon", (string charIcon) => ChangeSprite(charIcon));
        _StoryScript.BindExternalFunction("Font", (string charFont) => ChangeFont(charFont));
        _StoryScript.BindExternalFunction("Background", (string backgroundImage) => ChangeBackround(backgroundImage));
        _StoryScript.BindExternalFunction("FontStyle", (string fontStyle) => ChangeFontStyle(fontStyle));
        _StoryScript.BindExternalFunction("FadeIn", (float speed) => TransitionFadeIn(speed));
        _StoryScript.BindExternalFunction("FadeOut", (float speed) => TransitionFadeOut(speed));
    }

    public void DisplayNextLine() {
        if(_StoryScript.canContinue) {
            string text = _StoryScript.Continue();
            text = text?.Trim();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(text));
        }
        else if(_StoryScript.currentChoices.Count > 0){
            DisplayChoices();
        }
        else {
            // End of script
            animator.SetTrigger("FadeOut");
        }
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueBox.text = "";

        foreach(char letter in sentence) {
            
            dialogueBox.text += letter;
            yield return new WaitForSeconds(0.03F);
        }
    }

    private void DisplayChoices() {
        if(choiceHolder.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _StoryScript.currentChoices.Count; i++){
            var choice = _StoryScript.currentChoices[i];
            var button = CreateChoiceButton(choice.text);

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    Button CreateChoiceButton(string choice) {
        var choiceButton = Instantiate(choiceBasePrefab);
        choiceButton.transform.SetParent(choiceHolder.transform, false);

        var buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = choice;
        return choiceButton;
    }

    void OnClickChoiceButton(Choice choice) {
        _StoryScript.ChooseChoiceIndex(choice.index);
        RefreshChoiceView();
        DisplayNextLine();
    }

    void RefreshChoiceView() {
        if(choiceHolder != null) {
            foreach(var button in choiceHolder.GetComponentsInChildren<Button>()) {
                Destroy(button.gameObject);
            }
        }
    }

    public void ChangeName(string name) {
        string SpeakerName = name;
        nameTag.text = SpeakerName;
    }

    public void ChangeSprite(string icon) {
        if(icon == "") {
            characterIcon.sprite = null;
            return;
        }
        var charIcon = Resources.Load<Sprite>("CharacterSprites/" + icon);
        characterIcon.sprite = charIcon;
    }

    public void ChangeFont(string font) {
        var charFont = Resources.Load<TMP_FontAsset>("Fonts/" + font);
        dialogueBox.font = charFont;
    }

    public void ChangeBackround(string backgroundImage) {
        var bgImage = Resources.Load<Texture>("Scenario Backgrounds/" + backgroundImage);
        background.texture = bgImage;
    }

    public void ChangeFontStyle(string style) {
        if(style == "Italic") dialogueBox.fontStyle = FontStyles.Italic;
        if(style == "Bold") dialogueBox.fontStyle = FontStyles.Bold;
        else dialogueBox.fontStyle = FontStyles.Normal;

    }

    public void TransitionFadeIn(float speed) {
        animator.speed = speed;
        animator.SetTrigger("FadeIn");
    }

    public void TransitionFadeOut(float speed) {
        animator.speed = speed;
        animator.SetTrigger("FadeOutScene");
    }
}

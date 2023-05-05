using System.Collections;
using UnityEngine;
using TMPro;

public class GameDescriptionAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() {
        GameDescriptionText = GetComponent<TMP_Text>();
        GameDescriptionText.text = TheGameDescriptionText;
        GameDescriptionText.alignment = TextAlignmentOptions.Center;
        StartCoroutine(PlayTheTextSequence());
        // GameDescriptionText.enableWordWrapping = true;
    }
    public IEnumerator PlayTheTextSequence()
    {
        GameDescriptionText.ForceMeshUpdate();
        int TotalCharactersVisible = GameDescriptionText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;
        //should i change the color midway can be the next update
        while(visibleCount <= TotalCharactersVisible) {
            if(visibleCount >= TotalCharactersVisible) {
                yield return new WaitForSeconds(1.0f);
                GameDescriptionText.text = TheGameDescriptionText;
                yield return new WaitForSeconds(1.0f);
                break;
                /// idu enakke andre atleast two seconds frame nalli naan display maado text irli anta.
            }

            counter = counter % (TotalCharactersVisible + 1);
            visibleCount = counter;
            GameDescriptionText.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(0.06f);
        }
    }
    private TMP_Text GameDescriptionText;

    private string TheGameDescriptionText = "Win The War For Us And The world will give you a reward \nCommander";
}

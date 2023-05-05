using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameWonAnimations : MonoBehaviour
{
    
    void Awake() {
        GameWonText = GetComponent<TMP_Text>();
        GameWonText.text = TheGameWonText;
        // GameWonText.enableWordWrapping = true;
    }
    public IEnumerator PlayTheTextSequence()
    {
        GameWonText.ForceMeshUpdate();
        int TotalCharactersVisible = GameWonText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;
        
        while(visibleCount <= TotalCharactersVisible) {
            if(visibleCount >= TotalCharactersVisible) {
                yield return new WaitForSeconds(1.0f);
                GameWonText.text = TheGameWonText;
                yield return new WaitForSeconds(1.0f);
                break;
                /// idu enakke andre atleast two seconds frame nalli naan display maado text irli anta.
            }

            counter = counter % (TotalCharactersVisible + 1);
            visibleCount = counter;
            GameWonText.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(0.06f);
        }
    }
    public void GameWonButtonRewardTrigger(){
        SceneManager.LoadScene(2);
    }



    private TMP_Text GameWonText;

    private string TheGameWonText = "We Wonn Commander \n We Won The war";
}

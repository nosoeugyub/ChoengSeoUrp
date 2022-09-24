using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CutSceneImage
{
    public bool isOpen = false;
    public Image img;
    public Button button;
    public Image LibraryImage;
    public GameObject Numtext;

}

public enum CutType
{
    None,
    BearChick,//닭
    WatchDearAndSheepExercising,//벌
    BearRibbonToHen, //곰
    WatchSheepAndRabbit, // 닭
    WatchDearExercising, //벌
    ButtingCowToBear, //벌
    BearAndWalrus,//바다
    FakeLuxuryToCow,//바다
    ComeToPort,//곰
}

public class CutScene : MonoBehaviour
{
    public CutSceneImage[] Image;

    [SerializeField] Transform ImageSpawn;
    [SerializeField] Button ConfirmButton;
    [SerializeField] GameObject CutSceneLibrary;
    [SerializeField] Fader fader;
    Image curImage;

    [SerializeField] GameEvent playerMoveOnEvent;
    [SerializeField] GameEvent playerMoveOffEvent;

    [SerializeField] GameEvent playerCanInteractEvent;
    [SerializeField] GameEvent playerCantInteractEvent;

    public static bool IsCutSceneOn { get; set; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        PrintImage(3);
    }

    public void PrintImage(int index)//Calling this will also unlock the Image in the Library
    {
        ChangeIsCutSceneOn(true);
        index--;
        if (Image[index] != null && !Image[index].isOpen)
        {
            Image[index].isOpen = true;
            Image[index].button.interactable = true;
            //Image[index].LockedImage.gameObject.SetActive(false);
            Image[index].Numtext.gameObject.SetActive(false);
            Image[index].LibraryImage.enabled = true;

            StartCoroutine(GetImage(index));

        }
    }

    IEnumerator GetImage(int index)
    {
        playerMoveOffEvent.Raise();
        playerCantInteractEvent.Raise();
        yield return fader.IFadeOut(Color.black,2);
        curImage = Instantiate(Image[index].img, ImageSpawn.position, ImageSpawn.rotation, ImageSpawn);
        curImage.rectTransform.sizeDelta = new Vector2(1920, 1080);
        yield return fader.IFadeIn(Color.black, 2);
        yield return new WaitForSeconds(1);
        ShowConfirmButton();
    }

    public void OpenImage(int index) //Open the unlocked Image and show the "확인" button without 3sec waiting ->Use in Image Library
    {
        if (Image[index] != null && !Image[index].isOpen)
        {
            //SetLibraryStatus(false);
            Image[index].isOpen = true;
            curImage = Instantiate(Image[index].img, ImageSpawn.position, ImageSpawn.rotation, ImageSpawn);
            curImage.rectTransform.sizeDelta = new Vector2(1920, 1080);
            ShowConfirmButton();

        }
    }
    public void CloseImage()
    {
        Destroy(curImage.gameObject);
        playerMoveOnEvent.Raise();
        playerCanInteractEvent.Raise();
        ConfirmButton.gameObject.SetActive(false);
        foreach (CutSceneImage image in Image)
        {
            image.isOpen = false;
        }

    }
    void ShowConfirmButton()
    {
        ConfirmButton.gameObject.SetActive(true);
    }

    public void SetLibraryStatus(bool status)
    {
        CutSceneLibrary.SetActive(status);
    }

    public void CloseCutSceneLibrary()
    {

    }

    public void ChangeIsCutSceneOn(bool ison)
    {
        IsCutSceneOn = ison;
    }
}

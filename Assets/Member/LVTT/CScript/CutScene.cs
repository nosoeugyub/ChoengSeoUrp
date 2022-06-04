using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CutSceneImage
{
    public bool isOpen = false;
    public Image img;
    public Image LockedImage;
}

public enum CutType
{
    None,
    BearChick,
    WatchDearAndSheepExercising,
    BearRibbonToHen,
    WatchSheepAndRabbit,
    WatchDearExercising,
    ButtingCowToBear,
    BearAndWalrus,
    FakeLuxuryToCow,
}

public class CutScene : MonoBehaviour
{
    public CutSceneImage[] Image;

    [SerializeField] Transform ImageSpawn;
    [SerializeField] Button ConfirmButton;
    [SerializeField] GameObject CutSceneLibrary;
    Image curImage;


    public void PrintImage(int index)//Calling this will also unlock the Image in the Library
    {
        index--;
        if (Image[index] != null && !Image[index].isOpen)
        {
            Image[index].isOpen = true;
            Image[index].LockedImage.gameObject.SetActive(false);
            curImage = Instantiate(Image[index].img, ImageSpawn.position, ImageSpawn.rotation, ImageSpawn);
            curImage.rectTransform.sizeDelta = new Vector2(1920, 1080);
            Invoke("ShowConfirmButton", 3f);

        }

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
}

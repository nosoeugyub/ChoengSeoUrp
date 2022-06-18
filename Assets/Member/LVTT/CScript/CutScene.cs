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
    Image curImage;
    int num = 1;

    public static bool IsCutSceneOn { get; set; }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PrintImage(num);
            num++;
        }
    }
    public void PrintImage(int index)//Calling this will also unlock the Image in the Library
    {
        ChangeIsCutSceneOn(true);
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

    public void ChangeIsCutSceneOn(bool ison)
    {
        IsCutSceneOn = ison;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ShowWinnerMaster : MonoBehaviour {
    public GameObject oneGUIelement;
    public List<GameObject> createdElements;
    float xSize = 10f;
    bool lookedEveryImage = false;
    
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (movingCamera)
        {
            float elapsed = Time.time - moveStartTime;
            if (moveDuration > elapsed)
            {
                float zPos = UIcamera.transform.position.z;
                Vector3 newPos = Vector3.Lerp(startLoc, endLoc, elapsed/moveDuration);
                newPos.z = zPos;
                UIcamera.transform.position = newPos;
            }
            if (whatPlayerShowing >= Common.roundInformation.gameData.playerAmount)
            {
                lookedEveryImage = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateElements(Common.roundInformation.gameData.playerAmount);
            MoveCamera(testMove);
           
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(LoadAllImages());

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwipeLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwipeRight();
        }
	}

    void SwipeLeft()
    {
        whatPlayerShowing--;
        if (whatPlayerShowing < 0)
        {
            whatPlayerShowing = Common.roundInformation.gameData.playerAmount-1;
        }
        MoveCamera(whatPlayerShowing);
    }

    void SwipeRight()
    {
        whatPlayerShowing++;
        if (whatPlayerShowing >= Common.roundInformation.gameData.playerAmount)
        {
            whatPlayerShowing = 0;
        }
        MoveCamera(whatPlayerShowing);
    }

    int whatPlayerShowing = 0;
    public int testMove = 3;
    public void CreateElements(int playerAmount)
    {
        xSize = oneGUIelement.GetComponent<RectTransform>().rect.width;
        Vector3 positionGoer = oneGUIelement.transform.position;
        for(int n=0; n<playerAmount; n++)
        {
            GameObject createdObject=(GameObject)Instantiate(oneGUIelement, positionGoer, Quaternion.identity);
            createdElements.Add(createdObject);
            positionGoer.x += xSize;
        }
    }
    public bool loadingIMage = false;
    public int playerNumberLoading = -1;
    public int intGoer = 0;
    public Camera UIcamera;
    bool movingCamera = false;
    int newCameraloc;
    Vector3 startLoc;
    Vector3 endLoc;
    float moveStartTime;
    float moveDuration = 1f;

    public void LoadAllImagesFromPlayerURLS()
    {
        StartCoroutine(LoadAllImages());
    }

    IEnumerator LoadAllImages()
    {

        foreach(GameObject element in createdElements)
        {
            LoadOneImage(intGoer);
            while (loadingIMage)
            {
                yield return new WaitForSeconds(Time.smoothDeltaTime);
            }
            intGoer++;
        }
    }

    public void LoadOneImage(int playerNumber)
    {
        loadingIMage = true;
        playerNumberLoading = playerNumber;
        string url = Common.roundInformation.GetPlayerURL(playerNumber);
        Common.getImageFromURL.loadImage(url, this.gameObject, "ImageLoaded");
    }

    void ImageLoaded(Texture2D loadedImage)
    {
        SetImageToElement(createdElements[playerNumberLoading], loadedImage);
        loadingIMage = false;
    }

    void SetImageToElement(GameObject element,Texture2D result)
    {
        Image img = Common.FindPickedImageFromChild(element);
        Common.SetTexture2DToImage(img, result);
    }

    void MoveCamera(int toWhere)
    {
        newCameraloc = toWhere;
        startLoc = UIcamera.transform.position;
        endLoc = createdElements[toWhere].gameObject.transform.position;
        moveStartTime = Time.time;
        movingCamera = true;
    }
}



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class ShowWinnerMaster : MonoBehaviour {
    public GameObject oneGUIelement;
    public List<GameObject> createdElements;
    public List<string> createdElementsLoadedImagesURLS;
    public float xSize = 10f;
    bool lookedEveryImage = false;
    public int whatPlayerAmountTouse = 4;
    public bool useRealPlayerAmount = false;
    
	// Use this for initialization
	void Start () {
        Common.showWinnerMaster = this;
        //Invoke("TestCreate",2f);
    }

    void TestCreate()
    {
        CreateElements(whatPlayerAmountTouse);
    }

    bool CheckIfActive()
    {
        GameStates deb = (GameStates)Common.roundInformation.gameData.gameState;
      //  Debug.Log(deb);
        if (deb == GameStates.PickingWinner)
            return true;
        return false;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateElements(whatPlayerAmountTouse);//Common.roundInformation.gameData.playerAmount);
            MoveCamera(testMove);

        }

        if (CheckIfActive())
        {
            if (createdElements!=null)
            {
                Common.DebugLog("Createdsize", "SHowwinnerMaster", "count is " + createdElements.Count.ToString());
            }
            if (useRealPlayerAmount)
            {
                whatPlayerAmountTouse = Common.roundInformation.gameData.playerAmount;
            }
            else
            {
                whatPlayerAmountTouse = 3;
            }
            if (movingCamera)
            {
                float elapsed = Time.time - moveStartTime;
                if (moveDuration > elapsed)
                {
                    float zPos = UIcamera.transform.position.z;
                    Vector3 newPos = Vector3.Lerp(startLoc, endLoc, elapsed / moveDuration);
                    newPos.z = zPos;
                    UIcamera.transform.position = newPos;
                }
                if (whatPlayerShowing >= whatPlayerAmountTouse)//Common.roundInformation.gameData.playerAmount)
                {
                    lookedEveryImage = true;
                }
            }
           // Debug.Log("here");
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(LoadAllImages());

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log("Left");
                SwipeLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwipeRight();
            }
        }
    }

    public void SwipeBroadCastLeft()
    {
        Debug.Log("Got broadcasted swipe left");
        SwipeLeft();
    }

    public void SwipeBroadCastRight()
    {
        Debug.Log("Got broadcasted swipe right");
        SwipeRight();
    }

    void SwipeLeft()
    {
        if (CheckIfActive())
        {
            whatPlayerShowing--;
            if (whatPlayerShowing < 0)
            {
                whatPlayerShowing = whatPlayerAmountTouse - 1; //Common.roundInformation.gameData.playerAmount-1;
            }
            MoveCamera(whatPlayerShowing);
        }
    }

    void SwipeRight()
    {
        if (CheckIfActive())
        {
            whatPlayerShowing++;
            if (whatPlayerShowing >= whatPlayerAmountTouse) //Common.roundInformation.gameData.playerAmount)
            {
                whatPlayerShowing = 0;
            }
            MoveCamera(whatPlayerShowing);
        }
    }

    void SetElementName(int playerNumber)
    {
        GameObject nameTextGO = Common.FindGameObjectInChildWithTag(createdElements[playerNumber], frameNameTag);
        Text nameText = nameTextGO.GetComponent<Text>();
        nameText.text = "Loaded " + playerNumber.ToString();
    }

    int whatPlayerShowing = 0;
    public int testMove = 3;
    string frameNameTag = "FrameName";
    public void CreateElements(int playerAmount)
    {
        oneGUIelement.SetActive(false);   
        int currentAmount = createdElements.Count;
        int difference = currentAmount - playerAmount;
        int howManyNeededMore = 0;
        if (currentAmount != 0)
        {
            Debug.Log("Elements have already been made Remove or increase amount");
            if (difference > 0)
            {
                for (int n = playerAmount; n < currentAmount; n++)
                {
                    Destroy(createdElements[n]);
                }
                for (int n = playerAmount; n < currentAmount; n++)
                {
                    createdElements.RemoveAt(playerAmount);
                    createdElementsLoadedImagesURLS.RemoveAt(playerAmount);
                }
                return;
            }
            if (difference == 0)
            {
                return;
            }
            if (difference < 0)
            {
                howManyNeededMore = Mathf.Abs(difference);
            }
        }
        Common.DebugPopUp("Creating elements amount of players " + playerAmount.ToString());
        //xSize = oneGUIelement.GetComponent<RectTransform>().rect.width;
        Vector3 positionGoer = oneGUIelement.transform.position;
        Vector2 recPosGoer = oneGUIelement.GetComponent<RectTransform>().anchoredPosition;
        xSize = oneGUIelement.GetComponent<RectTransform>().rect.width;
        for (int n=0; n<playerAmount; n++)
        {
            if (howManyNeededMore == 0 || currentAmount <= n)
            {
                GameObject createdObject = (GameObject)Instantiate(oneGUIelement, positionGoer, Quaternion.identity);
                createdObject.SetActive(true);
                createdElements.Add(createdObject);
                createdElementsLoadedImagesURLS.Add("");
                
                RectTransform createdRect = createdObject.GetComponent<RectTransform>();
                createdRect.anchoredPosition = recPosGoer;
                createdObject.transform.localScale = oneGUIelement.transform.localScale;

                createdObject.transform.position = new Vector3(createdObject.transform.localPosition.x, createdObject.transform.localPosition.y, oneGUIelement.transform.localPosition.z);
                createdObject.transform.SetParent(Common.gameLoader.PickWinnerUI.transform, false);
                //createdObject.transform.SetParent(Common.gameLoader.ShowWinnerUI.transform);


                GameObject nameTextGO = Common.FindGameObjectInChildWithTag(createdObject, frameNameTag);
                Text nameText = nameTextGO.GetComponent<Text>();
                nameText.text = "UnAssigned " + n.ToString();
            }
            Debug.Log("goer on " + recPosGoer.x.ToString()+ "n on "+n.ToString()+" hmn "+howManyNeededMore.ToString());
                positionGoer.x += xSize;
                recPosGoer.x += xSize;
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

    public Image pickerChoiceImage;
    public List<Image> voteWinnerImage;

    public void LoadAllImagesFromPlayerURLS()
    {
        StartCoroutine(LoadAllImages());
    }

    public void SetUpShowWinnerScene(int hostChoice,List<int> voteWinners)
    {
        string urlToGet = Common.roundInformation.gameData.roundImageURLs[hostChoice];
        if (checkIfImagesAlreadyLoaded(urlToGet))
        {
            //URL has already been loaded to elements, so we don't have to load it again.
            pickerChoiceImage.sprite = FindSpriteFromElement(hostChoice);
            int counter = 0;
            foreach (int voteWinner in voteWinners)
            {
                voteWinnerImage[counter].sprite = FindSpriteFromElement(voteWinner);
                //GameObject newImage = (GameObject)Instantiate(voteWinnerImage.gameObject);
                //newImage.GetComponent<Image>().sprite = FindSpriteFromElement(voteWinner);
                counter++;
            }
        }
        else
        {
            //no iamges loaded already lets load sprites again.
            Debug.Log("Starting to load images");
            StartCoroutine(loadShowWinnerImages(hostChoice, voteWinners));
        }

        //Common.SetTexture2DToImage(img, result);
    }

    bool checkIfImagesAlreadyLoaded(string ulrToGet)
    {
        Debug.Log("checking if images already loaded on show winner");
        if (createdElementsLoadedImagesURLS!=null)
        {
            if (createdElementsLoadedImagesURLS.Contains(ulrToGet))
            {
                Debug.Log("Images already loaded, no need to load them again");
                return true;
            }
        }
        Debug.Log("images haven't been loaded");
        return false;
    }

    IEnumerator loadShowWinnerImages(int hostChoice,List<int> voteWinners)
    {

        string urlToGet = Common.roundInformation.gameData.roundImageURLs[hostChoice];
        int amount = 1+voteWinners.Count;
        for(int n=0; n<amount; n++)
        {
            Debug.Log("LoadAllimages on showWinner index loading " + n.ToString());
            LoadOneShowWinnerImage(n,hostChoice,voteWinners);
            while (loadingIMage)
            {
                yield return new WaitForSeconds(Time.smoothDeltaTime);
            }
        }
    }

    int whatShowWinnerLoading = -1; //-1 not setted, 0 host pick, 1 vote first, 2 vote second etc...
    void LoadOneShowWinnerImage(int whatLoading,int hostChoice, List<int> voteWinners)
    {
        loadingIMage = true;
        playerNumberLoading = whatLoading;
        string ulrToLoad = "";
        switch (whatLoading)
        {
            case 0:
                ulrToLoad= Common.roundInformation.gameData.roundImageURLs[hostChoice];
                break;
            default:
                int index = whatLoading - 1;
                ulrToLoad = Common.roundInformation.gameData.roundImageURLs[voteWinners[index]];
                break;
        }
       Common.getImageFromURL.loadImage(ulrToLoad, this.gameObject, "ShowWinnerImageLoaded");
    }

    public void ShowWinnerImageLoaded(Texture2D loadedImage)
    {
        Image toSetup;
        switch (playerNumberLoading)
        {
            case 0:
                toSetup = pickerChoiceImage;
                break;
            default:
                toSetup = voteWinnerImage[playerNumberLoading - 1];
                //ulrToLoad = Common.roundInformation.gameData.roundImageURLs[voteWinners[index]];
                break;
        }
        Debug.Log("Setting image to " + playerNumberLoading.ToString());
        Common.SetTexture2DToImage(toSetup, loadedImage);
        loadingIMage = false;
    }

    Sprite FindSpriteFromElement(int counter)
    {
        Image img = Common.FindPickedImageFromChild(createdElements[counter]);
        Sprite sprite = img.sprite;
        return sprite;
    }

    IEnumerator LoadAllImages()
    {
        intGoer = 0;
        foreach(GameObject element in createdElements)
        {
            Debug.Log("LoadAllimages index loading " + intGoer.ToString()+ "Created elements size is "+createdElements);
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
        Debug.Log("Getting player image at pid " + playerNumber.ToString());
        SetElementName(playerNumber);
        Common.getImageFromURL.loadImage(url, this.gameObject, "ImageLoaded");
    }

    public void FavouritePicked(GameObject whichElement)
    {
        int counter = 0;
        bool found = false;
        foreach(GameObject go in createdElements)
        {
            if (whichElement == go)
            {
                found = true;
                break;
            }
            counter++;
        }
        if (found)
        {
            Debug.Log("Found element next lets disable old ones");
            int pickedWinner = counter;
            int oldPickedNumber = Common.playerInformation.myVote; //Common.roundInformation.gameData.winnerVotes[Common.playerInformation.playerNumber];
            if (oldPickedNumber != -1) //meaning the old is not -1, meaning it has been selected already.
            {
                SetFavouritePickedGraphics(oldPickedNumber,false);
            }
            //Common.roundInformation.gameData.winnerVotes[Common.playerInformation.playerNumber] = pickedWinner;
            Common.playerInformation.myVote = pickedWinner;
            SetFavouritePickedGraphics(pickedWinner, true);

        }
        else
        {
            Common.DebugPopUp("ERROR Favourite picked and elements does not have pressed element FavouritePIcked on showwinnermaster");
        }

    }
    public GameObject foundWinnerGraphic;
    void SetFavouritePickedGraphics(int playerNumber,bool onOff)
    {
        Debug.Log("Setting picked graphics " + playerNumber);
        GameObject go = createdElements[playerNumber];
        GameObject findGraphic = Common.FindGameObjectInChildWithTagGOTroughAll(go, "WinnerPickedGraphic");
        foundWinnerGraphic = findGraphic;
        //Image winnerGraphic=go.FindComponentInChildWithTag<Image>("WinnerPickedGraphic");
        foundWinnerGraphic.GetComponent<Image>().enabled = onOff;//winnerGraphic.enabled = onOff;

    }

    void ImageLoaded(Texture2D loadedImage)
    {
        SetImageToElement(createdElements[playerNumberLoading], loadedImage);
        createdElementsLoadedImagesURLS[playerNumberLoading]= Common.roundInformation.GetPlayerURL(playerNumberLoading);

        loadingIMage = false;
    }

    void SetImageToElement(GameObject element,Texture2D result)
    {
        Image img = Common.FindPickedImageFromChild(element);
        Common.SetTexture2DToImage(img, result);
    }



    void MoveCamera(int toWhere)
    {
        Debug.Log("Moving camera to int " + toWhere.ToString());
        newCameraloc = toWhere;
        startLoc = UIcamera.transform.position;
        Debug.Log("Created elements size is  " + createdElements.Count.ToString());
        endLoc = createdElements[toWhere].gameObject.transform.position;
        moveStartTime = Time.time;
        movingCamera = true;
    }
}



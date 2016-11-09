using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InfoGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Common.infoGUI)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Common.infoGUI = this;
        }
        readyButton.interactable = false;
	}

    public void SetImage(Texture2D result)
    {
        // pickResult.material.mainTexture = result;
        Common.SetTexture2DToImage(pickResult, result);
    }
    public void SetProfilePic(Texture2D profilePic)
    {
        Common.SetTexture2DToImage(playerIcon,profilePic);
    }

    public void SetTexture2DToImage(Image img, Texture2D result)
    {
        Rect rec = new Rect(0, 0, result.width, result.height);
        img.sprite = Sprite.Create(result, rec, new Vector2(0.5f, 0.5f), 100);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public Image pickResult;
    public Text guestionText;
    public Toggle readyButton;

    public Image playerIcon;

    public void setNewGuestion(string newGuestion)
    {
        guestionText.text = newGuestion;
    }

    public void ReadyButtonToInteractable()
    {
        readyButton.interactable = true;
    }

    public void ReadyButtonToUnInteractable()
    {
        readyButton.interactable = false;
    }

    public void DisableReadyPressing()
    {
        Debug.Log("DISABLING READY PRESSING");
        readyButton.enabled = false;
    }

    public void AllowReadyPressing()
    {
        readyButton.enabled = true;
    }
    public void SetReadyToggle(bool value)
    {
        readyButton.isOn = value;
    }
}

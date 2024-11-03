using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject button;
    public GameObject pinButton;
    public GameObject logoImage;

    private void Start()
    {
        pinButton.SetActive(false);
    }
    public void DisableButton()
    {
        button.SetActive(false); 
    }

    public void EnablePinButton()
    {
        pinButton.SetActive(true);
    }

    public void EnableLogoImage() 
    {
        logoImage.SetActive(true);
    }
    
}

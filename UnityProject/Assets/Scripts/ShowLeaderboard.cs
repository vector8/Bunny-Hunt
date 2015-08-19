using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowLeaderboard : MonoBehaviour
{
    public Button signOut;

    private GameObject gp;
    private Googleplay gpScript;
    private Text signButtonText;
    // Use this for initialization
    void Start()
    {
        gp = GameObject.Find("GooglePlay");
        gpScript = gp.GetComponent<Googleplay>();
        bool status = gpScript.ReturnSignInStatus();
        signButtonText = signOut.transform.FindChild("Text").GetComponent<Text>();
        if (status == true)
        {
            signButtonText.text = "Signed Out";
        }
        else
        {
            signButtonText.text = "Signed In";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void showLeaderboard()
    {
        gp = GameObject.Find("GooglePlay");
        gpScript = gp.GetComponent<Googleplay>();
        gpScript.ShowLeaderboard();

    }

    public void signInOrOut()
    {
        gp = GameObject.Find("GooglePlay");
        gpScript = gp.GetComponent<Googleplay>();
        bool status = gpScript.ReturnSignInStatus();
        signButtonText = signOut.transform.FindChild("Text").GetComponent<Text>();
        if (status == true)
        {
            gpScript.SignOut();
            signButtonText.text = "Signed Out";
        }
        else
        {
            gpScript.SignIn();
            signButtonText.text = "Signed In";
        }
    }

}

using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Googleplay : MonoBehaviour
{
    private string leaderboardIDDay = "CgkI9raG460ZEAIQAQ";
    private string leaderboardIDCarrot = "CgkI9raG460ZEAIQDA";
    private string leaderboardIDHunter = "CgkI9raG460ZEAIQDQ";

    public static Googleplay googlePlayObject;
    public int[] daysSurvivedReqs = { 5, 10, 15, 20, 25, 50 };
    public string[] daysSurvivedIDs = { "CgkI9raG460ZEAIQBw", "CgkI9raG460ZEAIQCA", "CgkI9raG460ZEAIQCQ", "CgkI9raG460ZEAIQEA", "CgkI9raG460ZEAIQEQ", "CgkI9raG460ZEAIQEg" };
    public int[] huntersEatenReqs = { 25, 50, 100, 150, 200, 500 };
    public string[] huntersEatenIDs = { "CgkI9raG460ZEAIQEw", "CgkI9raG460ZEAIQFA", "CgkI9raG460ZEAIQCg", "CgkI9raG460ZEAIQCw", "CgkI9raG460ZEAIQFg", "CgkI9raG460ZEAIQFQ" };
    public int[] carrotsEatenReqs = { 10, 20, 30, 40, 50, 100 };
    public string[] carrotsEatenIDs = { "CgkI9raG460ZEAIQDg", "CgkI9raG460ZEAIQDw", "CgkI9raG460ZEAIQFw", "CgkI9raG460ZEAIQGA", "CgkI9raG460ZEAIQGQ", "CgkI9raG460ZEAIQGg" };

    private bool signInStatus = false;
    // Use this for initialization
    void Awake()
    {
        if (googlePlayObject == null)
        {
            googlePlayObject = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login successful");
                signInStatus = true;
            }
            else
            {
                Debug.Log("Login failed");
                signInStatus = false;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SignIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login successful");
                signInStatus = true;
            }
            else
            {
                Debug.Log("Login failed");
                signInStatus = false;
            }
        });

    }
    public void SignOut()
    {
        // sign out
        PlayGamesPlatform.Instance.SignOut();
        signInStatus = false;
    }

    //return player sign in status
    public bool ReturnSignInStatus()
    {
        return signInStatus;
    }

    // post day survived to leaderboard
    public void PostDaySurvived(int day)
    {
        Social.ReportScore(day, leaderboardIDDay, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Day survived posted");
            }
            else
            {
                Debug.Log("posting Day survived failed");
            }
        });
    }

    //post carots eaten to leaderboard
    public void PostCarrotEaten(int carrot)
    {
        Social.ReportScore(carrot, leaderboardIDCarrot, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Carrot eaten posted");
            }
            else
            {
                Debug.Log("posting Carrot eaten failed");
            }
        });
    }

    //post hunters eaten to leaderboard
    public void PostHunterEaten(int hunter)
    {
        Social.ReportScore(hunter, leaderboardIDHunter, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Hunter eaten posted");
            }
            else
            {
                Debug.Log("posting Hunter eaten failed");
            }
        });
    }

    //show leaderboard main menu 
    public void ShowLeaderboard()
    {
        //PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9raG460ZEAIQAQ");
        if (signInStatus == true)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            SignIn();
        }

    }
    public void ShowAchievements()
    {
        //PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI9raG460ZEAIQAQ");
        if (signInStatus == true)
        {
            Social.ShowAchievementsUI();
        }
        else
        {
            SignIn();
        }

    }
    public void DaysAchievement(int days)
    {
        if (signInStatus == true)
        {
            for(int i = 0; i < 6; i++)
            {
                if(days >= daysSurvivedReqs[i])
                {
                    Social.ReportProgress(daysSurvivedIDs[i], 100.0f, (bool success) =>
                    {
                        if (success)
                        {
                            Debug.Log("posted day " + i + " achievement");
                        }
                        else
                        {
                            Debug.Log("posted day " + i + " achievement failed");
                        }
                    });

                    if(i < 5)
                    {
                        Social.ReportProgress(daysSurvivedIDs[i+1], 0.0f, (bool success) =>
                        {
                            if (success)
                            {
                                Debug.Log("reveal day " + (i + 1) + " achievement");
                            }
                            else
                            {
                                Debug.Log("reveal day " + (i + 1) + " achievement failed");
                            }
                        });
                    }
                }
            }
        }
    }

    public void CarrotsAchievement(int carrots)
    {
        if (signInStatus == true)
        {
            for (int i = 0; i < 6; i++)
            {
                if (carrots >= carrotsEatenReqs[i])
                {
                    Social.ReportProgress(carrotsEatenIDs[i], 100.0f, (bool success) =>
                    {
                        if (success)
                        {
                            Debug.Log("posted carrot " + i + " achievement");
                        }
                        else
                        {
                            Debug.Log("posted carrot " + i + " achievement failed");
                        }
                    });

                    if (i < 5)
                    {
                        Social.ReportProgress(carrotsEatenIDs[i + 1], 0.0f, (bool success) =>
                        {
                            if (success)
                            {
                                Debug.Log("reveal carrot " + (i + 1) + " achievement");
                            }
                            else
                            {
                                Debug.Log("reveal carrot " + (i + 1) + " achievement failed");
                            }
                        });
                    }
                }
            }
        }
    }

    public void HunterAchievement(int hunters)
    {
        if (signInStatus == true)
        {
            for (int i = 0; i < 6; i++)
            {
                if (hunters >= huntersEatenReqs[i])
                {
                    Social.ReportProgress(huntersEatenIDs[i], 100.0f, (bool success) =>
                    {
                        if (success)
                        {
                            Debug.Log("posted hunter " + i + " achievement");
                        }
                        else
                        {
                            Debug.Log("posted hunter " + i + " achievement failed");
                        }
                    });

                    if (i < 5)
                    {
                        Social.ReportProgress(huntersEatenIDs[i + 1], 0.0f, (bool success) =>
                        {
                            if (success)
                            {
                                Debug.Log("reveal hunter " + (i + 1) + " achievement");
                            }
                            else
                            {
                                Debug.Log("reveal hunter " + (i + 1) + " achievement failed");
                            }
                        });
                    }
                }
            }
        }
    }
}

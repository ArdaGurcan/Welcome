using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManagement : MonoBehaviour
{
    public SimplePlatformController player;
    int score;
    public Text right;
    public Text left;
    public Text yourScore;
    public Highscore[] highscoresList;
    string playerUsername;

    const string privateKey = "FNHUhxQjK0qfmw96kRzwIA83FQ4ns8gkiWRH7ODVhbWA";
    const string publicKey = "5cc615d53eba951290fcd586";
    const string webURL = "http://dreamlo.com/lb/";

    public void CalculateScores()
    {
        Debug.Log(playerUsername);
        if(!string.IsNullOrEmpty(playerUsername))
        {
            UploadScore(playerUsername, 200 + 80 * player.coinCount);

        }
        else
        {

            yourScore.text = "Your Score: " + (200 + 80 * player.coinCount);
            DownloadScores();

        }
    }

    public void SetUsername(string username)
    {
        playerUsername = username;
    }

    void Update()
    {
        
    }

    public void UploadScore(string username,int highscore)
    {
        yourScore.text = "Your Score: " + highscore;
        StartCoroutine(UploadHighScore(username, highscore));

    }

    public void DownloadScores()
    {
        StartCoroutine("DownloadHighScores");
    }

    IEnumerator UploadHighScore(string username, int highscore)
    {
        WWW www = new WWW(webURL + privateKey + "/add/" + WWW.EscapeURL(username) + "/" + highscore);
        yield return www;

        if(string.IsNullOrEmpty(www.error))
        {
            print("Upload Successfull");
        }
        else
        {
            print("Error uploading - " + www.error);
        }

        DownloadScores();
    }

    IEnumerator DownloadHighScores()
    {
        WWW www = new WWW(webURL + publicKey + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print(www.text);
            FormatScores(www.text);
        }
        else
        {
            print("Error downloading - " + www.error);
        }
    }

    void FormatScores(string textStream) {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int userscore = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, userscore);
            print(highscoresList[i].username + " : " + highscoresList[i].score);
        }
        WriteHighScores();
    }

    void WriteHighScores()
    {
        if(highscoresList.Length > 0)
        {
            left.text = "";
            for (int i = 0; i < 5; i++)
            {
                print("1.i = " + i);
                if(highscoresList.Length > i)
                    left.text += i+1 + ". " + highscoresList[i].username + ": " + highscoresList[i].score + ((i == 4) ? "" : "\n\n");
            }
        }

        if (highscoresList.Length > 5)
        {
            right.text = "";
            for (int i = 5; i < 10; i++)
            {
                print("2.i = " + i);
                if (highscoresList.Length > i)
                    right.text += i+1 + ". " + highscoresList[i].username + ": " + highscoresList[i].score + ((i == 9) ? "" : "\n\n");
            }
        }
    }
}

public struct Highscore {
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}

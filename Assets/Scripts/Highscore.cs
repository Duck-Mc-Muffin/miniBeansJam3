using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SimpleJSON;

public static class HighScore
{
    private const string API_ENDPOINT = "https://grubstitute.highscore.kaycon.biz/api";

    public struct Score
    {
        public string Username { get; set; }
        public int Points { get; set; }
        public int Position { get; set; }
    }

    /// <summary>
    /// Get all highscores.
    /// </summary>
    /// <exception cref="WebException">Unable to fetch score data</exception>
    /// <returns>All Highscores</returns>
    public static IEnumerable<Score> GetHighscores()
    {
        var scores = new List<Score>();
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_ENDPOINT);
        httpWebRequest.ContentType = "application/json; charset=utf-8";
        httpWebRequest.Method = "GET";

        // Temp custom certificate trust.
        var prevCb = ServicePointManager.ServerCertificateValidationCallback;
        ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallback;

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        ServicePointManager.ServerCertificateValidationCallback = prevCb;
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var node = JSON.Parse(result);
            foreach (var s in node["scores"])
            {
                scores.Add(new Score
                {
                    Position = s.Value["position"],
                    Points = s.Value["score"],
                    Username = s.Value["name"]
                });
            }
        }
        return scores;
    }

    /// <summary>
    /// Upload a new score.
    /// </summary>
    /// <param name="token">Auth Token</param>
    /// <param name="score">Score to send</param>
    /// <param name="username">Chosen username</param>
    /// <exception cref="WebException">Failed to send data</exception>
    /// <exception cref="Exception">Failed to parse JSON</exception>
    /// <returns></returns>
    public static Score AddScore(string token, int score, string username = "Anonymous")
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(API_ENDPOINT);
        httpWebRequest.ContentType = "application/json; charset=utf-8";
        httpWebRequest.Method = "PUT";
        httpWebRequest.PreAuthenticate = false;
        httpWebRequest.PreAuthenticate = true;
        httpWebRequest.Headers.Set("Authorization", "Bearer " + token);

        // Temp custom certificate trust.
        var prevCb = ServicePointManager.ServerCertificateValidationCallback;
        ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallback;

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            var o = new JSONObject();
            o.Add("score", new JSONNumber(score));
            o.Add("username", new JSONString(username));
            string json = o.ToString();

            streamWriter.Write(json);
            streamWriter.Flush();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        ServicePointManager.ServerCertificateValidationCallback = prevCb;
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            var node = JSON.Parse(result);

            return new Score
            {
                Position = node["position"],
                Points = node["score"],
                Username = node["name"]
            };
        }
    }

    private static bool CertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain,
        // look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status == X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    continue;
                }
                chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                bool chainIsValid = chain.Build((X509Certificate2)certificate);
                if (!chainIsValid)
                {
                    isOk = false;
                    break;
                }
            }
        }
        return isOk;
    }
}
﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Http {

    public delegate void HttpCallback(HttpResult result);
    
    public class HttpManager {

        private static readonly string URL = "http://128.199.63.222/v1/users";

        public static IEnumerator getUsers(HttpCallback callback) {
            
            UnityWebRequest req = UnityWebRequest.Get(URL);
            req.SetRequestHeader("Content-Type", "application/json");
            
            yield return req.SendWebRequest();

            if (req.error != null) {
                Debug.Log(req.error);
            } else {
                string json = req.downloadHandler.text;

                HttpResult result = JsonUtility.FromJson<HttpResult>(json);
                callback(result);
            }
            
            

        }
        
        
    }
    
    
}
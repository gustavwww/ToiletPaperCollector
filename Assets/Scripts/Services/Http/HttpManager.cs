using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Http {

    public class HttpManager {
        
        private static readonly string URL = "localhost://api/v1/users";

        public static IEnumerator getUsers() {
            
            UnityWebRequest req = UnityWebRequest.Get(URL);
            req.SetRequestHeader("Content-Type", "application/json");
            yield return req.SendWebRequest();

            if (req.error != null) {
                Debug.Log(req.error);
            } else {
                string json = req.downloadHandler.text;

                HttpResult result = JsonUtility.FromJson<HttpResult>("{\"users\":" + json + "}");
                //TODO: Impl.
            }
            
            

        }
        
        
    }
    
    
}
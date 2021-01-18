
using System;
using Services.Http;
using UnityEngine;

namespace View {

    public class LeaderBoardView : MonoBehaviour {


        public GameObject ContentPanel;
        public GameObject ListItem;
        
        public void loadLeaderBoard() {
            HttpCallback callback = new HttpCallback(httpCallback);
            StartCoroutine(HttpManager.getUsers(callback));
        }

        private void httpCallback(HttpResult result) {
            
            Array.Sort(result.users, new Comparison<UserEntry>((x, y) => x.amount-y.amount));

            for (int i = 0; i < result.users.Length; i++) {
                var user = result.users[i];
                
                GameObject listItem = Instantiate(ListItem);
                LeaderBoardEntry entry = listItem.GetComponent<LeaderBoardEntry>();
                entry.nickname.text = user.nickname;
                entry.amount.text = user.amount.ToString();
                entry.place.text = i.ToString();
                listItem.transform.parent = ContentPanel.transform;
                listItem.transform.localScale = Vector3.one;
            }
            
        }
        
    }
    
}
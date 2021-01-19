
using System;
using Services.Http;
using UnityEngine;

namespace View {

    public class LeaderBoardView : MonoBehaviour {
        
        public GameObject TotalPanel;
        public GameObject WeeklyPanel;
        public GameObject ListItem;

        public void totalPressed() {
            TotalPanel.GetComponent<CanvasGroup>().alpha = 1;
            TotalPanel.GetComponent<CanvasGroup>().interactable = true;
            WeeklyPanel.GetComponent<CanvasGroup>().alpha = 0;
            WeeklyPanel.GetComponent<CanvasGroup>().interactable = false;
        }

        public void weeklyPressed() {
            TotalPanel.GetComponent<CanvasGroup>().alpha = 0;
            TotalPanel.GetComponent<CanvasGroup>().interactable = false;
            WeeklyPanel.GetComponent<CanvasGroup>().alpha = 1;
            WeeklyPanel.GetComponent<CanvasGroup>().interactable = true;
        }

        public void loadLeaderBoard() {
            HttpCallback callback = new HttpCallback(httpCallback);
            StartCoroutine(HttpManager.getUsers(callback));
        }

        private void httpCallback(HttpResult result) {
            createTotalTable(result);
            createWeeklyTable(result);
        }

        private void createTotalTable(HttpResult result) {
            
            Array.Sort(result.users, new Comparison<UserEntry>((x, y) => x.amount-y.amount));

            for (int i = 0; i < result.users.Length; i++) {
                var user = result.users[i];
                
                GameObject listItem = Instantiate(ListItem);
                LeaderBoardEntry entry = listItem.GetComponent<LeaderBoardEntry>();
                entry.nickname.text = user.nickname;
                entry.amount.text = user.amount.ToString();
                entry.place.text = (i+1).ToString();
                listItem.transform.parent = TotalPanel.transform;
                listItem.transform.localScale = Vector3.one;
            }
            
        }

        private void createWeeklyTable(HttpResult result) {
            
            Array.Sort(result.users, new Comparison<UserEntry>((x, y) => x.amount-y.amount));

            for (int i = 0; i < result.users.Length; i++) {
                var user = result.users[i];
                
                GameObject listItem = Instantiate(ListItem);
                LeaderBoardEntry entry = listItem.GetComponent<LeaderBoardEntry>();
                entry.nickname.text = user.nickname;
                entry.amount.text = user.weeklyAmount.ToString();
                entry.place.text = (i+1).ToString();
                listItem.transform.parent = WeeklyPanel.transform;
                listItem.transform.localScale = Vector3.one;
            }
            
        }
        
    }
    
}
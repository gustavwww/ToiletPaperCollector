
using System;
using Services.Http;
using UnityEngine;

namespace View {

    public class LeaderBoardView : MonoBehaviour {
        
        public GameObject TotalPanel;
        public GameObject WeeklyPanel;
        public CanvasGroup TotalScroll;
        public CanvasGroup WeeklyScroll;
        public GameObject ListItem;

        public void totalPressed() {
            TotalScroll.alpha = 1;
            TotalScroll.interactable = true;
            TotalScroll.blocksRaycasts = true;
            WeeklyScroll.alpha = 0;
            WeeklyScroll.interactable = false;
            WeeklyScroll.blocksRaycasts = false;
        }

        public void weeklyPressed() {
            TotalScroll.alpha = 0;
            TotalScroll.interactable = false;
            TotalScroll.blocksRaycasts = false;
            WeeklyScroll.alpha = 1;
            WeeklyScroll.interactable = true;
            WeeklyScroll.blocksRaycasts = true;
        }

        public void loadLeaderBoard() {
            HttpCallback callback = new HttpCallback(httpCallback);
            StartCoroutine(HttpManager.getUsers(callback));
        }

        private void httpCallback(HttpResult result) {
            foreach (Transform child in TotalPanel.transform) {
                Destroy(child.gameObject);
            }
            
            foreach (Transform child in WeeklyPanel.transform) {
                Destroy(child.gameObject);
            }

            createTotalTable(result);
            createWeeklyTable(result);
        }

        private void createTotalTable(HttpResult result) {
            
            Array.Sort(result.users, new Comparison<UserEntry>((x, y) => y.amount-x.amount));

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
            
            Array.Sort(result.users, new Comparison<UserEntry>((x, y) => y.amount-x.amount));

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
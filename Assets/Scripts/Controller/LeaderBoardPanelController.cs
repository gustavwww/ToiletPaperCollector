using System;
using Model;
using Services.Http;
using UnityEngine;
using View;

namespace Controller {
    public class LeaderBoardPanelController : MonoBehaviour {

        public GameModel gameModel;
        
        public GameObject totalContent;
        public GameObject weeklyContent;
        public CanvasGroup totalScroll;
        public CanvasGroup weeklyScroll;
        public GameObject listItem;
        public GameObject meListItem;

        public GameObject mainMenuPanel;

        public void totalPressed() {
            totalScroll.alpha = 1;
            totalScroll.interactable = true;
            totalScroll.blocksRaycasts = true;
            weeklyScroll.alpha = 0;
            weeklyScroll.interactable = false;
            weeklyScroll.blocksRaycasts = false;
        }

        public void weeklyPressed() {
            totalScroll.alpha = 0;
            totalScroll.interactable = false;
            totalScroll.blocksRaycasts = false;
            weeklyScroll.alpha = 1;
            weeklyScroll.interactable = true;
            weeklyScroll.blocksRaycasts = true;
        }
        
        public void loadLeaderBoard() {
            HttpCallback callback = new HttpCallback(httpCallback);
            StartCoroutine(HttpManager.getUsers(callback));
        }

        private void httpCallback(HttpResult result) {
            foreach (Transform child in totalContent.transform) {
                Destroy(child.gameObject);
            }
            
            foreach (Transform child in weeklyContent.transform) {
                Destroy(child.gameObject);
            }

            createTotalTable(result);
            createWeeklyTable(result);
        }

        private void createTotalTable(HttpResult result) {
            
            Array.Sort(result.users, new Comparison<UserEntry>((u1, u2) => u2.amount-u1.amount));

            for (int i = 0; i < result.users.Length; i++) {
                var user = result.users[i];
                
                GameObject lItem = Instantiate(listItem);
                if (user.nickname.Equals(gameModel.getUser().getNickname())) {
                    lItem = Instantiate(meListItem);
                }
                
                LeaderBoardEntry entry = lItem.GetComponent<LeaderBoardEntry>();
                entry.nickname.text = user.nickname;
                entry.amount.text = user.amount.ToString();
                entry.place.text = (i+1).ToString();
                lItem.transform.parent = totalContent.transform;
                lItem.transform.localScale = Vector3.one;
            }
            
        }
        
        private void createWeeklyTable(HttpResult result) {
            
            Array.Sort(result.users, new Comparison<UserEntry>((u1, u2) => u2.weeklyAmount-u1.weeklyAmount));

            for (int i = 0; i < result.users.Length; i++) {
                var user = result.users[i];
                
                GameObject lItem = Instantiate(listItem);
                if (user.nickname.Equals(gameModel.getUser().getNickname())) {
                    lItem = Instantiate(meListItem);
                }
                
                LeaderBoardEntry entry = lItem.GetComponent<LeaderBoardEntry>();
                entry.nickname.text = user.nickname;
                entry.amount.text = user.weeklyAmount.ToString();
                entry.place.text = (i+1).ToString();
                lItem.transform.parent = weeklyContent.transform;
                lItem.transform.localScale = Vector3.one;
            }
            
        }

        public void backPressed() {
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
        
    }
    
}
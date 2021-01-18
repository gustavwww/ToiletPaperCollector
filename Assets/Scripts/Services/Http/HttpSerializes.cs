using System;

namespace Services.Http {

    [Serializable]
    public class HttpResult {

        public UserEntry[] users;
    }

    [Serializable]
    public class UserEntry {

        public string nickname;
        public int amount;
        public int weeklyAmount;

    }
    
    
}
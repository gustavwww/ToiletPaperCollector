namespace Model {
    
    public class User {

        private readonly string nickname;
        private readonly int coins;
        private int amount;
        private int weeklyAmount;

        public User(string nickname, int coins, int amount, int weeklyAmount) {
            this.nickname = nickname;
            this.coins = coins;
            this.amount = amount;
            this.weeklyAmount = weeklyAmount;
        }

        public void addAmount(int amount) {
            this.amount += amount;
            weeklyAmount += amount;
        }

        public string getNickname() {
            return nickname;
        }

        public int getCoins() {
            return coins;
        }

        public int getAmount() {
            return amount;
        }

        public int getWeeklyAmount() {
            return weeklyAmount;
        }

    }
    
}
namespace Model {
    
    public class Level {

        private readonly int id;
        private readonly int requirement;
        private readonly int capacity;
        private readonly int spawnAmount;
        private readonly int boxIncrement;

        public Level(int id, int requirement, int capacity, int spawnAmount, int boxIncrement) {
            this.id = id;
            this.requirement = requirement;
            this.capacity = capacity;
            this.spawnAmount = spawnAmount;
            this.boxIncrement = boxIncrement;
        }

        public int getId() {
            return id;
        }

        public int getRequirement() {
            return requirement;
        }


        public int getCapacity() {
            return capacity;
        }

        public int getSpawnAmount() {
            return spawnAmount;
        }

        public int getBoxIncrement() {
            return boxIncrement;
        }

    }
    
    
}
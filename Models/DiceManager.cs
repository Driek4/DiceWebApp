namespace DiceWebApp.Models
{
    public class DiceManager
    {
        public List<Dice> DiceList { get; set; } = new();

        private static readonly Random random = new();

        public Dice Add(DiceType type)
        {
            var die = new Dice { Type = type, Location = "pool", Value = null };
            DiceList.Add(die);
            return die;
        }

        public void RemoveDice(int index)
        {
            if (index >= 0 && index < DiceList.Count)
                DiceList.RemoveAt(index);
        }

        public void RollAll()
        {
            foreach (var dice in DiceList)
                dice.Roll();
        }
    }
}

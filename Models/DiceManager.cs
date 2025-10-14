namespace DiceWebApp.Models
{
    public class DiceManager
    {
        public List<Dice> DiceList { get; set; } = new List<Dice>();

        public void AddDice(DiceType type = DiceType.Standard)
            => DiceList.Add(new Dice { Type = type, Value = 0 });
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

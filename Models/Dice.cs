namespace DiceWebApp.Models
{
    public class Dice //
    {
        public int Value { get; set; } = 1;
        private static readonly Random random = new Random();
        public void Roll()
        {
            Value = random.Next(1, 7);
        }
    }
}

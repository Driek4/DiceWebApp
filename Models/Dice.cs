namespace DiceWebApp.Models
{
    public enum DiceType
    {
        Standard,
        Bleed,
        Poison,
        Burn,
        Smite,
        Curse
    }
    public class Dice //
    {
        public DiceType Type { get; set; } = DiceType.Standard;
        public int Value { get; set; }
        public string Location { get; set; } = "pool";

        private static readonly Random random = new Random();
        public void Roll()
        {
            int[] faces = GetFaces(Type);
            Value = faces[random.Next(faces.Length)];
        }

        private static int[] GetFaces(DiceType type)
        {
            return type switch
            {
                DiceType.Bleed => new[] { 0, 1, 1, 2, 2, 3 },
                DiceType.Poison => new[] { 0, 0, 1, 3, 4, 4 },
                DiceType.Burn => new[] { 0, 1, 2, 2, 4, 6 },
                DiceType.Smite => new[] { 0, 1, 2, 6, 6, 6 },
                DiceType.Curse => new[] { 0, 1, 3, 3, 3, 10 },
                _ => new[] { 0, 1, 2, 3, 4, 5 } //Standard
            };
        }
    }
}

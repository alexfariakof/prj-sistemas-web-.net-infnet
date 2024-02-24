namespace Domain.Streaming.ValueObject
{
    public record Duration
    {
        public int Value { get; set; }
        public static implicit operator int(Duration d) => d.Value;
        public static implicit operator Duration(int value) => new Duration(value);

        public Duration(int value)
        {
            if (value < 0)
                throw new ArgumentException("Duração da musica não pode ser negativa");

            this.Value = value;
        }

        public String Formatted_ptBr()
        {
            int minutos = Value / 60;
            int segundos = Value % 60;

            return $"{minutos.ToString().PadLeft(2, '0')}:{segundos.ToString().PadLeft(2, '0')}";
        }
    }
}

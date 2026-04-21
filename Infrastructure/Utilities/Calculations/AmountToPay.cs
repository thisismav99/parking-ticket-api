namespace Infrastructure.Utilities.Calculations
{
    internal readonly struct AmountToPay
    {
        public double TotalHours { get; }
        public decimal Value { get; }

        private const decimal FlatRate = 50m;
        private const decimal HourlyRate = 20m;
        private const decimal OvernightRate = 500m;

        private const int HoursForFlatRate = 3;
        private const int HoursForOvernightRate = 12;

        public AmountToPay(double totalHours)
        {
            TotalHours = totalHours;
            Value = Calculate();
        }

        private decimal Calculate()
        {
            if (TotalHours <= HoursForFlatRate)
            {
                return FlatRate;
            }
            else if (TotalHours > HoursForFlatRate && TotalHours <= HoursForOvernightRate)
            {
                return FlatRate + HourlyRate * (decimal)(TotalHours - HoursForFlatRate);
            }
            else
            {
                return OvernightRate + HourlyRate * (decimal)(TotalHours - HoursForOvernightRate);
            }
        }
    }
}

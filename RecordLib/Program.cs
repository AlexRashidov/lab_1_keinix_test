namespace RecordLib
{
    public class Record
    {
        public string? TextField1 { get; set; }
        public string? TextField2 { get; set; }
        public double NumericField { get; set; }
        public bool BooleanField { get; set; }

        public override string ToString()
        {
            return String.Format("{0};{1};{2};{3}", TextField1, TextField2, NumericField, BooleanField);
        }
    }
}


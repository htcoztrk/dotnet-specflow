namespace TestAutomation.Framework.DomainLayer.Models.ValueObjects
{
    public class Interval
    {
        public double Length { get; private set; }

        public Interval(double length)
        {
            Length = length;
        }
    }
}

namespace Citaty.Core.Services
{
    public class ValueService : IValueService
    {
        public string[] GetValues()
        {
            return new[] {"1", "2", "3"};
        }
    }
}
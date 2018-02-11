namespace UserPayment.Models
{
    public interface IValidationDictionary
    {
        bool IsValid { get; }

        void AddError(string key, string errorMessage);        
    }
}

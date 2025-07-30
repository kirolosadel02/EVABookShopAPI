using FluentValidation.Results;

namespace EVABookShopAPI.Service.Extension
{
    public static class FluentValidationExtension
    {
        public static Dictionary<string, string[]> ToDictionary(this ValidationResult result)
        {
            return result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }
    }
}

namespace Common.Application.Validation
{
    public static class ValidationMessages
    {
        public const string Required = "This field is required";
        public const string InvalidPhoneNumber = "Phone number is invalid";
        public const string NotFound = "Requested data was not found";
        public const string MaxLength = "The number of entered characters exceeds the allowed limit";
        public const string MinLength = "The number of entered characters is less than the allowed limit";

        public static string required(string field) => $"{field} is required";
        public static string maxLength(string field, int maxLength) => $"{field} must be less than {maxLength} characters";
        public static string minLength(string field, int minLength) => $"{field} must be greater than {minLength} characters";
    }
}
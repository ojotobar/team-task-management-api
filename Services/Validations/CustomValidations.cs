namespace Services.Validations
{
    public class CustomValidations
    {
        public static bool BeAValidFutureDate(DateTime? date)
        {
            return date.HasValue && date.Value >= DateTime.UtcNow;
        }

        public static bool BeAValidFutureDate(DateTime date)
        {
            return date >= DateTime.UtcNow;
        }

        public static bool BeValidUserIds(List<string> userIds)
        {
            return userIds != null && userIds.Count > 0 && userIds.All(x => Guid.TryParse(x, out var _));
        }

        public static bool BeAValidUserId(string userId)
        {
            return Guid.TryParse(userId, out var _);
        }
    }
}

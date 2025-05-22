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
            var allAreValidIds = userIds.All(x => Guid.TryParse(x, out var _));
            return allAreValidIds;
        }

        public static bool BeAValidUserId(string userId)
        {
            return Guid.TryParse(userId, out var _);
        }
    }
}

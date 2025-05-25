namespace Shared
{
    public class ResponseMessages
    {
        public static readonly string RegistrationSuccessful = "User registration successful";
        public static readonly string GenericErrorMessage = "An error occurred while processing the request. Please try again shortly";
        public static readonly string WrongEmailOrPassword = "Wrong Email or Password. Please try again.";
        public static readonly string PasswordMustMatch = "Password and Confirm Password must match.";
        public static readonly string UserNotFound = "No user found";
        public static readonly string InvalidUserCredentials = "Invalid loggedin user credential.";
        public static readonly string TeamCreated = "Team successfully created";
        public static readonly string InvalidInput = "Your input is invalid. Please try again.";
        public static readonly string TeamRecordNotFound = "Team record not found";
        public static readonly string UsersInvited = "{0} user(s) successfully invited to the {1} team.";
        public static readonly string UsersAlreadyInTeam = "{0} user(s) already in the team. Please re-select and try again later.";
        public static readonly string TaskRecordNotFound = "Task record not found.";
        public static readonly string AlreadyInStatus = "Task already in '{0}' status. Please try another status.";
        public static readonly string TaskDeleted = "Task record successfully deleted";
        public static readonly string PermissionDenied = "User denied permission to carry out this action.";
        public static readonly string SomeUsersNotInTeam = "Task creation and assignment failed. {0} not in the {1} team.";
    }
}

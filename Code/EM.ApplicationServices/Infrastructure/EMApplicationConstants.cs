namespace EM.ApplicationServices.Infrastructure
{
    public static class EMApplicationConstants
    {
        public const string DuplicateUserName = "User name already exists. Please enter a different user name.";
        public const string InvalidPassword = "The password provided is invalid. Please enter a valid password value.";
        public const string InvalidUserName = "The user name provided is invalid. Please check the value and try again.";
        public const string Unknown = "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        public const string IncorrectUserName = "The user name provided is incorrect. Please check the value and try again.";
        public const string IncorrectPassword = "The password provided is incorrect. Please check the value and try again.";
        public const string ProfaneUserName = "Username not valid.";

        public const string InvalidNewOrOldPassword = "The current password is incorrect or the new password is invalid.";
        public const string PlayListEmpty = "The is no Play List in Cohort";
        public const string InvalidTaxonomy = "Taxonomy for the current request is not exist";
        public const string InvalidContentName = "The content name provided is invalid";
        public const string InvalidTaxonomyImplementation = "There is no Implementation for Taxonomy";
        public const string DuplicatePlayListName = "The playlist name you entered is already in use. Please choose a different name.";
        public const string DuplicateDlaName = "The Dla name you entered is already in use. Please choose a different name.";
        //public const string DuplicateUserDlaTitle = "The userdla title you entered is already in use. Please choose a different title.";
        public const string UserDlaNotExist = "We don't recognize this userdla. Please try again.";
        public const string AddToPlaylist = "We're sorry, there was a system error when saving your playlist. please try saving it again.";

        public const string InvalidUserNameAndBirthdate = "We don't recognize that username and birthday. Please try again. If you don't have an account, you can create one.";
        public const string InvalidEmailOrCellPhone = "No registered email address or cell phone number exist. Please contact PML administrator.";
        public const string InvalidEmail = "We don't recognize that email address. Please enter the email address you provided when you signed up.";
        public const string InvalidPhone = "The phone number you entered was not recognized. Please make sure to enter the phone number you used when you registered.";
        public const string InvalidToken = "The password reset link provided is invalid or expired. If you reset your password more than once, make sure you are using the latest reset link that you received by email, as a new link is issued every time you reset your password.";
        public const string InvalidParent = "The parent user name provided is incorrect. Please check the value and try again.";
        public const string InvalidEducator = "The educator user name provided is incorrect. Please check the value and try again.";
        public const string InvalidChild = "The child with provided details doesnt exist. Please check the value and try again.";
        //New exception message to add child with wrong details PML - 1815
        public const string InvalidChildCriteria = "We don't recognize that username and birthday. Please try again.";
        public const string ValidatePassword = "We don't recognize that password. Please try again.";
        public const string ValidateUserName = "We don't recognize that user. Please try again.";
        public const string InvalidClassCode = "That is not a valid class code. Please check the code and try again.";
        public const string InvalidStudentClass = "Student with the name {0} doesnt belong to the class. Please try again.";
        public const string StudentClassAlreadyExists = "Student already belongs to this class.";
        public const string StudentAlreadyExists = "Child already associated.";
        public const string PlayListNotExist = "We don't recognize this playlist.Please try again.";
        public const string InvalidFileType = "Invalid file type. Please choose a different file.";
        public const string InvalidFileSize = "Invalid file size. Please choose a file less than 2Mb in size.";
        public const string DuplicateEmailAddress = "Email address already exists. Please enter a different email address.";
        public const string InvalidEmailOrPhone = "We don't recognize that email address/cell phone number. Please enter the information you provided when you signed up.";
        public const string InvalidParentUser = "We don't recognize that username. Please try again. If you don't have an account, you can create one.";
        public const string InvalidUser = "PML user with username {0} not found";
        public const string AssignmentNotExist = "No Assignment exists for the playlist";

        public const string UserNotExist = "There is no PowerMyLearning user called <b>{0}</b>. Please try again.";
        public const string UserGameDisabled = "<b>{0}</b> is not currently using the gamification platform, so s/he can't accept your invitation.";
        public const string OtherUser = "We are sorry! Currently you can invite only student users.";
        public const string UserNameItself = "Oops! The user name you entered is your's.";
        public const string PendingChildApproval = "A request to connect with {0} has previously been sent. This request is still pending approval.";
    }
}
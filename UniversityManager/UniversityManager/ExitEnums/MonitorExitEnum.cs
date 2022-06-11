using System.ComponentModel;

namespace UniversityManager.ExitEnums
{
    public enum MonitorExitEnum
    {
        [Description("Operation succeeded.")]
        SUCCEEDED,
        [Description("ERROR: Something went wrong.")]
        ERROR_SOMETHING_WENT_WRONG,
        [Description("ERROR: User table is empty")]
        ERROR_USERS_IS_NULL,
        [Description("ERROR: User is not a student.")]
        ERROR_USER_IS_NOT_STUDENT,
        [Description("ERROR: UsersGroups table is empty.")]
        ERROR_USERS_GROUPS_IS_NULL,
        [Description("ERROR: Monitor hasn't permision for this student.")]
        ERROR_ACCESS_DENIED,
        [Description("ERROR: Subjects is empty.")]
        ERROR_SUBJECTS_IS_NULL,
        [Description("ERROR: This subject not set for this group.")]
        ERROR_SUBJECT_NOT_FOR_THIS_GROUP,
        [Description("ERROR: Log of passes is empty. Good group.")]
        ERROR_LOG_OF_PASSES_IS_NULL,
        [Description("ERROR: Groups is empty.")]
        ERROR_GROUPS_IS_NULL,
        [Description("ERROR: Date didn't choose.")]
        ERROR_DATE_NOT_CHOOSE,
        [Description("ERROR: This subject isn't valid.")]
        ERROR_SUBJECT_NOT_VALID,
        [Description("ERROR: Achieve types table is empty.")]
        ERROR_ACHIEVETYPE_IS_NULL
    }
}

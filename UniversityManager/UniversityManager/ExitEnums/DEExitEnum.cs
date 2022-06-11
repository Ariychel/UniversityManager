using System.ComponentModel;

namespace UniversityManager.ExitEnums
{
    public enum DEExitEnum
    {
        [Description("Operation succeeded.")]
        SUCCEEDED,
        [Description("ERROR: Something went wrong.")]
        ERROR_SOMETHING_WENT_WRONG,
        [Description("ERROR: This user isn't student.")]
        ERROR_USER_IS_NOT_STUDENT,
        [Description("ERROR: Table with users is empty.")]
        ERROR_USERS_IS_NULL,
        [Description("ERROR: Table with groups is empty.")]
        ERROR_GROUPS_IS_NULL,
        [Description("ERROR: UsersGroups is empty.")]
        ERROR_USERS_GROUPS_IS_NULL,
        [Description("ERROR: Table doesn't include this group.")]
        ERROR_TABLE_NOT_INCLUDE_GROUP,
        [Description("ERROR: Student already in group.")]
        ERROR_STUDENT_ALREADY_IN_GROUP,
        [Description("ERROR: Table with subjects is empty.")]
        ERROR_SUBJECTS_IS_NULL,
        [Description("ERROR: Table doesn't include this subject.")]
        ERROR_TABLE_NOT_INCLUDE_SUBJECT,
        [Description("ERROR: Subject for this group already added.")]
        ERROR_SUBJECT_GROUP_ALREADY_ADDED,
        [Description("ERROR: This user already added in database.")]
        ERROR_USER_ALREADY_CREATED,
        [Description("ERROR: This group already added.")]
        ERROR_THIS_GROUPS_ALREADY_ADDED,
        [Description("ERROR: Curator already added to this group.")]
        ERROR_CURATOR_ALREADY_ADDED

    }


}

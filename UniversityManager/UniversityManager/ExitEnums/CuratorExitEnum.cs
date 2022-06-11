using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityManager.ExitEnums
{
    public enum CuratorExitEnum
    {
        [Description("Operation succeded.")]
        SUCCEEDED,
        [Description("ERROR: Users table in empty.")]
        ERROR_USERS_IS_NULL,
        [Description("ERROR: User is not a student.")]
        ERROR_USER_IS_NOT_STUDENT,
        [Description("ERROR: UsersGroups table is empty")]
        ERROR_USERS_GROUPS_IS_NULL,
        [Description("ERROR: Curator hasn't permision for this student.")]
        ERROR_ACCESS_DENIED,
        [Description("ERROR: Subjects is empty.")]
        ERROR_SUBJECTS_IS_NULL,
        [Description("ERROR: This subject isn't valid.")]
        ERROR_SUBJECT_NOT_VALID,
        [Description("ERROR: This subject not set for this group.")]
        ERROR_SUBJECT_NOT_FOR_THIS_GROUP,
        [Description("ERROR: Something went wrong.")]
        ERROR_SOMETHING_WENT_WRONG
    }
}

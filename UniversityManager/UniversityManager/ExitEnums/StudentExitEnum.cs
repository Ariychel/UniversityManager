using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityManager.ExitEnums
{
    public enum StudentExitEnum
    {
        [Description("ERROR: Users table is empty.")]
        ERROR_USERS_IS_NULL,
        [Description("ERROR: Groups table is empty.")]
        ERROR_GROUPS_IS_NULL,
        [Description("ERROR: UsersGroups is epmty.")]
        ERROR_USERS_GROUPS_IS_NULL,
        [Description("ERROR: Log of passes is empty.")]
        ERROR_LOG_OF_PASSES_IS_NULL
    }
}

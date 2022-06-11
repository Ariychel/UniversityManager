using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UniversityManager.Structures;

namespace UniversityManager.Utils
{
    public static class Authenticator
    {
        public static User CheckAuthData(string userName, string password)
        {
            string query = "SELECT * FROM mydb.users";
            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.OpenConnection())
            {
                MySqlCommand command = new MySqlCommand(query, dataBaseConnector.Connection);

                MySqlDataReader dataReader = command.ExecuteReader();
                List<int> usersId = new List<int>();
                List<string> fullNames = new List<string>();
                List<string> userNames = new List<string>();
                List<string> passwords = new List<string>();
                List<string> eMails = new List<string>();
                List<string> phoneNumbers = new List<string>();
                List<int> usersType = new List<int>();

                while (dataReader.Read())
                {
                    usersId.Add(Convert.ToInt32(dataReader["idUsersTable"]));
                    fullNames.Add(Convert.ToString(dataReader["Full Name"]));
                    userNames.Add(Convert.ToString(dataReader["Username"]));
                    eMails.Add(Convert.ToString(dataReader["E-mail"]));
                    phoneNumbers.Add(Convert.ToString(dataReader["PhoneNumber"]));
                    usersType.Add(Convert.ToInt32(dataReader["UserType_idUserType"]));
                }
                dataReader.Close();

                List<int> lengthOfPasswords = new List<int>();
                for (int i = 0; i < usersId.Count; i++)
                {
                    lengthOfPasswords.Add(GetPasswordLength(usersId[i], dataBaseConnector.Connection));
                }

                #region ReadPasswords
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    byte[] rawData = new byte[lengthOfPasswords[Convert.ToInt32(dataReader["idUsersTable"]) - 1]];
                    dataReader.GetBytes(dataReader.GetOrdinal("Password"),
                        0, rawData, 0, rawData.Length);
                    passwords.Add(Encoding.ASCII.GetString(Protector.Unprotect(rawData)));
                }
                #endregion

                if (userNames.Contains(userName) && passwords.Contains(password))
                {
                    int userIndex = userNames.IndexOf(userName);
                    dataBaseConnector.CloseConnection();
                    return new User(usersId[userIndex], fullNames[userIndex], userNames[userIndex], passwords[userIndex],
                        eMails[userIndex], phoneNumbers[userIndex], usersType[userIndex]);

                }

                dataBaseConnector.CloseConnection();
                return null;
            }

            return null;
        }

        private static int GetPasswordLength(int rowId, MySqlConnection connection)
        {

            DataBaseConnector dataBaseConnector = new DataBaseConnector();
            string query = $"Select OCTET_LENGTH(Password) from `mydb`.`users` where idUsersTable = {rowId};";
            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = command.ExecuteReader();
            int length = 0;
            while (dataReader.Read())
            {
                length = Convert.ToInt32(dataReader["OCTET_LENGTH(Password)"]);
            }
            dataReader.Close();
            return length;
        }
    }
}

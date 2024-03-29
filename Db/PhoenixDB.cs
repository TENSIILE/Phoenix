﻿using System;
using System.Data.OleDb;
using Phoenix.Helpers;

namespace Phoenix.Db
{
    public class PhoenixDB
    {
        private static OleDbConnection _connection;
        private static string _selectedTable;

        public PhoenixDB(string stringConnection)
        {
            _connection = new OleDbConnection(stringConnection);
            _connection.Open();
        }

        public PhoenixDBCommonCommand InTable(string nameTable)
        {
            _selectedTable = nameTable;
            return new PhoenixDBCommonCommand();
        }

        public bool Auth(string login, string password)
        {
            string query = $@"SELECT count(*) FROM users WHERE login = '{login}' AND password = '{password}'";
            OleDbCommand command = new OleDbCommand(query, GetConnection);
            object response = command.ExecuteScalar();

            return !TypeMatchers.IsNull(response) && Convert.ToInt32(response) == 1;
        }

        public static OleDbConnection GetConnection => _connection;
        internal static string GetTable => _selectedTable;

        internal static int Execute(string query)
        {
            OleDbCommand command = new OleDbCommand(query, GetConnection);
            return command.ExecuteNonQuery();
        }
    }

    public class PhoenixDBCommonCommand
    {
        public PhoenixDML.PhoenixDBCommandSelect Select => new PhoenixDML.PhoenixDBCommandSelect();
        public PhoenixDML.PhoenixDBCommandUpdate Update => new PhoenixDML.PhoenixDBCommandUpdate();
        public PhoenixDML.PhoenixDBCommandDelete Delete => new PhoenixDML.PhoenixDBCommandDelete();
        public PhoenixDML.PhoenixDBCommandInsert Insert => new PhoenixDML.PhoenixDBCommandInsert();
    }

    public class PhoenixDBConstructor
    {
        protected string query;

        public int Exec()
        {
            return PhoenixDB.Execute(query);
        }
    }
}

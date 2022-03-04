using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.OleDb;
using Phoenix.Core;
using Phoenix.Extentions;

namespace Phoenix.Db
{
    public class PhoenixDML
    {
        public class PhoenixDBCommandDelete : PhoenixDBConstructor
        {
            public PhoenixDBCommandDelete()
            {
                query = $@"DELETE FROM {PhoenixDB.GetTable}";
            }

            public PhoenixDBCommandDelete Where(string conditions)
            {
                query += $@" WHERE {conditions}";
                return this;
            }
        }

        public class PhoenixDBCommandUpdate : PhoenixDBConstructor
        {
            public PhoenixDBCommandUpdate Set(string values)
            {
                query = $@"UPDATE {PhoenixDB.GetTable} SET {values}";
                return this;
            }

            public PhoenixDBCommandUpdate Where(string conditions)
            {
                query = $@" {query} WHERE {conditions}";
                return this;
            }
        }

        public class PhoenixDBCommandInsert : PhoenixDBConstructor
        {
            private List<string> _columns;

            private string ConvertingValues(List<string> values)
            {
                List<dynamic> result = new List<dynamic>();
                const int LENGTH_KEY = 3;

                for (int i = 0; i < values.Count; i++)
                {
                    string str = values[i].Trim();

                    double number;

                    if (double.TryParse(str, out number))
                    {
                        result.Add(number);
                    }
                    else
                    {
                        string code = str.Substring(0, LENGTH_KEY);

                        if (code == "[S]")
                        {
                            result.Add($@"'{str.Substring(LENGTH_KEY, str.Length - LENGTH_KEY).Trim()}'");
                            continue;
                        }

                        result.Add($@"'{str}'");
                    }
                }

                return result.Unite(",");
            }

            public PhoenixDBCommandInsert SetColumns(params string[] parameters)
            {
                _columns = parameters.ToList();
                return this;
            }

            public PhoenixDBCommandInsert SetValues(string values)
            {
                List<string> args = values.Split(',').ToList();

                query = $@"INSERT INTO {PhoenixDB.GetTable} ({_columns.Unite(",")}) VALUES ({ConvertingValues(args)})";

                return this;
            }
        }

        public class PhoenixDBCommandSelect
        {
            private string _query;

            private OleDbDataReader Execute(string query)
            {
                OleDbCommand command = new OleDbCommand(query, PhoenixDB.GetConnection);
                return command.ExecuteReader();
            }

            private dynamic ExecuteScalar(string query)
            {
                OleDbCommand command = new OleDbCommand(query, PhoenixDB.GetConnection);
                return command.ExecuteScalar();
            }

            public OleDbDataReader FindAll()
            {
                return Execute($@"SELECT * FROM {PhoenixDB.GetTable}");
            }

            public OleDbDataReader FindWithColumns(string columns)
            {
                return Execute($@"SELECT {columns} FROM {PhoenixDB.GetTable}");
            }

            public OleDbDataReader FindWhere(string conditions)
            {
                return Execute($@"SELECT * FROM {PhoenixDB.GetTable} WHERE {conditions}");
            }

            public OleDbDataReader FindWithColumnsWhere(string columns, string conditions)
            {
                return Execute($@"SELECT {columns} FROM {PhoenixDB.GetTable} WHERE {conditions}");
            }

            public PhoenixDBCommandSelect GetColumns(string columns)
            {
                _query = $@"SELECT {columns} FROM {PhoenixDB.GetTable}";
                return this;
            }

            public PhoenixDBCommandSelect Where(string conditions)
            {
                _query += $@" WHERE {conditions}";
                return this;
            }

            public PhoenixDBCommandSelect OrderBy(string column, string type)
            {
                if (type.ToLower() == "desc" || type.ToLower() == "asc")
                {
                    _query += $@" ORDER BY {column} {type.ToLower()}";
                    return this;
                }

                throw new PhoenixException(
                    "Sorting type must be either [desc] or [asc]!", 
                    new ArgumentException()
                );
            }

            public OleDbDataReader Exec()
            {
                return Execute(_query);
            }

            public dynamic Scalar()
            {
                return ExecuteScalar(_query);
            }
        }
    }
}

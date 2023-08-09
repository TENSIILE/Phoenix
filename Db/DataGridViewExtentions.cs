using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace Phoenix.Db
{
    public static class DataGridViewExtentions
    {
        /// <summary>
        /// Method that adds data to a table by its name.
        /// </summary>
        public static void LoadData(this DataGridView dataGridView, OleDbConnection connection, string tableName)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter($@"SELECT * FROM ${tableName}", connection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet, tableName);

            dataGridView.DataSource = dataSet.Tables[0];
        }
    }
}

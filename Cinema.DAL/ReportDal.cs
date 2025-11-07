using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Cinema.Entities;

namespace Cinema.DAL
{
    public class ReportDal
    {
        public DataTable RevenueByDate(DateTime from, DateTime to)
        {
            var table = new DataTable();
            var db = new Db();

            try
            {
                using (var connection = db.Open())
                using (var command = new SqlCommand("sp_RevenueByDate", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@From", from);
                    command.Parameters.AddWithValue("@To", to);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message, ex);
            }

            return table;
        }
    }
}

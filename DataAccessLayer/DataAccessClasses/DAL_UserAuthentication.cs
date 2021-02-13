using DataAccessLayer.AppConfigurationManager;
using DataAccessLayer.Helpers;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataAccessClasses
{
    public class DAL_UserAuthentication
    {
        public string ConnectionString { get; set; }
        public DAL_UserAuthentication()
        {
            ConnectionString = new AppConfiguration().sqlConnectionString;
        }

        public DBResult<DataTable> DAL_Login(string Email, string password)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_Login", conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.Parameters.Add(new SqlParameter("@Email", Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", password));
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(table);
                        }
                    }
                    conn.Close();
                }
                return new DBResult<DataTable>
                {
                    TransactionResult = true,
                    Data = table
                };
            }
            catch (Exception ex)
            {
                return new DBResult<DataTable>
                {
                    TransactionResult = false
                };
            }
        }

        public DBResult<DataTable> DAL_Registration(MstUser p_UserDetailsModel)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_GetAddUpdateUser", conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.Parameters.Add(new SqlParameter("@FirstName", p_UserDetailsModel.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@LastName", p_UserDetailsModel.LastName));
                        cmd.Parameters.Add(new SqlParameter("@MobileNumber", p_UserDetailsModel.MobileNumber));
                        cmd.Parameters.Add(new SqlParameter("@Email", p_UserDetailsModel.Email));
                        cmd.Parameters.Add(new SqlParameter("@Password", p_UserDetailsModel.Password));
                        cmd.Parameters.Add(new SqlParameter("@IsAdd", true));
                        cmd.Parameters.Add(new SqlParameter("@IsEdit", false));
                        using (var da = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            da.Fill(table);
                        }
                    }
                    conn.Close();
                }
                return new DBResult<DataTable>
                {
                    TransactionResult = true,
                    Data = table
                };
            }
            catch (Exception ex)
            {
                return new DBResult<DataTable>
                {
                    TransactionResult = false
                };
            }

        }

    }
}

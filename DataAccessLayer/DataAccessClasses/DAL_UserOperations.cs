using DataAccessLayer.AppConfigurationManager;
using DataAccessLayer.Helpers;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccessLayer.DataAccessClasses
{
    public class DAL_UserOperations
    {
        public string ConnectionString { get; set; }

        public DAL_UserOperations()
        {
            ConnectionString = new AppConfiguration().sqlConnectionString;
        }

        public DBResult<DataTable> DAL_GetUserList()
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
                        cmd.Parameters.Add(new SqlParameter("@UserId", null));
                        cmd.Parameters.Add(new SqlParameter("@FirstName", null));
                        cmd.Parameters.Add(new SqlParameter("@LastName", null));
                        cmd.Parameters.Add(new SqlParameter("@MobileNumber", null));
                        cmd.Parameters.Add(new SqlParameter("@Email", null));
                        cmd.Parameters.Add(new SqlParameter("@Password", null));
                        cmd.Parameters.Add(new SqlParameter("@CountryId", null));
                        cmd.Parameters.Add(new SqlParameter("@Gender", null));
                        cmd.Parameters.Add(new SqlParameter("@LanguageId", null));
                        cmd.Parameters.Add(new SqlParameter("@Remarks", null));
                        cmd.Parameters.Add(new SqlParameter("@IsAdd", false));
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

        public DBResult<DataTable> DAL_GetProfileData(int UserId)
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
                        cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                        cmd.Parameters.Add(new SqlParameter("@FirstName", null));
                        cmd.Parameters.Add(new SqlParameter("@LastName", null));
                        cmd.Parameters.Add(new SqlParameter("@MobileNumber", null));
                        cmd.Parameters.Add(new SqlParameter("@Email", null));
                        cmd.Parameters.Add(new SqlParameter("@Password", null));
                        cmd.Parameters.Add(new SqlParameter("@CountryId", null));
                        cmd.Parameters.Add(new SqlParameter("@Gender", null));
                        cmd.Parameters.Add(new SqlParameter("@LanguageId", null));
                        cmd.Parameters.Add(new SqlParameter("@Remarks", null));
                        cmd.Parameters.Add(new SqlParameter("@IsAdd", false));
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

        public DBResult<object> DAL_SaveUserDetails(MstUser user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_GetAddUpdateUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 60;
                        cmd.Parameters.Add(new SqlParameter("@UserId", user.UserId));
                        cmd.Parameters.Add(new SqlParameter("@CountryId", user.CountryId));
                        cmd.Parameters.Add(new SqlParameter("@Gender", user.Gender));
                        cmd.Parameters.Add(new SqlParameter("@LanguageId", user.LanguageId));
                        cmd.Parameters.Add(new SqlParameter("@Remarks", user.Remarks));
                        cmd.Parameters.Add(new SqlParameter("@IsAdd", false));
                        cmd.Parameters.Add(new SqlParameter("@IsEdit", true));
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return new DBResult<object>
                {
                    TransactionResult = true
                };
            }
            catch (Exception ex)
            {
                return new DBResult<object>
                {
                    TransactionResult = false
                };
            }
        }

        public DBResult<DataTable> DAL_SaveCountry(MstCountry country)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_GetAddUpdateDeleteCountry", conn))
                    {
                        cmd.CommandTimeout = 60;
                        if (country.CountryId == 0)
                        {
                            cmd.Parameters.Add(new SqlParameter("@CountryName", country.CountryName));
                            cmd.Parameters.Add(new SqlParameter("@CountryCode", country.CountryCode));
                            cmd.Parameters.Add(new SqlParameter("@UserId", country.UserId));
                            cmd.Parameters.Add(new SqlParameter("@IsActive", true));
                            cmd.Parameters.Add(new SqlParameter("@IsAdd", true));
                            cmd.Parameters.Add(new SqlParameter("@IsEdit", false));
                            cmd.Parameters.Add(new SqlParameter("@IsDelete", false));
                        }
                        else
                        {
                            cmd.Parameters.Add(new SqlParameter("@CountryId", country.CountryId));
                            cmd.Parameters.Add(new SqlParameter("@CountryName", country.CountryName));
                            cmd.Parameters.Add(new SqlParameter("@CountryCode", country.CountryCode));
                            cmd.Parameters.Add(new SqlParameter("@UserId", country.UserId));
                            cmd.Parameters.Add(new SqlParameter("@IsAdd", false));
                            cmd.Parameters.Add(new SqlParameter("@IsEdit", true));
                            cmd.Parameters.Add(new SqlParameter("@IsDelete", false));
                        }
                        
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

        public DBResult<DataTable> DAL_DeleteCountry(int CountryId, int UserId)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_GetAddUpdateDeleteCountry", conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.Parameters.Add(new SqlParameter("@CountryId", CountryId));
                        cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                        cmd.Parameters.Add(new SqlParameter("@IsAdd", false));
                        cmd.Parameters.Add(new SqlParameter("@IsEdit", false));
                        cmd.Parameters.Add(new SqlParameter("@IsDelete", true));
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

        public DBResult<DataTable> DAL_GetCountries(int UserId)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_GetAddUpdateDeleteCountry", conn))
                    {
                        cmd.CommandTimeout = 60;
                        cmd.Parameters.Add(new SqlParameter("@CountryId", null));
                        cmd.Parameters.Add(new SqlParameter("@CountryName", null));
                        cmd.Parameters.Add(new SqlParameter("@CountryCode", null));
                        cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                        cmd.Parameters.Add(new SqlParameter("@IsActive", null));
                        cmd.Parameters.Add(new SqlParameter("@IsAdd", false));
                        cmd.Parameters.Add(new SqlParameter("@IsEdit", false));
                        cmd.Parameters.Add(new SqlParameter("@IsDelete", false));
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

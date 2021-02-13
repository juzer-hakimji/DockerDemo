using DataAccessLayer.DataAccessClasses;
using DataAccessLayer.Helpers;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ViewModels;
using ViewModels.Helpers;

namespace BusinessLogicLayer.BusinessLogicClasses
{
    public interface IUserOperationsService
    {
        TransactionResult<List<UserVM>> BL_GetUserList();
        TransactionResult<UserVM> BL_GetProfileData(int UserId);
        TransactionResult<object> BL_SaveUserDetails(UserVM UserVM);
        TransactionResult<List<CountryVM>> BL_SaveCountry(CountryVM CountryVM);
        TransactionResult<List<CountryVM>> BL_DeleteCountry(int CountryId, int UserId);
        TransactionResult<List<CountryVM>> BL_GetCountries(int UserId);
    }
    public class BL_UserOperationsService : IUserOperationsService
    {
        private DAL_UserOperations DAL_UserOperations { get; set; }

        public BL_UserOperationsService()
        {
            DAL_UserOperations = new DAL_UserOperations();
        }

        public TransactionResult<List<UserVM>> BL_GetUserList()
        {
            List<UserVM> UserListVM = new List<UserVM>();
            DBResult<DataTable> DBResult = DAL_UserOperations.DAL_GetUserList();

            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    foreach (DataRow row in DBResult.Data.Rows)
                    {
                        UserListVM.Add(new UserVM
                        {
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            MobileNumber = row["MobileNumber"].ToString(),
                            Email = row["Email"].ToString()
                        });
                    }

                    return new TransactionResult<List<UserVM>>
                    {
                        Success = true,
                        Data = UserListVM
                    };
                }
                else
                {
                    return new TransactionResult<List<UserVM>>
                    {
                        Success = true,
                        Message = "No users found."
                    };
                }
            }
            else
            {
                return new TransactionResult<List<UserVM>>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }
        }

        public TransactionResult<UserVM> BL_GetProfileData(int UserId)
        {
            DBResult<DataTable> DBResult = DAL_UserOperations.DAL_GetProfileData(UserId);
            UserVM userVM;
            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(DBResult.Data.Rows[0]["CountryId"].ToString()))
                    {
                        userVM = new UserVM
                        {
                            UserId = Convert.ToInt32(DBResult.Data.Rows[0]["UserId"]),
                            FirstName = DBResult.Data.Rows[0]["FirstName"].ToString(),
                            LastName = DBResult.Data.Rows[0]["LastName"].ToString(),
                            MobileNumber = DBResult.Data.Rows[0]["MobileNumber"].ToString(),
                            Email = DBResult.Data.Rows[0]["Email"].ToString(),
                            CountryId = null,
                            Gender = DBResult.Data.Rows[0]["Gender"].ToString(),
                            LanguageId = DBResult.Data.Rows[0]["LanguageId"].ToString(),
                            Remarks = DBResult.Data.Rows[0]["Remarks"].ToString(),
                        };
                    }
                    else
                    {
                        userVM = new UserVM
                        {
                            UserId = Convert.ToInt32(DBResult.Data.Rows[0]["UserId"]),
                            FirstName = DBResult.Data.Rows[0]["FirstName"].ToString(),
                            LastName = DBResult.Data.Rows[0]["LastName"].ToString(),
                            MobileNumber = DBResult.Data.Rows[0]["MobileNumber"].ToString(),
                            Email = DBResult.Data.Rows[0]["Email"].ToString(),
                            CountryId = Convert.ToInt32(DBResult.Data.Rows[0]["CountryId"]),
                            Gender = DBResult.Data.Rows[0]["Gender"].ToString(),
                            LanguageId = DBResult.Data.Rows[0]["LanguageId"].ToString(),
                            Remarks = DBResult.Data.Rows[0]["Remarks"].ToString(),
                        };
                    }

                    foreach (DataRow row in DAL_UserOperations.DAL_GetCountries(UserId).Data.Rows)
                    {
                        userVM.CountryList.Add(new CountryVM
                        {
                            CountryId = Convert.ToInt32(row["CountryId"]),
                            CountryName = row["CountryName"].ToString(),
                            CountryCode = row["CountryCode"].ToString()
                        });
                    }
                    return new TransactionResult<UserVM>
                    {
                        Success = true,
                        Data = userVM

                    };
                }
                else
                {
                    return new TransactionResult<UserVM>
                    {
                        Success = false,
                        Message = "cannot show user profile. Please try again."
                    };
                }
            }
            else
            {
                return new TransactionResult<UserVM>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }
        }

        public TransactionResult<object> BL_SaveUserDetails(UserVM UserVM)
        {
            DBResult<object> DBResult = DAL_UserOperations.DAL_SaveUserDetails(new MstUser
            {
                UserId = UserVM.UserId,
                CountryId = UserVM.CountryId,
                Gender = (UserVM.Gender == null) ? (int?)null : Convert.ToInt32(UserVM.Gender),
                LanguageId = (UserVM.LanguageId == null) ? (int?)null : Convert.ToInt32(UserVM.LanguageId),
                Remarks = UserVM.Remarks
            });
            if (DBResult.TransactionResult)
            {
                return new TransactionResult<object>
                {
                    Success = true,
                    Message = "changes saved successfully."
                };
            }
            else
            {
                return new TransactionResult<object>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }
        }

        public TransactionResult<List<CountryVM>> BL_SaveCountry(CountryVM CountryVM)
        {
            List<CountryVM> CountryList = new List<CountryVM>();
            DBResult<DataTable> DBResult = DAL_UserOperations.DAL_SaveCountry(new MstCountry
            {
                CountryId = CountryVM.CountryId ?? 0,
                CountryName = CountryVM.CountryName,
                CountryCode = CountryVM.CountryCode,
                UserId = CountryVM.UserId,
                IsActive = true
            });


            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    foreach (DataRow row in DBResult.Data.Rows)
                    {
                        CountryList.Add(new CountryVM
                        {
                            CountryId = Convert.ToInt32(row["CountryId"]),
                            CountryName = row["CountryName"].ToString(),
                            CountryCode = row["CountryCode"].ToString()
                        });
                    }
                    return new TransactionResult<List<CountryVM>>
                    {
                        Success = true,
                        Data = CountryList
                    };
                }
                else
                {
                    return new TransactionResult<List<CountryVM>>
                    {
                        Success = false,
                        Message = "No country exists."
                    };
                }
            }
            else
            {
                return new TransactionResult<List<CountryVM>>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }

        }

        public TransactionResult<List<CountryVM>> BL_DeleteCountry(int CountryId, int UserId)
        {
            List<CountryVM> CountryList = new List<CountryVM>();
            DBResult<DataTable> DBResult = DAL_UserOperations.DAL_DeleteCountry(CountryId, UserId);

            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    foreach (DataRow row in DBResult.Data.Rows)
                    {
                        CountryList.Add(new CountryVM
                        {
                            CountryId = Convert.ToInt32(row["CountryId"]),
                            CountryName = row["CountryName"].ToString(),
                            CountryCode = row["CountryCode"].ToString()
                        });
                    }
                    return new TransactionResult<List<CountryVM>>
                    {
                        Success = true,
                        Data = CountryList
                    };
                }
                else
                {
                    return new TransactionResult<List<CountryVM>>
                    {
                        Success = true,
                        Message = "No country exists."
                    };
                }
            }
            else
            {
                return new TransactionResult<List<CountryVM>>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }
        }

        public TransactionResult<List<CountryVM>> BL_GetCountries(int UserId)
        {
            List<CountryVM> CountryList = new List<CountryVM>();
            DBResult<DataTable> DBResult = DAL_UserOperations.DAL_GetCountries(UserId);

            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    foreach (DataRow row in DBResult.Data.Rows)
                    {
                        CountryList.Add(new CountryVM
                        {
                            CountryId = Convert.ToInt32(row["CountryId"]),
                            CountryName = row["CountryName"].ToString(),
                            CountryCode = row["CountryCode"].ToString()
                        });
                    }
                    return new TransactionResult<List<CountryVM>>
                    {
                        Success = true,
                        Data = CountryList
                    };
                }
                else
                {
                    return new TransactionResult<List<CountryVM>>
                    {
                        Success = true,
                        Message = "No country exists."
                    };
                }
            }
            else
            {
                return new TransactionResult<List<CountryVM>>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }

        }
    }
}

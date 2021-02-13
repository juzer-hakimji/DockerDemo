using DataAccessLayer.DataAccessClasses;
using DataAccessLayer.Helpers;
using DataAccessLayer.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;
using ViewModels.Helpers;

namespace BusinessLogicLayer.BusinessLogicClasses
{
    public interface IUserAuthenticationService
    {
        TransactionResult<UserVM> BL_Login(string username, string password);
        TransactionResult<UserVM> BL_Registration(UserVM p_UserDetailsModel);
    }
    public class BL_UserAuthenticationService : IUserAuthenticationService
    {
        private readonly AppSettings _appSettings;
        private DAL_UserAuthentication DAL_UserAuthentication { get; set; }
        public BL_UserAuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            DAL_UserAuthentication = new DAL_UserAuthentication();
        }

        public TransactionResult<UserVM> BL_Login(string Email, string password)
        {

            DBResult<DataTable> DBResult = DAL_UserAuthentication.DAL_Login(Email, password);

            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    return new TransactionResult<UserVM>
                    {
                        Success = true,
                        Data = new UserVM
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
                            Token = BL_generateJwtToken(DBResult.Data)
                        }
                    };
                }
                else
                {
                    return new TransactionResult<UserVM>
                    {
                        Success = false,
                        Message = "Incorrect username or password."
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


        public TransactionResult<UserVM> BL_Registration(UserVM p_UserDetailsModel)
        {
            DBResult<DataTable> DBResult = DAL_UserAuthentication.DAL_Registration(new MstUser
            {
                FirstName = p_UserDetailsModel.FirstName,
                LastName = p_UserDetailsModel.LastName,
                MobileNumber = p_UserDetailsModel.MobileNumber,
                Email = p_UserDetailsModel.Email,
                Password = p_UserDetailsModel.Password
            });

            if (DBResult.TransactionResult)
            {
                if (DBResult.Data.Rows.Count > 0)
                {
                    return new TransactionResult<UserVM>
                    {
                        Success = true,
                        Message = "Registration successfull.",
                        Data = new UserVM
                        {
                            UserId = Convert.ToInt32(DBResult.Data.Rows[0]["UserId"]),
                            FirstName = DBResult.Data.Rows[0]["FirstName"].ToString(),
                            LastName = DBResult.Data.Rows[0]["LastName"].ToString(),
                            MobileNumber = DBResult.Data.Rows[0]["MobileNumber"].ToString(),
                            Email = DBResult.Data.Rows[0]["Email"].ToString(),
                            Token = BL_generateJwtToken(DBResult.Data)
                        }
                    };
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
            else
            {
                return new TransactionResult<UserVM>
                {
                    Success = false,
                    Message = "Something went wrong. Please try again."
                };
            }
        }

        private string BL_generateJwtToken(DataTable user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Rows[0]["UserId"].ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

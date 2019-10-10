using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Helpers;

namespace Rite.Software.Shepherd.DAL
{
    public class ApplicationDbContextExtension : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            /*-------------create default user----------------*/
            Church church = new Church()
            {
                Address = "Default Church Address",
                DateRecorded = DateTime.Now,
                Email = "info@defaultchurch.org",
                LastDateModified = DateTime.Now,
                LastModifiedBy = "Applicationn",
                Name = "DEFAULT CHURCH",
                Phone1 = "000000000",
                Phone2 = "000000001",
                RecordedBy = "Application",
                Website = "http://www.defaultchurch.org",
                Dioceses = new List<Diocese>()
                {
                    new Diocese()
                    {
                        Address = "Default Diocese Address",
                        DateRecorded = DateTime.Now,
                        Email = "info@defaultdiocese.org",
                        LastDateModified = DateTime.Now,
                        LastModifiedBy = "Applicationn",
                        Name = "DEFAULT DOICESE",
                        Phone1 = "000000000",
                        Phone2 = "000000001",
                        RecordedBy = "Application",
                        Website = "http://www.defaultdiocese.org",
                        Parishes = new List<Parish>()
                        {
                            new Parish()
                            {
                                Address = "Default Parish Address",
                                DateRecorded = DateTime.Now,
                                Email = "info@defaultparish.org",
                                LastDateModified = DateTime.Now,
                                LastModifiedBy = "Applicationn",
                                Name = "DEFAULT PARISH",
                                Phone1 = "000000000",
                                Phone2 = "000000001",
                                RecordedBy = "Application",
                                Website = "http://www.defaultparish.org",
                                ApplicationGroups = new List<ApplicationGroup>()
                                {
                                    new ApplicationGroup()
                                    {
                                        DateRecorded = DateTime.Now,
                                        LastDateModified = DateTime.Now,
                                        LastModifiedBy = "Application",
                                        Name = "Super Admin",
                                        CanChange = false,
                                        RecordedBy = "Application",
                                        AppUsers = new List<AppUser>()
                                        {
                                            new AppUser()
                                            {
                                                Email = "info@shepherdaid.org",
                                                UserName = "info@shepherdaid.org",
                                                FirstName = "System",
                                                MiddleName = "Super",
                                                LastName = "Admin",
                                                IsActive = true,
                                                PasswordHash = Crypto.HashPassword("WillTriple3@church"),
                                                SecurityStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff"),
                                                RecordedBy = "Application",
                                                DateRecorded = DateTime.Now,
                                                LastModifiedBy = "Application",
                                                LastDateModified = DateTime.Now
                                            }
                                        }

                                    },
                                    new ApplicationGroup()
                                    {
                                        DateRecorded = DateTime.Now,
                                        LastDateModified = DateTime.Now,
                                        LastModifiedBy = "Application",
                                        Name = "Church Admin",
                                        CanChange = false,
                                        RecordedBy = "Application",
                                    }
                                }

                            }
                        }
                    }
                }
            };

            /*--------------end default user------------------*/


            /*--------------add rank type------------------*/
            List<RankType> rankList = new List<RankType>();

            RankType rankType = new RankType { Id = 1, Rank = "Super Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            rankList.Add(rankType);
            rankType = new RankType { Id = 2, Rank = "Church Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            rankList.Add(rankType);
            rankType = new RankType { Id = 3, Rank = "Diocese Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            rankList.Add(rankType);
            rankType = new RankType { Id = 4, Rank = "Parish Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            rankList.Add(rankType);
            rankType = new RankType { Id = 5, Rank = "End User Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            rankList.Add(rankType);
            /*--------------end add rank type------------------*/

            context.RankTypes.AddRange(rankList);
            context.Churches.Add(church);
            base.Seed(context);

            if (!AddDefaultRoles(context))
            {
                //handle errors
            }
        }
        
        private bool AddDefaultRoles(ApplicationDbContext context)
        {
            try
            {

                List<AppRole> appRoleList = new List<AppRole>();

                AppRole appRole = new AppRole { Name = "Super Admin", RankTypeId = 1 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Manage Church", RankTypeId = 1 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Manage Diocese", RankTypeId = 2 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Manage Parishes", RankTypeId = 3 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Manage Groups", RankTypeId = 3 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Manage Roles", RankTypeId = 3 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Manage Users", RankTypeId = 3 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Assign User Roles", RankTypeId = 4 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Revoke User Roles", RankTypeId = 4 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Assign Group Roles", RankTypeId = 4 };
                appRoleList.Add(appRole);

                appRole = new AppRole { Name = "Revoke Group Roles", RankTypeId = 4 };
                appRoleList.Add(appRole);

                context.AppRoles.AddRange(appRoleList);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
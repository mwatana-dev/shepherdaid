using Rite.Software.Shepherdaid.DAL.RegistrationEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;
//using System.Web;
using System.Web.Helpers;

namespace Rite.Software.Shepherdaid.DAL.SecurityEntities
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


            ///*--------------add rank type------------------*/
            //List<RankType> rankList = new List<RankType>();

            //RankType rankType = new RankType { Id = 1, Rank = "Super Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            //rankList.Add(rankType);
            //rankType = new RankType { Id = 2, Rank = "Church Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            //rankList.Add(rankType);
            //rankType = new RankType { Id = 3, Rank = "Diocese Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            //rankList.Add(rankType);
            //rankType = new RankType { Id = 4, Rank = "Parish Admin Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            //rankList.Add(rankType);
            //rankType = new RankType { Id = 5, Rank = "End User Level", RecordedBy = "Application", DateRecorded = DateTime.Now };
            //rankList.Add(rankType);
            ///*--------------end add rank type------------------*/

            //context.RankTypes.AddRange(rankList);
            context.Churches.Add(church);
            base.Seed(context);

            if (!this.AddDefaultTypes(context))
            {
                //handle errors
            }
            if (!this.AddDefaultRoles(context))
            {
                //handle errors
            }
        }
        private bool AddDefaultTypes(ApplicationDbContext context)
        {
            try
            {

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
                context.RankTypes.AddRange(rankList);

                List<GenderType> genderTypeList = new List<GenderType>();
                genderTypeList.Add(new GenderType { Name = "Female", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                genderTypeList.Add(new GenderType { Name = "Male", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                context.GenderTypes.AddRange(genderTypeList);

                List<MaritalStatusType> maritalStatusTypeList = new List<MaritalStatusType>();
                maritalStatusTypeList.Add(new MaritalStatusType { Name = "Single", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                maritalStatusTypeList.Add(new MaritalStatusType { Name = "Married", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                maritalStatusTypeList.Add(new MaritalStatusType { Name = "Widow", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                maritalStatusTypeList.Add(new MaritalStatusType { Name = "Widower", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                context.MaritalStatusTypes.AddRange(maritalStatusTypeList);

                List<RelationshipType> relationshipTypeList = new List<RelationshipType>();
                relationshipTypeList.Add(new RelationshipType { Name = "Father", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                relationshipTypeList.Add(new RelationshipType { Name = "Mother", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                relationshipTypeList.Add(new RelationshipType { Name = "Spouse", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                relationshipTypeList.Add(new RelationshipType { Name = "Brother", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                relationshipTypeList.Add(new RelationshipType { Name = "Sister", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                relationshipTypeList.Add(new RelationshipType { Name = "Guardian", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                relationshipTypeList.Add(new RelationshipType { Name = "Ward", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                context.RelationshipTypes.AddRange(relationshipTypeList);

                List<SalutationType> salutationTypeList = new List<SalutationType>();
                salutationTypeList.Add(new SalutationType { Name = "Mr.", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                salutationTypeList.Add(new SalutationType { Name = "Mrs.", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                salutationTypeList.Add(new SalutationType { Name = "Dr.", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                salutationTypeList.Add(new SalutationType { Name = "Ms", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                salutationTypeList.Add(new SalutationType { Name = "Miss", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                salutationTypeList.Add(new SalutationType { Name = "Chief", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                salutationTypeList.Add(new SalutationType { Name = "Alhadji", RecordedBy = "Application", DateRecorded = DateTime.Now, LastModifiedBy = "Application", LastDateModified = DateTime.Now });
                context.SalutationTypes.AddRange(salutationTypeList);

                //num_code,alpha_2_code,alpha_3_code,en_short_name,nationality
                // Default folder 

                string textFile = HttpContext.Current.Server.MapPath("countries.txt");
                //string textFile = @"Rite.Software.Shepherdaid.DAL\SecurityEntities\countries.txt";
                if (File.Exists(textFile))
                {
                    // Read a text file line by line.
                    string[] countryList = File.ReadAllLines(textFile);
                    List<NationalityType> nationalityTypeList = new List<NationalityType>();
                    string[] data = null;
                    //4,AF,AFG,Afghanistan,Afghan
                    foreach (string country in countryList)
                    {
                        data = country.Split(',');
                        NationalityType nationalityType = new NationalityType()
                        {
                            NumCode = Convert.ToInt32(data[0]),
                            TwoAphaCode = data[1],
                            ThreeAphaCode = data[2],
                            Country = data[3],
                            Nationality = data[4],
                            RecordedBy = "Application",
                            DateRecorded = DateTime.Now,
                            LastModifiedBy = "Applicationn",
                            LastDateModified = DateTime.Now
                        };
                        nationalityTypeList.Add(nationalityType);
                    }
                    context.NationalityTypes.AddRange(nationalityTypeList);
                }
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
            catch (Exception)
            {
                return false;
            }
        }


    }
}
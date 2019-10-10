using MvcBreadCrumbs;
using Rite.Software.Shepherdaid;
using Rite.Software.Shepherdaid.BOL;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;
using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace ShepherdAid.Controllers
{
    [AccessDeniedAuthorize]
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[ChildActionOnly]
        //public PartialViewResult MemberPartial()
        //{
        //    int id = Convert.ToInt32(Session["id"]);
        //    Member member = db.Members.Find(id);
        //    if (member == null)
        //    {
        //        ViewBag.Error = "Member cannot be found.";
        //        return PartialView();
        //    }

        //    //get the member photo into view bag
        //    if (!string.IsNullOrEmpty(member.FilePath))
        //    {
        //        ViewBag.Photo = member.FilePath;
        //    }
        //    else
        //    {
        //        ViewBag.Photo = ShepherdAid.Models.StaticVariables.MemberDefaultPhoto;
        //    }

        //    string name = string.Format("{0} {1} {2} {3} ({4})", member.AspNetUser.FirstName, member.AspNetUser.MiddleName, member.AspNetUser.LastName, Environment.NewLine, member.MemberNo);
        //    ViewBag.Name = name;

        //    return PartialView();
        //}
        //// GET: Members
        [BreadCrumb(Clear = true, Label = "Members")]
        public ActionResult Index(string SuccessMessage, string ErrorMessage)
        {
            try
            {
                ViewBag.Success = SuccessMessage;
                ViewBag.Error = ErrorMessage;

                Session["id"] = null;

                int parishID = Convert.ToInt32(Session["iid"]);
                var result = db.Members.Where(x => x.AppUser.ApplicationGroup.ParishId == parishID).ToList();
                return View(result);

            }
            catch (Exception ex)
            {
                ViewBag.Error = Utility.ShowErrorMessage(ex);
                return View();
            }
        }

        //// GET: Members/Details/5
        //[BreadCrumb(Label = "Member Details")]
        //public ActionResult Details(int id, string SuccessMessage, string ErrorMessage)
        //{
        //    try
        //    {
        //        ViewBag.Success = SuccessMessage;
        //        ViewBag.Error = ErrorMessage;

        //        ViewBag.MemberPhoto = false;

        //        Member member = db.Members.Find(id);
        //        if (member == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        if (!string.IsNullOrEmpty(member.FilePath))
        //        {
        //            ViewBag.MemberPhoto = true;
        //        }
        //        Session["id"] = id;
        //        MyEnums.Status status = (MyEnums.Status)member.StatusTypeID;
        //        switch (status)
        //        {

        //            case MyEnums.Status.CREATED:
        //                return RedirectToAction("file", "MemberPhotoes", new { id = id });
        //            default:
        //                break;
        //        }
        //        return View(member);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = "Details error: " + SABase.ShowErrorMessage(ex);

        //    }

        //    return View();
        //}

        //// GET: Members/Create
        //[BreadCrumb(Label = "Create Member")]
        //public ActionResult Create()
        //{
        //    int parishID = Convert.ToInt32(Session["iid"]);
        //    int countryID = Convert.ToInt32(MyEnums.Nationality.DefaultNationality);
        //    ViewBag.Country = countryID;
        //    ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name");
        //    ViewBag.ParishID = new SelectList(db.Parishes, "ID", "Name");
        //    ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "Name");
        //    ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.ParishID == parishID), "ID", "Name");
        //    ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "Name");
        //    ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "Name", countryID);
        //    ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.ParishID == parishID), "ID", "Name");
        //    ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "Name");
        //    return View();
        //}

        //// POST: Members/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,GenerateID,MemberID,SalutationTypeID,FirstName,MiddleName,LastName,GenderTypeID,DateOfBirth,MaritalStatusTypeID,ResidentAddress,Email,PhoneNumber,OfficePhone,NationalityTypeID,CountyID,Region,MemberTypeID")] MyViewModels.MemberViewModel member)
        //{
        //    int parishID = Convert.ToInt32(Session["iid"]);

        //    try
        //    {

        //        if (ModelState.IsValid)
        //        {
        //            member.ParishID = parishID;
        //            member.RecordedBy = User.Identity.Name;
        //            member.DateRecorded = DateTime.Now;
        //            member.StatusTypeID = Convert.ToInt32(MyEnums.Status.CREATED);

        //            long ID = 0;
        //            string errorMessage = string.Empty;

        //            if (member.GenerateID)//generate member number and create member
        //            {
        //                string memberNo = string.Empty;

        //                if (!SABase.GenerateMemberNumber(out memberNo))
        //                {
        //                    ViewBag.error = SABase.PromptMessage;
        //                }

        //                if (!string.IsNullOrEmpty(memberNo))
        //                {
        //                    member.MemberID = memberNo;


        //                    if (MyApplicationUser.CreateUser(member, ref ID, ref errorMessage))
        //                    {
        //                        Session["id"] = ID;

        //                        return RedirectToAction("file", "MemberPhotoes", new { id = ID });
        //                    }
        //                    else
        //                    {
        //                        ViewBag.Error = errorMessage;
        //                    }
        //                }
        //            }
        //            else//create member without generating member number
        //            {
        //                if (MyApplicationUser.CreateUser(member, ref ID, ref errorMessage))
        //                {
        //                    Session["id"] = ID;

        //                    return RedirectToAction("file", "MemberPhotoes", new { id = ID });
        //                }
        //                else
        //                {
        //                    ViewBag.Error = errorMessage;
        //                }
        //            }


        //        }
        //        else
        //        {
        //            ViewBag.Error = "Invalid model state.";
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ViewBag.Error = "Error: " + ShowErrorMessage(ex);
        //    }

        //    ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name", member.CountyID);
        //    ViewBag.ParishID = new SelectList(db.Parishes, "ID", "Name", member.ParishID);
        //    ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.ParishID == parishID), "ID", "Name", member.MemberTypeID);
        //    ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "Name", member.MaritalStatusTypeID);
        //    ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "Name", member.NationalityTypeID);
        //    ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.ParishID == parishID), "ID", "Name", member.SalutationTypeID);
        //    ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "Name", member.StatusTypeID);
        //    ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "Name", member.GenderTypeID);
        //    return View(member);
        //}

        //// GET: Members/Edit/5
        //[BreadCrumb(Label = "Edit Member")]
        //public ActionResult Edit(int id)
        //{
        //    MyViewModels.MemberViewModel memberViewModel = new MyViewModels.MemberViewModel();
        //    try
        //    {
        //        Session["id"] = id;
        //        Member member = db.Members.Find(id);
        //        Session["original_state"] = member;

        //        int parishID = Convert.ToInt32(Session["iid"]);
        //        ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name", member.CountyID);
        //        ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "Name", member.GenderTypeID);
        //        ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.ParishID == parishID), "ID", "Name", member.MemberTypeID);
        //        ViewBag.ParishID = new SelectList(db.Parishes, "ID", "Name", member.ParishID);
        //        ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "Name", member.MaritalStatusTypeID);
        //        ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "Name", member.NationalityTypeID);
        //        ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.ParishID == parishID), "ID", "Name", member.SalutationTypeID);
        //        ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "Name", member.StatusTypeID);
        //        ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "Name", member.StatusTypeID);

        //        memberViewModel = new MyViewModels.MemberViewModel()
        //        {
        //            Id = member.ID,
        //            DateOfBirth = member.DateOfBirth,
        //            DateRecorded = member.DateRecorded,
        //            Email = member.AspNetUser.Email,
        //            FilePath = member.FilePath,
        //            FirstName = member.AspNetUser.FirstName,
        //            LastName = member.AspNetUser.LastName,
        //            MemberID = member.MemberNo,
        //            MiddleName = member.AspNetUser.MiddleName,
        //            OfficePhone = member.OfficePhone,
        //            ParishID = member.ParishID,
        //            PhoneNumber = member.AspNetUser.PhoneNumber,
        //            RecordedBy = member.RecordedBy,
        //            Region = member.Region,
        //            ResidentAddress = member.ResidentAddress,
        //            AspNetUserId = member.AspNetUserId,
        //            CountyID = member.CountyID,
        //            GenderTypeID = member.GenderTypeID,
        //            MaritalStatusTypeID = member.MaritalStatusTypeID,
        //            MemberTypeID = member.MemberTypeID,
        //            NationalityTypeID = member.NationalityTypeID,
        //            SalutationTypeID = member.SalutationTypeID,
        //            StatusTypeID = member.StatusTypeID,
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = SABase.ShowErrorMessage(ex);
        //    }
        //    return View(memberViewModel);
        //}

        //// POST: Members/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,ParishID,MemberID,SalutationTypeID,FirstName,MiddleName,LastName,GenderTypeID,DateOfBirth,MaritalStatusTypeID,ResidentAddress,Email,PhoneNumber,OfficePhone,NationalityTypeID,CountyID,Region,MemberTypeID,StatusTypeID,AspNetUserId, RecordedBy,DateRecorded")] MyViewModels.MemberViewModel memberViewModel)
        //{
        //    string message = "Member successfully edited.";

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            using (ShepherdAidContext memberDbConnect = new ShepherdAidContext())
        //            {
        //                //AspNetUser aspnetUser = memberDbConnect.AspNetUsers.Find(memberViewModel.AspNetUserId);
        //                //Session["original_state"] = aspnetUser;
        //                //aspnetUser.Email = memberViewModel.Email;
        //                //aspnetUser.FirstName = memberViewModel.FirstName;
        //                //aspnetUser.LastName = memberViewModel.LastName;
        //                //aspnetUser.MiddleName = memberViewModel.MiddleName;
        //                //aspnetUser.PhoneNumber = memberViewModel.PhoneNumber;
        //                //memberDbConnect.Entry(aspnetUser).State = EntityState.Modified;
        //                //memberDbConnect.SaveChanges();

        //                Member member = memberDbConnect.Members.Find(memberViewModel.Id);
        //                member.CountyID = memberViewModel.CountyID;
        //                member.DateOfBirth = memberViewModel.DateOfBirth;
        //                member.GenderTypeID = memberViewModel.GenderTypeID;
        //                member.MemberTypeID = memberViewModel.MemberTypeID;
        //                member.NationalityTypeID = memberViewModel.NationalityTypeID;
        //                member.MaritalStatusTypeID = memberViewModel.MaritalStatusTypeID;
        //                member.MemberNo = memberViewModel.MemberID.ToUpper();
        //                member.OfficePhone = memberViewModel.OfficePhone;
        //                member.Region = memberViewModel.Region;
        //                member.ResidentAddress = memberViewModel.ResidentAddress;
        //                member.SalutationTypeID = memberViewModel.SalutationTypeID;
        //                member.StatusTypeID = memberViewModel.StatusTypeID;
        //                memberDbConnect.Entry(member).State = EntityState.Modified;
        //                memberDbConnect.SaveChanges(User.Identity.Name);


        //                memberDbConnect.SaveChanges(User.Identity.Name);
        //            }


        //            ////ShepherdAidContext entity = new ShepherdAidContext();
        //            //db = new ShepherdAidContext();

        //            //using (ShepherdAidContext entity = new ShepherdAidContext())
        //            //{
        //            //    AspNetUser aspnetUser = entity.AspNetUsers.Find(memberViewModel.AspNetUserId);
        //            //    Session["original_state"] = aspnetUser;
        //            //    aspnetUser.Email = memberViewModel.Email;
        //            //    aspnetUser.FirstName = memberViewModel.FirstName;
        //            //    aspnetUser.LastName = memberViewModel.LastName;
        //            //    aspnetUser.MiddleName = memberViewModel.MiddleName;
        //            //    aspnetUser.PhoneNumber = memberViewModel.PhoneNumber;

        //            //    entity.Entry(aspnetUser).State = EntityState.Modified;
        //            //    entity.SaveChanges(User.Identity.Name);
        //            //}
        //            //AspNetUser aspnetUser = db.AspNetUsers.Find(memberViewModel.AspNetUserId);
        //            //Session["original_state"] = aspnetUser;
        //            //aspnetUser.Email = memberViewModel.Email;
        //            //aspnetUser.FirstName = memberViewModel.FirstName;
        //            //aspnetUser.LastName = memberViewModel.LastName;
        //            //aspnetUser.MiddleName = memberViewModel.MiddleName;
        //            //aspnetUser.PhoneNumber = memberViewModel.PhoneNumber;

        //            //db.Entry(aspnetUser).State = EntityState.Modified;
        //            //db.SaveChanges(User.Identity.Name);

        //            return RedirectToAction("Details", new { id = Session["id"], SuccessMessage = message });
        //        }
        //        else
        //        {
        //            ViewBag.Error = "Invalid model state.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = SABase.ShowErrorMessage(ex);
        //    }
        //    int parishID = Convert.ToInt32(Session["iid"]);
        //    ViewBag.CountyID = new SelectList(db.Counties, "ID", "Name", memberViewModel.CountyID);
        //    ViewBag.GenderTypeID = new SelectList(db.GenderTypes, "ID", "Name", memberViewModel.GenderTypeID);
        //    ViewBag.MemberTypeID = new SelectList(db.MemberTypes.Where(x => x.ParishID == parishID), "ID", "Name", memberViewModel.MemberTypeID);
        //    ViewBag.ParishID = new SelectList(db.Parishes, "ID", "Name", memberViewModel.ParishID);
        //    ViewBag.MaritalStatusTypeID = new SelectList(db.MaritalStatusTypes, "ID", "Name", memberViewModel.MaritalStatusTypeID);
        //    ViewBag.NationalityTypeID = new SelectList(db.NationalityTypes, "ID", "Name", memberViewModel.NationalityTypeID);
        //    ViewBag.SalutationTypeID = new SelectList(db.SalutationTypes.Where(x => x.ParishID == parishID), "ID", "Name", memberViewModel.SalutationTypeID);
        //    ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "Name", memberViewModel.StatusTypeID);
        //    ViewBag.StatusTypeID = new SelectList(db.StatusTypes, "ID", "Name", memberViewModel.StatusTypeID);

        //    return View(memberViewModel);
        //}

        //// GET: Members/Delete/5
        //public ActionResult Delete()
        //{
        //    string message = "Member successfully deleted.";
        //    try
        //    {
        //        long id = Convert.ToInt64(Session["id"]);
        //        Member member = db.Members.Find(id);
        //        AspNetUser aspNetUser = db.AspNetUsers.Find(member.AspNetUserId);
        //        Session["original_state"] = aspNetUser;
        //        db.AspNetUsers.Remove(aspNetUser);
        //        db.Members.Remove(member);
        //        db.SaveChanges(User.Identity.Name);
        //        return RedirectToAction("Index", new { SuccessMessage = message });
        //    }
        //    catch (Exception ex)
        //    {
        //        message = SABase.ShowErrorMessage(ex);
        //        return RedirectToAction("Details", new { id = Session["id"], ErrorMessage = message });
        //    }
        //}

        //// POST: Members/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult DeleteConfirmed(int id)
        ////{
        ////    Member member = db.Members.Find(id);
        ////    db.Members.Remove(member);
        ////    db.SaveChanges(User.Identity.Name);
        ////    return RedirectToAction("Index");
        ////}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}

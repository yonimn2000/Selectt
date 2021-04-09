using Microsoft.AspNet.Identity;
using Selectt.Entities;
using Selectt.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Selectt.Controllers
{
    [Authorize]
    public class RespondentsController : Controller
    {
        private ApplicationDbContext Db { get; } = new ApplicationDbContext();

        private Respondent GetRespondentForCurrentUserById(int? respondentId)
        {
            string userId = User.Identity.GetUserId();
            return Db.Respondents.Where(r => r.Poll.OwnerId.Equals(userId) && r.RespondentId == respondentId).FirstOrDefault();
        }

        private Poll GetPollForCurrentUserById(int? pollId)
        {
            string userId = User.Identity.GetUserId();
            return Db.Polls.Where(p => p.OwnerId.Equals(userId) && p.PollId == pollId).FirstOrDefault();
        }

        // GET: Respondents/Create
        public ActionResult Create(int pollId)
        {
            Poll poll = GetPollForCurrentUserById(pollId);
            if (poll == null || poll.WasSent) // If poll does not belong to current user or was sent,
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            return View(new RespondentCreateModel
            {
                PollId = poll.PollId
            });
        }

        // POST: Respondents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RespondentCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Poll poll = GetPollForCurrentUserById(model.PollId);
                if (poll == null) // If poll does not belong to current user or was sent,
                    return RedirectToAction("Index", "Polls");
                Db.Respondents.Add(new Respondent
                {
                    Email = model.Email,
                    PollId = model.PollId
                });
                Db.SaveChanges();
                return RedirectToAction("Edit", "Polls", new { id = poll.PollId });
            }

            return View(model);
        }

        // GET: Respondents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Respondent respondent = GetRespondentForCurrentUserById(id);
            if (respondent.Poll.WasSent)
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            if (respondent == null)
                return HttpNotFound();
            return View(new RespondentEditModel
            {
                Email = respondent.Email,
                RespondentId = respondent.RespondentId,
                PollId = respondent.PollId
            });
        }

        // POST: Respondents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RespondentEditModel model)
        {
            if (ModelState.IsValid)
            {
                Respondent respondent = GetRespondentForCurrentUserById(model.RespondentId);
                if (respondent.Poll.WasSent)
                    return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
                respondent.Email = model.Email;
                Db.SaveChanges();
                return RedirectToAction("Edit", "Polls", new { id = respondent.PollId });
            }
            return View(model);
        }

        // GET: Respondents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Respondent respondent = GetRespondentForCurrentUserById(id);
            if (respondent.Poll.WasSent)
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            if (respondent == null)
                return HttpNotFound();
            return View(respondent);
        }

        // POST: Respondents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respondent respondent = GetRespondentForCurrentUserById(id);
            if (respondent.Poll.WasSent)
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            if (respondent == null)
                return HttpNotFound(); Db.Respondents.Remove(respondent);
            Db.SaveChanges();
            return RedirectToAction("Edit", "Polls", new { id = respondent.PollId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Db.Dispose();
            base.Dispose(disposing);
        }
    }
}
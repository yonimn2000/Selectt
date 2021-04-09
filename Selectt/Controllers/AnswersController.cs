using Microsoft.AspNet.Identity;
using Selectt.Entities;
using Selectt.Models;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Selectt.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private ApplicationDbContext Db { get; } = new ApplicationDbContext();

        private Answer GetAnswerForCurrentUserById(int? answerId)
        {
            string userId = User.Identity.GetUserId();
            return Db.Answers.Where(a => a.Poll.OwnerId.Equals(userId) && a.AnswerId == answerId).FirstOrDefault();
        }

        private Poll GetPollForCurrentUserById(int? pollId)
        {
            string userId = User.Identity.GetUserId();
            return Db.Polls.Where(p => p.OwnerId.Equals(userId) && p.PollId == pollId).FirstOrDefault();
        }

        // GET: Answers/Create
        public ActionResult Create(int pollId)
        {
            Poll poll = GetPollForCurrentUserById(pollId);
            if (poll == null || poll.WasSent) // If poll does not belong to current user or was sent,
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            return View(new AnswerCreateModel
            {
                PollId = pollId
            });
        }

        // POST: Answers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AnswerCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Poll poll = GetPollForCurrentUserById(model.PollId);
                if (poll == null || poll.WasSent) // If poll does not belong to current user or was sent,
                    return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
                Db.Answers.Add(new Answer
                {
                    PollId = model.PollId,
                    PollAnswer = model.Answer
                });
                Db.SaveChanges();
                return RedirectToAction("Edit", "Polls", new { id = poll.PollId });
            }

            return View(model);
        }

        // GET: Answers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            Answer answer = GetAnswerForCurrentUserById(id);
            
            if (answer == null)
                return HttpNotFound();

            if (answer.Poll.WasSent)
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.

            return View(new AnswerEditModel
            {
                Answer = answer.PollAnswer,
                AnswerId = answer.AnswerId,
                PollId = answer.PollId
            });
        }

        // POST: Answers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnswerEditModel model)
        {
            if (ModelState.IsValid)
            {
                Answer answer = GetAnswerForCurrentUserById(model.AnswerId);
                if (answer.Poll.WasSent)
                    return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
                answer.PollAnswer = model.Answer;

                Db.SaveChanges();
                return RedirectToAction("Edit", "Polls", new { id = answer.PollId });
            }
            return View(model);
        }

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Answer answer = GetAnswerForCurrentUserById(id);
            if (answer == null)
                return HttpNotFound();
            if (answer.Poll.WasSent)
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Answer answer = GetAnswerForCurrentUserById(id);
            if (answer.Poll.WasSent)
                return RedirectToAction("Index", "Polls"); // Do not allow modification after poll submission.
            if (answer == null)
                return HttpNotFound();
            Db.Answers.Remove(answer);
            Db.SaveChanges();
            return RedirectToAction("Edit", "Polls", new { id = answer.PollId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Db.Dispose();
            base.Dispose(disposing);
        }
    }
}
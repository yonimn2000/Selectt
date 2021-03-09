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
    public class PollsController : Controller
    {
        private ApplicationDbContext Db { get; } = new ApplicationDbContext();

        private Poll GetPollForCurrentUserById(int? pollId)
        {
            string userId = User.Identity.GetUserId();
            return Db.Polls.Where(p => p.OwnerId.Equals(userId) && p.PollId == pollId).FirstOrDefault();
        }

        // GET: Polls
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            return View(Db.Polls.Where(p => p.OwnerId.Equals(userId)).ToList());
        }

        // GET: Polls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Poll poll = GetPollForCurrentUserById(id);

            if (poll == null)
                return HttpNotFound();

            return View(poll);
        }

        // GET: Polls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Polls/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PollCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Db.Polls.Add(new Poll
                {
                    Name = model.Name,
                    Question = model.Question,
                    OwnerId = User.Identity.GetUserId(),
                    AreResultsPublic = model.AreResultsPublic,
                    EndDateTime = model.EndDateTime
                });
                Db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Polls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Poll poll = GetPollForCurrentUserById(id);
            if (poll == null)
                return HttpNotFound();
            if (poll.WasSent)
                return RedirectToAction("Index");
            return View(new PollEditModel
            {
                PollId = poll.PollId,
                Name = poll.Name,
                Question = poll.Question,
                AreResultsPublic = poll.AreResultsPublic,
                EndDateTime = poll.EndDateTime,
                Answers = poll.Answers,
                Respondents = poll.Respondents
            });
        }

        // POST: Polls/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PollEditModel model)
        {
            if (ModelState.IsValid)
            {
                Poll poll = GetPollForCurrentUserById(model.PollId);
                if (poll.WasSent)
                    return RedirectToAction("Index"); // Do not allow modification after poll submission.

                poll.Name = model.Name;
                poll.Question = model.Question;
                poll.AreResultsPublic = model.AreResultsPublic;
                poll.EndDateTime = model.EndDateTime;

                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Polls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Poll poll = GetPollForCurrentUserById(id);

            if (poll == null)
                return HttpNotFound();

            return View(poll);
        }

        // POST: Polls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poll poll = GetPollForCurrentUserById(id);
            Db.Polls.Remove(poll);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Polls/Send
        public ActionResult Send(int id)
        {
            Poll poll = GetPollForCurrentUserById(id);

            if (poll == null)
                return HttpNotFound();

            return View(poll);
        }

        // POST: Polls/Send
        [HttpPost, ActionName("Send")]
        [ValidateAntiForgeryToken]
        public ActionResult SendConfirmed(int id)
        {
            Poll poll = GetPollForCurrentUserById(id);
            foreach (Respondent respondent in poll.Respondents)
            {
                string voteUrl = Url.Action("Vote", "Polls", new { token = respondent.Token }, HttpContext.Request.Url.Scheme);
                _ = EmailService.SendAsync(
                    destination: respondent.Email,
                    subject: $"Your Vote Is Needed For '{poll.Name}'",
                    body: $"Click <a href=\"{voteUrl}\">here</a> to vote.");
            }
            poll.WasSent = true;
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Polls/Vote
        [AllowAnonymous]
        public ActionResult Vote(string token)
        {
            Respondent respondent = Db.Respondents.Where(r => r.Token.Equals(token)).FirstOrDefault();

            if (respondent == null)
                return HttpNotFound();

            if (respondent.HasVoted)
                return RedirectToAction("AnonymousResults", new { token });
            ViewBag.Token = token;
            return View(respondent.Poll);
        }

        // GET: Polls/Vote
        [AllowAnonymous]
        public ActionResult SelectVote(string token, int answerId)
        {
            Respondent respondent = Db.Respondents.Where(r => r.Token.Equals(token)).FirstOrDefault();

            if (respondent == null)
                return HttpNotFound();

            Answer answer = respondent.Poll.Answers.Where(a => a.AnswerId == answerId).FirstOrDefault();

            if(answer == null)
                return HttpNotFound();

            respondent.HasVoted = true;
            answer.Responses.Add(new Response
            {
                AnswerId = answer.AnswerId
            });
            Db.SaveChanges();
            return RedirectToAction("AnonymousResults", new { token });
        }

        // GET: Polls/Results
        [AllowAnonymous]
        public ActionResult AnonymousResults(string token)
        {
            Respondent respondent = Db.Respondents.Where(r => r.Token.Equals(token)).FirstOrDefault();

            if (respondent == null)
                return HttpNotFound();

            return View("Results", respondent.Poll);
        }

        // GET: Polls/Results
        public ActionResult Results(int id)
        {
            Poll poll = GetPollForCurrentUserById(id);
            if (poll == null)
                return HttpNotFound();
            return View(poll);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Db.Dispose();
            base.Dispose(disposing);
        }
    }
}
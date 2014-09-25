using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System;

namespace TwitterManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MyLatestTweets()
        {
            var twitterRepo = new TwitterHelper.TwitterRepository();
            return View(await twitterRepo.GetLatesStatuses(User.Identity.Name));
        }

        public async Task<ActionResult> Followers(Int64? cursor = -1)
        {
            var twitterRepo = new TwitterHelper.TwitterRepository();
            return View(await twitterRepo.GetFollowers(User.Identity.Name, (Int64)cursor));
        }

        public async Task<ActionResult> Following(Int64? cursor = -1)
        {
            var twitterRepo = new TwitterHelper.TwitterRepository();
            return View(await twitterRepo.GetFollowings(User.Identity.Name, (Int64)cursor));
        }

        public async Task<ActionResult> NotFollowingBack(Int64? cursor = -1)
        {
            var twitterRepo = new TwitterHelper.TwitterRepository();

            //TODO: this one is a new one that might improve the unfollowers call, to make it more efficient.
            var test = await twitterRepo.GetUnFollowers(User.Identity.Name, (Int64)cursor);

            var users = await twitterRepo.GetFollowings(User.Identity.Name, (Int64)cursor);
            if (users != null && users.users != null)
                users.users = users.users.Where(m => m.IsFollowingBack == false).ToList();
            return View(users);
        }

        public void Unfollow(string id){
            var twitterRepo = new TwitterHelper.TwitterRepository();
            twitterRepo.Unfollow(id, id);
        }

        public void MentionToFix(string id)
        {
            var twitterRepo = new TwitterHelper.TwitterRepository();
            var Status = "@" + id + " you are not following me! fix it...";
            twitterRepo.Tweet(Status);
        }

        public void DirectMessageToFix(string screenname)
        {
            var twitterRepo = new TwitterHelper.TwitterRepository();
            //twitterRepo.Unfollow(id, id);
        }
    }
}
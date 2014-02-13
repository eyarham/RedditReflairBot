using RedditSharp;
using System;
using System.Configuration;
using System.Linq;

namespace RedditReflairBot
{
    class Program
    {
        private static readonly string ModUserName = ConfigurationManager.AppSettings["modUserName"];
        private static readonly string ModPassword = ConfigurationManager.AppSettings["modPassword"];
        private static readonly string SubReddit = ConfigurationManager.AppSettings["subReddit"];
        private static readonly string TriggerText = ConfigurationManager.AppSettings["triggerText"];
        private static readonly string AssignFlair = ConfigurationManager.AppSettings["assignFlair"];
        private static readonly string AssignFlairClass = ConfigurationManager.AppSettings["assignFlairClass"];
        private static readonly string ReadBackDays = ConfigurationManager.AppSettings["readBackDays"];
        private static Subreddit _sub;
        static void Main(string[] args)
        {
            var reddit = new Reddit();
            int backDays;
            try
            {
                reddit.User = reddit.LogIn(ModUserName, ModPassword);
                _sub = reddit.GetSubreddit(SubReddit);
                int.TryParse(ReadBackDays, out backDays);
                if(backDays<1)
                {
                    Console.WriteLine("Invalid value for readBackDays in config file. Please enter an integer greater than 0.");
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Configuration Error. Please confirm values in config file.");
                Console.WriteLine("Press Enter to exit.");
                Console.Read();
                return;
            }
            while (true)
            {
                try
                {
                    AssignFlairs(TriggerText, DateTime.Today.AddDays(backDays));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        private static void AssignFlairs(string keyPhrase, DateTime startDate)
        {
            var now = DateTime.Now;
            try
            {
                var flairedNumber = SetFlair(keyPhrase, AssignFlair, AssignFlairClass, startDate);
                Console.WriteLine("Assigned flair '" + AssignFlair + "' to " + flairedNumber + " posts.");
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error in Assign Flair: " + e.Message);
            }
            var difference = DateTime.Now.Subtract(now);
            Console.WriteLine("Assign Flair Loop finished. Time elapsed: " + difference);
        }
        public static int SetFlair(string keyPhrase, string flairText, string flairClass, DateTime startDate)
        {
            var postsFlaired = 0;
            var posts = _sub.GetNew().Where(a => a != null && a.Created > startDate);
            foreach (var post in from post in posts where post.IsSelfPost where post.LinkFlairText != flairText where post.SelfText.ToLower().Contains(keyPhrase.ToLower()) select post)
            {
                
                post.SetFlair(flairText, flairClass);
                postsFlaired++;
            }
            return postsFlaired;
        }

    }
}

RedditReflairBot
================

Allows Reddit mods to reflair posts in their subs based on a trigger phrase in the selftext.

How to configure
================

1. Open App.config.
2. Fill in the following fields within the appSettings node:

    <add key="subReddit" value="******"/>
	<add key="modUserName" value="******"/>
    <add key="modPassword" value="******"/>
    <add key="triggerText" value="******"/>
    <add key="assignFlair" value="******"/>
    <add key="assignFlairClass" value="******"/>
    <add key="readBackDays" value="******"/>

**subReddit**: the name of the subreddit the application should act on.
**modUserName**: the reddit username of a user who is a moderator on the above subreddit.
**modPassword**: the password for the above reddit user.
**triggerText**: the text that, when located in the selftext of a post, will trigger reflairing of that post.
* triggerText is not case-sensitive by default. The program converts the search string and all the selftext to lowercase for comparison.
**assignFlair**: the flair you wish to assign to posts that contain the triggerText.
**assignFlairClass**: the flair class you wish to assign to posts that contain the triggerText.
**readBackDays**: the number of days between today and the oldest posts you wish to scan.
* note: this may cause you to scan posts so old that you can't reflair them. The program should be able to handle the error, but you will lose cycle time.


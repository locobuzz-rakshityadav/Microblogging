using System;
using System.Collections.Generic;
using System.Linq;

// User entity class
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public List<int> FollowedUserIds { get; set; } // List of user IDs that this user follows
}





// Follow Management service
public class FollowManagementService
{
    private readonly UserManagementService _userManagementService;
    private readonly FeedGenerationService _feedGenerationService;

    public FollowManagementService()
    {
        _userManagementService = new UserManagementService();
        _feedGenerationService = new FeedGenerationService();
    }

    public void FollowUser(int followerId, int followeeId)
    {
        User follower = _userManagementService.GetUserById(followerId);
        User followee = _userManagementService.GetUserById(followeeId);

        if (follower == null || followee == null)
        {
            throw new ArgumentException("Invalid user IDs");
        }

        follower.FollowedUserIds.Add(followeeId);
        _userManagementService.UpdateUser(follower);

        
    }

    public void UnfollowUser(int followerId, int followeeId)
    {
        User follower = _userManagementService.GetUserById(followerId);
        User followee = _userManagementService.GetUserById(followeeId);

        if (follower == null || followee == null)
        {
            throw new ArgumentException("Invalid user IDs");
        }

        follower.FollowedUserIds.Remove(followeeId);
        _userManagementService.UpdateUser(follower);

       
    }

    public List<int> GetFollowers(int userId)
    {
        User user = _userManagementService.GetUserById(userId);

        if (user == null)
        {
            throw new ArgumentException("Invalid user ID");
        }

        if (_feedGenerationService.GetFollowerList(userId, out List<int> followerIds))
        {
            return followerIds;
        }

        return new List<int>();
    }

   

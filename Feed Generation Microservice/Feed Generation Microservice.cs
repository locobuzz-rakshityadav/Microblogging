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

// User Management service for interacting with user-related operations
public class UserManagementService
{
    private readonly Dictionary<int, User> _users; // Simulating in-memory storage for users

    public UserManagementService()
    {
        _users = new Dictionary<int, User>();
    }

    public User GetUserById(int userId)
    {
        if (_users.ContainsKey(userId))
        {
            return _users[userId];
        }
        return null;
    }

    public void UpdateUser(User user)
    {
        if (_users.ContainsKey(user.Id))
        {
            _users[user.Id] = user;
        }
    }
}

// Feed Generation service for interacting with feed-related operations
public class FeedGenerationService
{
    private readonly Dictionary<int, List<int>> _followerLists; // Simulating in-memory storage for follower lists

    public FeedGenerationService()
    {
        _followerLists = new Dictionary<int, List<int>>();
    }

    public void UpdateFollowerList(int userId, List<int> followerIds)
    {
        if (_followerLists.ContainsKey(userId))
        {
            _followerLists[userId] = followerIds;
        }
        else
        {
            _followerLists.Add(userId, followerIds);
        }
    }
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

        UpdateFeedGeneration(followerId);
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

        UpdateFeedGeneration(followerId);
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

    private void UpdateFeedGeneration(int userId)
    {
        User user = _userManagementService.GetUserById(userId);

        if (user == null)
        {

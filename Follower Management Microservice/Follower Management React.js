import React, { useState } from 'react';

const FollowManagement = () => {
  const [followers, setFollowers] = useState([]);
  const [userId, setUserId] = useState('');

  const fetchFollowers = async () => {
    try {
      const response = await fetch(`http://followmanagement/api/followers/${userId}`);
      if (response.ok) {
        const followersData = await response.json();
        setFollowers(followersData);
      } else {
        setFollowers([]);
      }
    } catch (error) {
      console.error('Error fetching followers:', error);
      setFollowers([]);
    }
  };

  return (
    <div>
      <h2>Follow Management</h2>
      <input
        type="text"
        value={userId}
        onChange={(e) => setUserId(e.target.value)}
        placeholder="User ID"
      />
      <button onClick={fetchFollowers}>Fetch Followers</button>
      {followers.length > 0 ? (
        <div>
          <h3>Followers</h3>
          <ul>
            {followers.map((follower) => (
              <li key={follower.id}>
                <p>User ID: {follower.id}</p>
                <p>Name: {follower.name}</p>
                {/* Display other follower details */}
              </li>
            ))}
          </ul>
        </div>
      ) : (
        <p>No followers found for the user.</p>
      )}
    </div>
  );
};

export default FollowManagement;

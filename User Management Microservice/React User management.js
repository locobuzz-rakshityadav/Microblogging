import React, { useState } from 'react';

const UserManagement = () => {
  const [user, setUser] = useState(null);
  const [userId, setUserId] = useState('');

  const fetchUserById = async () => {
    try {
      const response = await fetch(`http://usermanagement/api/users/${userId}`);
      if (response.ok) {
        const userData = await response.json();
        setUser(userData);
      } else {
        setUser(null);
      }
    } catch (error) {
      console.error('Error fetching user:', error);
      setUser(null);
    }
  };

  return (
    <div>
      <h2>User Management</h2>
      <input
        type="text"
        value={userId}
        onChange={(e) => setUserId(e.target.value)}
        placeholder="User ID"
      />
      <button onClick={fetchUserById}>Fetch User</button>
      {user && (
        <div>
          <h3>User Details</h3>
          <p>ID: {user.id}</p>
          <p>Name: {user.name}</p>
          <p>Email: {user.email}</p>
          {/* Display other user details */}
        </div>
      )}
    </div>
  );
};

export default UserManagement;

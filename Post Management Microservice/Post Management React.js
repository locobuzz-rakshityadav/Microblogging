import React, { useState } from 'react';

const PostManagement = () => {
  const [posts, setPosts] = useState([]);
  const [userId, setUserId] = useState('');

  const fetchPostsByUserId = async () => {
    try {
      const response = await fetch(`http://postmanagement/api/posts/user/${userId}`);
      if (response.ok) {
        const postsData = await response.json();
        setPosts(postsData);
      } else {
        setPosts([]);
      }
    } catch (error) {
      console.error('Error fetching posts:', error);
      setPosts([]);
    }
  };

  return (
    <div>
      <h2>Post Management</h2>
      <input
        type="text"
        value={userId}
        onChange={(e) => setUserId(e.target.value)}
        placeholder="User ID"
      />
      <button onClick={fetchPostsByUserId}>Fetch Posts</button>
      {posts.length > 0 ? (
        <div>
          <h3>Posts</h3>
          {posts.map((post) => (
            <div key={post.id}>
              <p>Post ID: {post.id}</p>
              <p>Title: {post.title}</p>
              <p>Content: {post.content}</p>
              {/* Display other post details */}
            </div>
          ))}
        </div>
      ) : (
        <p>No posts found for the user.</p>
      )}
    </div>
  );
};

export default PostManagement;

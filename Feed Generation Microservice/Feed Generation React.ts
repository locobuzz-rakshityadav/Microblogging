import React, { useState } from 'react';

const FeedGeneration = () => {
  const [feed, setFeed] = useState([]);
  const [userId, setUserId] = useState('');

  const generateFeed = async () => {
    try {
      const response = await fetch(`http://feedgeneration/api/feed/${userId}`);
      if (response.ok) {
        const feedData = await response.json();
        setFeed(feedData);
      } else {
        setFeed([]);
      }
    } catch (error) {
      console.error('Error generating feed:', error);
      setFeed([]);
    }
  };

  return (
    <div>
      <h2>Feed Generation</h2>
      <input
        type="text"
        value={userId}
        onChange={(e) => setUserId(e.target.value)}
        placeholder="User ID"
      />
      <button onClick={generateFeed}>Generate Feed</button>
      {feed.length > 0 ? (
        <div>
          <h3>Feed</h3>
          {feed.map((post) => (
            <div key={post.id}>
              <p>Post ID: {post.id}</p>
              <p>Title: {post.title}</p>
              <p>Content: {post.content}</p>
              {/* Display other post details */}
            </div>
          ))}
        </div>
      ) : (
        <p>No posts found in the feed.</p>
      )}
    </div>
  );
};

export default FeedGeneration;

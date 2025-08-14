import React, { useState } from 'react';
import './Auth.css';
import AuthService from '../../services/authService';

const Auth = ({ onLogin }) => {
  const [activeTab, setActiveTab] = useState('login'); 
  const [email, setEmail] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!email || !password || (activeTab === 'signup' && !userName)) {
      setError('All fields are required.');
      return;
    }

    setLoading(true);
    try {
      let data;
      if (activeTab === 'login') {
        data = await AuthService.login({ email, password });
      } else {
        data = await AuthService.register({ email, password, userName });
      }
      onLogin(data);
    } catch (err) {
      setError(err.message || `${activeTab} failed`);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="auth-wrapper">
      <div className="auth-container">
        <div className="tabs">
          <button
            className={activeTab === 'login' ? 'active' : ''}
            onClick={() => setActiveTab('login')}
          >
            Login
          </button>
          <button
            className={activeTab === 'signup' ? 'active' : ''}
            onClick={() => setActiveTab('signup')}
          >
            Signup
          </button>
        </div>
        {error && <p className="error">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div>
            <label>Email:</label>
            <input
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          {activeTab === 'signup' && (
            <div>
              <label>Username:</label>
              <input
                type="text"
                value={userName}
                onChange={(e) => setUserName(e.target.value)}
                required
              />
            </div>
          )}
          <div>
            <label>Password:</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <button type="submit" disabled={loading}>
            {loading ? (activeTab === 'login' ? 'Logging in...' : 'Signing up...') : activeTab === 'login' ? 'Login' : 'Signup'}
          </button>
        </form>
      </div>
    </div>
  );
};

export default Auth;

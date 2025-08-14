import React, { useState } from 'react';
import './Auth.css';
import AuthService from '../../services/authService';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const Auth = ({ onLogin }) => {
  const [activeTab, setActiveTab] = useState('login'); 
  const [email, setEmail] = useState('');
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!email || !password || (activeTab === 'signup' && !userName)) {
      toast.error('⚠️ All fields are required.');
      return;
    }

    setLoading(true);
    let data;

    try {
      if (activeTab === 'login') {
        data = await AuthService.login({ email, password });
      } else {
        data = await AuthService.register({ email, password, userName });
        toast.success('🎉 Signup successful! You can now log in.');
      }

      onLogin(data);
    } catch (err) {
      let errorMessage = '';

      if (err.errors && Array.isArray(err.errors)) {
        errorMessage = err.errors.join(' ');
      } else if (err.message) {
        errorMessage = err.message;
      } else {
        errorMessage = `${activeTab} failed`;
      }

      toast.error(`❌ ${errorMessage}`);
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
            {loading
              ? activeTab === 'login'
                ? 'Logging in...'
                : 'Signing up...'
              : activeTab === 'login'
              ? 'Login'
              : 'Signup'}
          </button>
        </form>
      </div>
    </div>
  );
};

export default Auth;

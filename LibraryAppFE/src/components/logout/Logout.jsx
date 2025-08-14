import React from 'react';
import AuthService from '../../services/authService';
import './Logout.css'; 

const LogoutButton = () => {
  const handleLogout = () => {
    AuthService.logout();
    window.location.reload();
  };

  return (
    <div className="auth-wrapper">
      <div className="auth-container">
        <button
          type="button"
          className="logout-btn"
          onClick={handleLogout}
        >
          Logout
        </button>
      </div>
    </div>
  );
};

export default LogoutButton;

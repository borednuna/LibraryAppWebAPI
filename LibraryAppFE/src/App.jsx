import { useEffect, useState } from 'react'
import './App.css'
import Catalogue from './components/catalogue/Catalogue';
import BookService from './services/bookService';
import Auth from './components/auth/Auth';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Logout from './components/logout/Logout';
import CreateBookForm from './components/createBookForm/CreateBookForm';

function App() {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [books, setBooks] = useState([]);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const data = await BookService.getAll();
        setBooks(data);
      } catch (err) {
        console.log(err);
      }
    }

    fetchBooks();
  }, []);

  const handleLogin = (data) => {
    if (data.token) {
      toast.success('✅ Login successful! Reloading...');
      setToken(data.token); 
      localStorage.setItem("token", data.token);
      setTimeout(() => window.location.reload(), 1000);
    }
  }

  return (
    <>
      <h2>Simple Library App</h2>
      {!token ? 
        <Auth onLogin={handleLogin} />
        :
        <Logout />
      }
      {token ?
        <CreateBookForm />
        :
        <p>Login as admin to create book</p>
      }
      <Catalogue items={books} />
      <ToastContainer position="top-right" autoClose={3000} hideProgressBar={false} />
    </>
  )
}

export default App

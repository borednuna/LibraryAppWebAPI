import { useEffect, useState } from 'react'
import './App.css'
import Catalogue from './components/catalogue/Catalogue';
import BookService from './services/bookService';
import Auth from './components/auth/Auth';

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
      } finally {
        // setLoading(false);
      }
    }

    fetchBooks();
  }, []);

  const handleLogin = (data) => {
    if (data.token) {
      setToken(data.token); 
      localStorage.setItem("token", data.token);
    }
  }

  const handleClick = (item) => {
    alert(`You clicked: ${item}`);
  };

  return (
    <>
      <h2>Simple Library App</h2>
      {!token && 
        <>
          <Auth onLogin={handleLogin} />
        </>
      }
      <Catalogue items={books} onItemClick={handleClick} />
    </>
  )
}

export default App

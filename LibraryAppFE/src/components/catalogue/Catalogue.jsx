import React, { useState } from 'react';
import './Catalogue.css';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import BookService from '../../services/bookService';

const genres = [
  "FICTION",
  "NONFICTION",
  "MYSTERY",
  "SCIENCE_FICTION",
  "FANTASY",
  "ROMANCE",
  "THRILLER",
  "HORROR",
  "BIOGRAPHY",
  "HISTORY",
  "SCIENCE",
  "TECHNOLOGY",
  "PHILOSOPHY",
  "RELIGION",
  "CHILDREN",
  "YOUNG_ADULT",
  "POETRY",
  "DRAMA",
  "COMICS",
  "EDUCATIONAL"
];

const Catalogue = ({ items, onUpdateSuccess }) => {
  const preview =
    "https://th.bing.com/th/id/R.2af68743f13622397f78e4464fc16169?rik=RIMyFGnmnT%2ftIQ&riu=http%3a%2f%2fwww.clipartbest.com%2fcliparts%2fdc8%2fXM8%2fdc8XM8pce.jpeg&ehk=TKWNozNGO3sLaLN6JkpHlzKdp4CbCGbq9GIOHLubEFk%3d&risl=&pid=ImgRaw&r=0";

  const [editingBook, setEditingBook] = useState(null);
  const [formData, setFormData] = useState({ title: '', author: '', isbn: '', genre: '', image: '' });

  if (!items || items.length === 0) {
    return <p className="list-empty">No items available.</p>;
  }

  const handleDelete = async (id) => {
    if (!window.confirm('Are you sure you want to delete this book?')) return;

    try {
      await BookService.delete(id);
      toast.success('✅ Book deleted successfully');
      setTimeout(() => window.location.reload(), 1000);
      if (onUpdateSuccess) onUpdateSuccess();
    } catch (err) {
      if (err?.status === 401 || err?.response?.status === 401) {
        toast.error("Unauthorized");
        return;
      }
      let errorMessage = err.message || 'Delete failed';
      if (err.errors && Array.isArray(err.errors)) {
        errorMessage = err.errors.join(' ');
      }
      toast.error(`❌ ${errorMessage}`);
    }
  };

  const handleEdit = (book) => {
    setEditingBook(book);
    setFormData({
      id: book.id || 0,
      title: book.title || '',
      author: book.author || '',
      isbn: book.isbn || '',
      genre: book.genre ?? '',
      image: book.image || ''
    });
  };

  const handleModalClose = () => {
    setEditingBook(null);
  };

  const handleFormChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "genre" ? parseInt(value, 10) : value,
    }));
  };

  const handleSave = async () => {
    try {
      await BookService.update(formData);
      toast.success('✅ Book updated successfully');
      setTimeout(() => window.location.reload(), 1000);
      setEditingBook(null);
      if (onUpdateSuccess) onUpdateSuccess();
    } catch (err) {
      if (err?.status === 401 || err?.response?.status === 401) {
        toast.error("Unauthorized");
        return;
      }
      let errorMessage = err.message || 'Update failed';
      if (err.errors && Array.isArray(err.errors)) {
        errorMessage = err.errors.join(' ');
      }
      toast.error(`❌ ${errorMessage}`);
    }
  };

  return (
    <>
      <ul className="list-container">
        {items.map((item) => (
          <li key={item.id} className="list-item">
            <img
              src={item.image ?? preview}
              alt={item.title}
              className="list-item-image"
            />
            <div className="list-item-info">
              <h3>{item.title}</h3>
              <p><strong>Author:</strong> {item.author}</p>
              <p><strong>ISBN:</strong> {item.isbn}</p>
              <p><strong>Genre:</strong> {genres[item.genre] ?? item.genre}</p>
              <div className="list-item-actions">
                <button className="btn-edit" onClick={() => handleEdit(item)}>✏️ Edit</button>
                <button className="btn-delete" onClick={() => handleDelete(item.id)}>🗑 Delete</button>
              </div>
            </div>
          </li>
        ))}
      </ul>

      {editingBook && (
        <div className="modal-backdrop">
          <div className="modal">
            <h3>Edit Book</h3>
            <label>Title:</label>
            <input type="text" name="title" value={formData.title} onChange={handleFormChange} />

            <label>Author:</label>
            <input type="text" name="author" value={formData.author} onChange={handleFormChange} />

            <label>ISBN:</label>
            <input type="text" name="isbn" value={formData.isbn} onChange={handleFormChange} />

            <label>Genre:</label>
            <select name="genre" value={formData.genre} onChange={handleFormChange}>
              {genres.map((g, index) => (
                <option key={index} value={index}>{g}</option>
              ))}
            </select>

            <label>Image URL:</label>
            <input type="text" name="image" value={formData.image} onChange={handleFormChange} />

            <div className="modal-actions">
              <button className="btn-cancel" onClick={handleModalClose}>Cancel</button>
              <button className="btn-save" onClick={handleSave}>Save</button>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default Catalogue;

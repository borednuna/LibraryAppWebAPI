import React, { useState } from "react";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import BookService from "../../services/bookService";
import "./CreateBookForm.css";

const GENRES = [
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

const CreateBookForm = () => {
  const [formData, setFormData] = useState({
    title: "",
    author: "",
    isbn: "",
    genre: "",
    image: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "genre" ? parseInt(value, 10) : value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.title || !formData.author) {
      toast.error("Title and Author are required");
      return;
    }

    try {
      await BookService.create(formData);
      toast.success("✅ Book created successfully");
      setFormData({
        title: "",
        author: "",
        isbn: "",
        genre: "",
        image: "",
      });
      setTimeout(() => window.location.reload(), 1000);
    } catch (err) {
      let errorMessage = err.message || "Create failed";
      if (err.errors && Array.isArray(err.errors)) {
        errorMessage = err.errors.join(" ");
      }
      toast.error(`❌ ${errorMessage}`);
    }
  };

  return (
    <form className="create-book-form" onSubmit={handleSubmit}>
      <h3>Add New Book</h3>

      <div className="form-grid">
        <div className="form-column">
          <label>Title:</label>
          <input
            type="text"
            name="title"
            value={formData.title}
            onChange={handleChange}
            placeholder="Book title"
          />

          <label>Author:</label>
          <input
            type="text"
            name="author"
            value={formData.author}
            onChange={handleChange}
            placeholder="Author name"
          />

          <label>ISBN:</label>
          <input
            type="text"
            name="isbn"
            value={formData.isbn}
            onChange={handleChange}
            placeholder="ISBN number"
          />
        </div>

        <div className="form-column">
          <label>Genre:</label>
          <select
            name="genre"
            value={formData.genre}
            onChange={handleChange}
          >
            <option value="">Select genre</option>
            {GENRES.map((genreName, index) => (
              <option key={index} value={index}>
                {genreName.replace(/_/g, " ")}
              </option>
            ))}
          </select>

          <label>Image URL:</label>
          <input
            type="text"
            name="image"
            value={formData.image}
            onChange={handleChange}
            placeholder="Cover image link"
          />

          <button type="submit" className="btn-save">
            ➕ Add Book
          </button>
        </div>
      </div>
    </form>
  );
};

export default CreateBookForm;

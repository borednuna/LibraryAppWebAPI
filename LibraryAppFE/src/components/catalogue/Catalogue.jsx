import React from 'react';
import './Catalogue.css';

const Catalogue = ({ items, onItemClick }) => {
  if (!items || items.length === 0) {
    return <p className="list-empty">No items available.</p>;
  }

  return (
    <ul className="list-container">
      {items.map((item) => (
        <li
          key={item.id}
          className="list-item"
          onClick={() => onItemClick && onItemClick(item)}
        >
          <img
            src={item.image}
            alt={item.title}
            className="list-item-image"
          />
          <div className="list-item-info">
            <h3>{item.title}</h3>
            <p><strong>Author:</strong> {item.author}</p>
            <p><strong>ISBN:</strong> {item.isbn}</p>
            <p><strong>Genre:</strong> {item.genre}</p>
          </div>
        </li>
      ))}
    </ul>
  );
};

export default Catalogue;

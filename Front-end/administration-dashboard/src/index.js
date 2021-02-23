import React from 'react';
import ReactDOM from 'react-dom';
import PagesManager from "./components/PagesManager";

const goods = [{type: "books", name: "Книги"}, {type: "pencils", name: "Карандаши"}];

ReactDOM.render(
  <React.StrictMode>
    <PagesManager goods = {goods} />
  </React.StrictMode>,
  document.getElementById('root')
);
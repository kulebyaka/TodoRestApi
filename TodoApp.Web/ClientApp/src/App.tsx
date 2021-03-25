import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

import AddTodo from "./components/add-todo";
import TodoOverview from "./components/todo-overview";
import TodoList from "./components/todo-list";
import {Link, Switch, Route, BrowserRouter} from "react-router-dom";

function App() {
  return (
	  <BrowserRouter>
		<div>
		  <nav className="navbar navbar-expand navbar-dark bg-dark">
			<a href="/todos" className="navbar-brand">
			  Barclays
			</a>
			<div className="navbar-nav mr-auto">
			  <li className="nav-item">
				<Link to={"/todos"} className="nav-link">
				  Todos
				</Link>
			  </li>
			  <li className="nav-item">
				<Link to={"/add"} className="nav-link">
				  Add
				</Link>
			  </li>
			</div>
		  </nav>

		  <div className="container mt-3">
			<Switch>
			  <Route exact path={["/", "/todos"]} component={TodoList}/>
			  <Route exact path="/add" component={AddTodo}/>
			  <Route path="/todos/:id" component={TodoOverview}/>
			</Switch>
		  </div>
		</div>
	  </BrowserRouter>
  );
}

export default App;

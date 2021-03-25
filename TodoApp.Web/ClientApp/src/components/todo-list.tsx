import React, {useState, useEffect} from "react";
import TodoDataService from "../services/TodoService";
import {Link} from "react-router-dom";
import {ITodo} from "../models/ApiDataType";

const TodoList = () => {
  const [todos, setTodos] = useState<ITodo[]>([]);
  const [currentTodo, setCurrentTodo] = useState<ITodo | null>(null);
  const [currentIndex, setCurrentIndex] = useState(-1);

  useEffect(() => {
    retrieveTodos();
  }, []);

  const retrieveTodos = () => {
    TodoDataService.getAll()
        .then(response => {
          setTodos(response.data);
          console.log(response.data);
        })
        .catch(e => {
          console.log(e);
        });
  };

  const refreshList = () => {
    retrieveTodos();
    setCurrentTodo(null);
    setCurrentIndex(-1);
  };

  const setActiveTodo = (todo: ITodo, index: number) => {
    setCurrentTodo(todo);
    setCurrentIndex(index);
  };

  const completeTodo = (id: string) => {
    TodoDataService.complete(id).then(
        () => refreshList()
    );
  };

  return (
      <div className="list row">
        <div className="col-md-6">
          <h4>Todos List</h4>

          <ul className="list-group">
            {todos &&
            todos.map((todo, index) => (
                <li
                    className={
                      "list-group-item " + (index === currentIndex ? "active" : "")
                    }
                    onClick={() => setActiveTodo(todo, index)}
                    key={index}>
                  {todo.title}
                </li>
            ))}
          </ul>


        </div>
        <div className="col-md-6">
          {currentTodo ? (
              <div>
                <h4>Todo</h4>
                <div>
                  <label>
                    <strong>Title:</strong>
                  </label>{" "}
                  {currentTodo.title}
                </div>
                <div>
                  <label>
                    <strong>priority:</strong>
                  </label>{" "}
                  {currentTodo.priority}
                </div>
                <div>
                  <label>
                    <strong>state:</strong>
                  </label>{" "}
                  {currentTodo.state}
                </div>

                <Link to={"/todos/" + currentTodo.id} className="badge badge-warning">
                  Edit
                </Link>

                <button className="m-3 btn btn-sm btn-danger" onClick={() => completeTodo(currentTodo.id)}>
                  Complete
                </button>

              </div>
          ) : (
              <div>
                <br/>
                <p>Please click on a Todo...</p>
              </div>
          )}
        </div>
      </div>
  );
};

export default TodoList;

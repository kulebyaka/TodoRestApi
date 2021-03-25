import React, {useState} from "react";
import TodoDataService from "../services/TodoService";
import {ITodo, State} from "../models/ApiDataType";

const AddTodo = () => {

  const initialTodoState: ITodo = {
    id: "",
    title: "",
    priority: 0,
    state: State.NotStarted,
  }

  const [todo, setTodo] = useState<ITodo>(initialTodoState);
  const [submitted, setSubmitted] = useState(false);

  const handleInputChange = (event: { target: { name: any; value: any; }; }) => {
    const {name, value} = event.target;
    setTodo({...todo, [name]: value});
  };

  const saveTodo = () => {
    TodoDataService.create(todo)
        .then(response => {
          setTodo(
              response.data
          );
          setSubmitted(true);
          console.log(response.data);
        })
        .catch(e => {
          console.log(e);
        });
  };

  const newTodo = () => {
    setTodo(initialTodoState);
    setSubmitted(false);
  };

  return (
      <div className="submit-form">
        {submitted ? (
            <div>
              <h4>You submitted successfully!</h4>
              <button className="btn btn-success" onClick={newTodo}>
                Add
              </button>
            </div>
        ) : (
            <div>
              <div className="form-group">
                <label htmlFor="title">Title</label>
                <input
                    type="text"
                    className="form-control"
                    id="title"
                    required
                    value={todo.title}
                    onChange={handleInputChange}
                    name="title"
                />
              </div>

              <div className="form-group">
                <label htmlFor="priority">priority</label>
                <input
                    type='number'
                    className="form-control"
                    id="priority"
                    required
                    value={todo.priority}
                    onChange={handleInputChange}
                    name="priority"
                />
              </div>

              <button onClick={saveTodo} className="btn btn-success">
                Save
              </button>
            </div>
        )}
      </div>
  );
};

export default AddTodo;

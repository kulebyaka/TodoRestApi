import React, {useState, useEffect} from "react";
import TodoDataService from "../services/TodoService";
import {ITodo, State} from "../models/ApiDataType";

const TodoOverview = (props: { match: { params: { id: string; }; }; history: string[]; }) => {
  const initialTodoState: ITodo = {
    id: "",
    title: "",
    priority: 0,
    state: State.NotStarted,
  }
  const [currentTodo, setCurrentTodo] = useState<ITodo>(initialTodoState);
  const [message, setMessage] = useState("");

  const getTodo = (id: string) => {
    TodoDataService.get(id)
        .then(response => {
          setCurrentTodo(response.data);
          console.log(response.data);
        })
        .catch(e => {
          console.log(e);
        });
  };

  useEffect(() => {
    getTodo(props.match.params.id);
  }, [props.match.params.id]);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const {name, value} = event.currentTarget;
    setCurrentTodo({...currentTodo, [name]: value});

  };

  // const updatePublished = status => {
  //   var data = {
  //     id: currentTodo.id,
  //     title: currentTodo.title,
  //     description: currentTodo.description,
  //     published: status
  //   };
  //
  //   TodoDataService.update(currentTodo.id, data)
  //     .then(response => {
  //       setCurrentTodo({ ...currentTodo, published: status });
  //       console.log(response.data);
  //     })
  //     .catch(e => {
  //       console.log(e);
  //     });
  // };

  const updateTodo = () => {
    TodoDataService.update(currentTodo.id, currentTodo)
        .then(response => {
          console.log(response.data);
          setMessage("The todo was updated successfully!");
        })
        .catch(e => {
          console.log(e);
        });
  };

  const deleteTodo = () => {
    TodoDataService.remove(currentTodo.id)
        .then(response => {
          console.log(response.data);
          props.history.push("/todos");
        })
        .catch(e => {
          console.log(e);
        });
  };

  return (
      <div>
        {currentTodo ? (
            <div className="edit-form">
              <h4>Todo</h4>
              <form>
                <div className="form-group">
                  <label htmlFor="title">Title</label>
                  <input
                      type="text"
                      className="form-control"
                      id="title"
                      name="title"
                      value={currentTodo.title}
                      onChange={handleInputChange}
                  />
                </div>
                <div className="form-group">
                  <label htmlFor="description">Description</label>
                  <input
                      type="text"
                      className="form-control"
                      id="description"
                      name="description"
                      value={currentTodo.priority}
                      onChange={handleInputChange}
                  />
                </div>

                <div className="form-group">
                  <label>
                    <strong>Status:</strong>
                  </label>
                  {currentTodo.state}
                </div>
              </form>

              {/*{currentTodo.published ? (*/}
              {/*  <button*/}
              {/*    className="badge badge-primary mr-2"*/}
              {/*    onClick={() => updatePublished(false)}*/}
              {/*  >*/}
              {/*    UnPublish*/}
              {/*  </button>*/}
              {/*) : (*/}
              {/*  <button*/}
              {/*    className="badge badge-primary mr-2"*/}
              {/*    onClick={() => updatePublished(true)}*/}
              {/*  >*/}
              {/*    Publish*/}
              {/*  </button>*/}
              {/*)}*/}

              <button className="badge badge-danger mr-2" onClick={deleteTodo}>
                Delete
              </button>

              <button
                  type="submit"
                  className="badge badge-success"
                  onClick={updateTodo}
              >
                Update
              </button>
              <p>{message}</p>
            </div>
        ) : (
            <div>
              <br/>
              <p>Please click on a Todo...</p>
            </div>
        )}
      </div>
  );
};

export default TodoOverview;

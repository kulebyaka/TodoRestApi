import http from "../http-common";
import {AxiosResponse} from "axios";
import {ITodo, State} from "../models/ApiDataType";

export const getAll = async (): Promise<AxiosResponse<ITodo[]>> => {
  try {
    return await http.get('/todos/list')
  } catch (error) {
    throw new Error(error)
  }
}

const create = async (formData: ITodo): Promise<AxiosResponse<ITodo>> => {
  try {
    const todo: Omit<ITodo, 'id'> = {
      title: formData.title,
      priority: formData.priority,
      state: State.NotStarted,
    }
    const saveTodo: AxiosResponse<ITodo> = await http.post(
        '/todos/add',
        todo
    )
    return saveTodo
  } catch (error) {
    throw new Error(error)
  }
}

export const complete = async (id: string) => {
  try {
    await http.patch(`/todos/${id}/complete`)
  } catch (error) {
    throw new Error(error)
  }
}

export const updateTodo = async (todo: ITodo) => {
  try {
    const todoUpdate: Pick<ITodo, 'state'> = {
      state: State.Completed,
    }
    await http.put(`/todos/${todo.id}`, todoUpdate)
  } catch (error) {
    throw new Error(error)
  }
}

const get = (id: string): Promise<AxiosResponse<ITodo>> => {
  return http.get(`/todos/${id}`);
};

const update = async (id: string, data: ITodo) => {
  try {
    return await http.put(`/todos/${id}`, data);
  } catch (error) {
    throw new Error(error)
  }
};

const remove = (id: string) => {
  return http.delete(`/todos/${id}`);
};

export default {
  getAll,
  get,
  create,
  update,
  complete,
  remove
};

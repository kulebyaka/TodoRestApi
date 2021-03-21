import axios, {AxiosResponse} from 'axios'

const baseUrl: string = 'https://localhost:5001'

export const getTodos = async (): Promise<AxiosResponse<ApiDataType>> => {
  try {
    const todos: AxiosResponse<ApiDataType> = await axios.get(
        baseUrl + '/todos'
    )
    return todos
  } catch (error) {
    throw new Error(error)
  }
}

export const addTodo = async (
    formData: ITodo
): Promise<AxiosResponse<ApiDataType>> => {
  try {
    const todo: Omit<ITodo, 'id'> = {
      title: formData.title,
      priority: formData.priority,
      status: 0,
    }
    const saveTodo: AxiosResponse<ApiDataType> = await axios.post(
        baseUrl + '/todos',
        todo
    )
    return saveTodo
  } catch (error) {
    throw new Error(error)
  }
}

export const updateTodo = async (
    todo: ITodo
): Promise<AxiosResponse<ApiDataType>> => {
  try {
    const todoUpdate: Pick<ITodo, 'status'> = {
      status: 1,
    }
    const updatedTodo: AxiosResponse<ApiDataType> = await axios.put(
        `${baseUrl}/edit-todo/${todo.id}`,
        todoUpdate
    )
    return updatedTodo
  } catch (error) {
    throw new Error(error)
  }
}

export const deleteTodo = async (
    _id: string
): Promise<AxiosResponse<ApiDataType>> => {
  try {
    const deletedTodo: AxiosResponse<ApiDataType> = await axios.delete(
        `${baseUrl}/delete-todo/${_id}`
    )
    return deletedTodo
  } catch (error) {
    throw new Error(error)
  }
}

interface ITodo {
  id: string
  title: string
  priority: number
  status: number
}

type TodoProps = {
  todo: ITodo
}

type ApiDataType = {
  message: string
  status: string
  todos: ITodo[]
  todo?: ITodo
}
  
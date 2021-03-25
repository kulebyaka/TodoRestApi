export interface ITodo {
  id: string
  title: string
  priority: number
  state: State
}

export enum State {
  NotStarted,
  InProgress,
  Completed
}